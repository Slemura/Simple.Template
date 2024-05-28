using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace RpDev.Services.AsyncStateMachine.Abstractions
{
    public interface IThruState : IDisposable
    {
        public UniTask Thru(CancellationToken token);
    }
}