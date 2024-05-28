using System;

namespace RpDev.Services.AsyncStateMachine.Abstractions
{
    public interface IStateFactory
    {
        T Create<T>() where T : class, IState;

        object Create(Type type);
    }
}