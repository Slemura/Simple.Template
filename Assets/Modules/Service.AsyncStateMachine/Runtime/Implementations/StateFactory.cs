using System;
using RpDev.Services.AsyncStateMachine.Abstractions;
using RpDev.Services.GenericFactories.VContainer;
using VContainer;

namespace RpDev.Services.AsyncStateMachine.Implementations
{
    public class StateFactory : IStateFactory
    {
        private readonly IObjectResolver _container;
        private readonly IPlainClassFactory _plainClassFactory;

        public StateFactory(IObjectResolver container, IPlainClassFactory plainClassFactory)
        {
            _container = container;
            _plainClassFactory = plainClassFactory;
        }
        
        public T Create<T>() where T : class, IState 
        {
            return _plainClassFactory.Create<T>();
        }
        
        public object Create(Type type)
        {
            return _plainClassFactory.Create(type);
        }
    }
}