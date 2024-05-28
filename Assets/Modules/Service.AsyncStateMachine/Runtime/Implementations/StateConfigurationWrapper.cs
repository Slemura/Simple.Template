using System;
using System.Collections.Generic;
using RpDev.Services.AsyncStateMachine.Abstractions;

namespace RpDev.Services.AsyncStateMachine
{
    public sealed class StateConfigurationWrapper<TTrigger>
    {
        private readonly Dictionary<TTrigger, StateConfiguration<TTrigger>> _stateTypeByTrigger;
        
        public StateConfigurationWrapper()
        {
            _stateTypeByTrigger = new Dictionary<TTrigger, StateConfiguration<TTrigger>>();
        }

        public StateConfiguration<TTrigger> AllowTransition<T>(TTrigger trigger)
            where T : IState
        {
            if (_stateTypeByTrigger.ContainsKey(trigger))
            {
                var stateName = typeof(T).Name;
                var triggerName = $"{typeof(TTrigger).Name}.{Enum.GetName(typeof(TTrigger), trigger)}";
                throw new ArgumentException(
                    $"State '{stateName}' is already configured for '{triggerName}' trigger.");
            }

            var configuration = new StateConfiguration<TTrigger>(this);
            configuration.AllowTransitionInternal<T>();
            
            _stateTypeByTrigger.Add(trigger, configuration);
            
            return configuration;
        }

        public bool TryGetTargetStateType(TTrigger trigger, out StateConfiguration<TTrigger> stateType)
        {
            return _stateTypeByTrigger.TryGetValue(trigger, out stateType);
        }
    }
}