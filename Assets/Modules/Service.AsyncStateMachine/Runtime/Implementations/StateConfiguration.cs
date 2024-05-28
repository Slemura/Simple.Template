using System;
using System.Collections.Generic;
using RpDev.Services.AsyncStateMachine.Abstractions;

namespace RpDev.Services.AsyncStateMachine
{
    public sealed class StateConfiguration<TTrigger>
    {
        private readonly StateConfigurationWrapper<TTrigger> _wrapper;
        private readonly List<Type> _thruStates;
        
        private Type _stateType;
        public IReadOnlyList<Type> ThruStates => _thruStates;

        public Type StateType => _stateType;

        public StateConfiguration(StateConfigurationWrapper<TTrigger> wrapper)
        {
            _wrapper = wrapper;
            _thruStates = new List<Type>();
        }

        public StateConfiguration<TTrigger> ThruState<T>() where T : IThruState
        {
            _thruStates.Add(typeof(T));
            return this;
        }

        public StateConfiguration<TTrigger> AllowTransition<T>(TTrigger trigger) where T : IState
        {
            return _wrapper.AllowTransition<T>(trigger);
        }

        internal StateConfiguration<TTrigger> AllowTransitionInternal<T>() where T : IState
        {
            _stateType = typeof(T);
            return this;
        }
    }
}