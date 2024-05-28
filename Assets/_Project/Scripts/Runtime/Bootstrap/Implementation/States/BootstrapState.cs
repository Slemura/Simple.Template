using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.RootStateHandler;
using RpDev.Services.AssetProvider.Abstractions;
using RpDev.Services.AsyncStateMachine.Abstractions;
using RpDev.Services.AudioService;

namespace RpDev.Bootstrap.States
{
    public class BootstrapState : StateBase
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IRootStatesHandler _rootStatesHandler;
        private readonly IEnumerable<IBootstrap> _bootstraps;
        private readonly GameAudioHandler _gameAudioHandler;
        private readonly CancellationTokenSource _bootstrapCts = new CancellationTokenSource();

        public BootstrapState(IAssetProvider assetProvider,
            IRootStatesHandler rootStatesHandler,
            IEnumerable<IBootstrap> bootstraps,
            GameAudioHandler gameAudioHandler)
        {
            _assetProvider = assetProvider;
            _rootStatesHandler = rootStatesHandler;
            _bootstraps = bootstraps;
            _gameAudioHandler = gameAudioHandler;
        }

        public override async UniTask OnEnter(CancellationToken cancellationToken)
        {
            await LoadAudioPacks();
            await LoadAllBootstraps();
            
            _rootStatesHandler.GoToMainMenuState();
        }

        public override UniTask OnExit(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public override void Dispose()
        {
            
        }

        private async UniTask LoadAllBootstraps()
        {
            await UniTask.WhenAll(_bootstraps.Select(bootstrap => bootstrap.Bootstrap(_bootstrapCts.Token)));
        }

        private async UniTask LoadAudioPacks()
        {
            var audioPackLibrary = await _assetProvider.LoadAsset<AudioPackLibrary>("AudioPackLibrary", _bootstrapCts.Token ); 
            _gameAudioHandler.AddAudioLibrary(audioPackLibrary);
        }
    }
}