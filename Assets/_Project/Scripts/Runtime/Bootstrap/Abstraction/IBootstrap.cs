using System.Threading;
using Cysharp.Threading.Tasks;

namespace RpDev.Bootstrap
{
    public interface IBootstrap
    {
        public UniTask Bootstrap(CancellationToken token);
    }
}
