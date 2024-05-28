using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.Bootstrap;
using RpDev.Level.Data;
using RpDev.Services.AssetProvider.Abstractions;

namespace RpDev.Level.Bootstrap
{
    public class LevelBootstrap : IBootstrap
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ILevelInitService _levelInitService;

        public LevelBootstrap(IAssetProvider assetProvider, ILevelInitService levelInitService)
        {
            _assetProvider = assetProvider;
            _levelInitService = levelInitService;
        }
        
        public async UniTask Bootstrap(CancellationToken token)
        {
            var levels = await _assetProvider.LoadAsset<LevelContainer>("Levels", default);
            
            _levelInitService.InitLevels(levels.Levels);
        }
    }
}
