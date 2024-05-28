using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.Services.AsyncStateMachine.Abstractions;
using RpDev.Services.AsyncStateMachine.Data.Exceptions;

namespace RpDev.Services.AsyncStateMachine
{
    public sealed class StateMachine<TTrigger> : IStateMachine, IDisposable
        where TTrigger : Enum
    {
        private readonly Dictionary<Type, StateConfigurationWrapper<TTrigger>> _configurations;
        private readonly IStateFactory _factory;
        
        private StateConfiguration<TTrigger> _initialStateConfiguration;

        private bool _isStarted;
        
        private Action<IState> _onStateChanged;

        private IState CurrentState { get; set; }

        public StateMachine(IStateFactory plainClassFactory)
        {
            _factory = plainClassFactory;
            _configurations = new Dictionary<Type, StateConfigurationWrapper<TTrigger>>();
        }

        public StateConfiguration<TTrigger> SetInitialState<T>() where T : IState
        {
            Configure<T>();
            _initialStateConfiguration = new StateConfiguration<TTrigger>(null);
            _initialStateConfiguration.AllowTransitionInternal<T>();
            
            return _initialStateConfiguration;
        }
        
        public void Dispose()
        {
            CurrentState?.Dispose();
        }

        /// <summary>
        /// Runs the state machine.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public async UniTask Start(CancellationToken cancellationToken)
        {
            if (_isStarted)
                throw new RuntimeStateMachineException($"'{GetType().Name}' is already started.");

            _isStarted = true;
            await Enter(_initialStateConfiguration, cancellationToken);
        }

        /// <summary>
        /// Runs the state machine with payload if initial state requires payload.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TPayload">Payload that will be passed to the initial state</typeparam>
        public async UniTask Start<TPayload>(TPayload payload, CancellationToken cancellationToken)
        {
            if (_isStarted)
                throw new RuntimeStateMachineException($"'{GetType().Name}' is already started.");
            
            _isStarted = true;
            await Enter(_initialStateConfiguration, payload, cancellationToken);
        }

        /// <summary>
        /// Stops the state machine with dispose of current state.
        /// State machine will be rolled back to the initial state
        /// </summary>
        /// <param name="cancellationToken"></param>
        public async UniTask Stop(CancellationToken cancellationToken)
        {
            await ExitCurrentState(null, cancellationToken);

            CurrentState = null;

            _isStarted = false;
        }
        
        public void SubscribeOnStateChanged(Action<IState> callback)
        {
            _onStateChanged += callback;
        }

        public void UnsubscribeOnStateChanged(Action<IState> callback)
        {
            _onStateChanged = callback;
        }

        public StateConfigurationWrapper<TTrigger> Configure<T>() where T : IState
        {
            if (_configurations.TryGetValue(typeof(T), out var configuration))
                return configuration;

            configuration = new StateConfigurationWrapper<TTrigger>();
            _configurations.Add(typeof(T), configuration);

            return configuration;
        }
        
        public async UniTask Dispatch(TTrigger trigger, CancellationToken cancellationToken)
        {
            await DispatchInternal(trigger, cancellationToken);
        }

        public async UniTask Dispatch<TPayload>(TTrigger trigger, TPayload payload, CancellationToken cancellationToken)
        {
            await DispatchInternal(trigger, payload, cancellationToken);
        }

        private async UniTask Enter(StateConfiguration<TTrigger> stateConfiguration, CancellationToken cancellationToken)
        {
            await ExitCurrentState(stateConfiguration.StateType, cancellationToken);

            var state = _factory.Create(stateConfiguration.StateType);

            CurrentState = (IState) state;

            var plainState = (IPlainState) CurrentState;

            foreach (var thruStateType in stateConfiguration.ThruStates)
            {
                var thruState = (IThruState)_factory.Create(thruStateType);
                await thruState.Thru(cancellationToken);
                
                thruState.Dispose();
            }
            
            await plainState.OnEnter(cancellationToken);
            
            _onStateChanged?.Invoke(CurrentState);
        }

        private async UniTask Enter<TPayload>(StateConfiguration<TTrigger> stateConfiguration, TPayload payload, CancellationToken cancellationToken)
        {
            await ExitCurrentState(stateConfiguration.StateType, cancellationToken);

            var state = _factory.Create(stateConfiguration.StateType);

            var payloadState = (IPayloadedState<TPayload>) state;

            CurrentState = payloadState;

            foreach (var thruStateType in stateConfiguration.ThruStates)
            {
                var thruState = (IThruState)_factory.Create(thruStateType);
                await thruState.Thru(cancellationToken);
                thruState.Dispose();
            }
            
            await payloadState.OnEnter(payload, cancellationToken);
            
            _onStateChanged?.Invoke(CurrentState);
        }

        private async UniTask ExitCurrentState(Type stateType, CancellationToken cancellationToken)
        {
            if (CurrentState != null)
            {
                await CurrentState.OnExit(cancellationToken);
                
                CurrentState.Dispose();
            }
        }

        private async UniTask DispatchInternal(TTrigger trigger, CancellationToken cancellationToken)
        {
            var type = VerifyAndReturnStateType(trigger);

            if (type == null)
                return;
            
            await Enter(type, cancellationToken);
        }

        private async UniTask DispatchInternal<TPayload>(TTrigger trigger, TPayload payload,
            CancellationToken cancellationToken)
        {
            var type = VerifyAndReturnStateType(trigger);

            if (type == null)
                return;
            
            await Enter(type, payload, cancellationToken);
        }

        private StateConfiguration<TTrigger> VerifyAndReturnStateType(TTrigger trigger)
        {
            if (CurrentState == null)
            {
                throw new RuntimeStateMachineException(
                    $"State Machine '{GetType().Name}' is not initialized. Use '{nameof(Start)}' method");
            }

            if (_configurations.TryGetValue(CurrentState.GetType(), out var configuration) == false)
            {
                var stateName = CurrentState.GetType().Name;
                throw new ArgumentException(
                    $"State Machine '{GetType().Name}' has no configuration for '{stateName}' state.");
            }

            if (configuration.TryGetTargetStateType(trigger, out var nextStateType) == false)
            {
                var stateName = CurrentState.GetType().Name;
                var triggerName = $"{typeof(TTrigger).Name}.{Enum.GetName(typeof(TTrigger), trigger)}";
                throw new ArgumentException($"State '{stateName}' is not configured for '{triggerName}' trigger.");
            }

            return nextStateType;
        }
    }
}