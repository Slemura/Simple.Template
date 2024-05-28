using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.EntryPoint.UI;
using RpDev.Gameplay.States.Payload;
using RpDev.Level;
using RpDev.RootStateHandler;
using RpDev.Runtime.Screens;
using RpDev.Services.UI;
using RpDev.Services.AsyncStateMachine.Abstractions;
using RpDev.Services.UI.Mediators;

namespace RpDev.EntryPoint.States
{
    public class MainMenuState : StateBase
    {
        private readonly IUIService _uiService;
        private readonly IRootStatesHandler _rootStatesHandler;
        private readonly ILevelServiceInfo _levelServiceInfo;
        private readonly UIMediatorFactory _uiMediatorFactory;
        private readonly LoadingScreen _loadingScreen;
        
        private MainMenuScreen _mainMenuScreen;
        private MainMenuScreenMediator _mainMenuScreenMediator;

        public MainMenuState(IUIService uiService,
            IRootStatesHandler rootStatesHandler,
            ILevelServiceInfo levelServiceInfo,
            UIMediatorFactory uiMediatorFactory,
            LoadingScreen loadingScreen)
        {
            _uiService = uiService;
            _rootStatesHandler = rootStatesHandler;
            _levelServiceInfo = levelServiceInfo;
            _uiMediatorFactory = uiMediatorFactory;
            _loadingScreen = loadingScreen;
        }
         
        public override async UniTask OnEnter(CancellationToken cancellationToken)
        {
            await InitMainMenu();
            AddScreenHandlers();
        }
        
        public override async UniTask OnExit(CancellationToken cancellationToken)
        {
            await _mainMenuScreen.FadeOutAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _mainMenuScreen.OnStartGameClicked -= GoToGameState;
            _uiService.DestroyScreen(_mainMenuScreen);
            _mainMenuScreenMediator.Dispose();
        }
        
        private async UniTask InitMainMenu()
        {
            _mainMenuScreen = await _uiService.SpawnScreen<MainMenuScreen>();
            _mainMenuScreenMediator = _uiMediatorFactory.Create<MainMenuScreenMediator, MainMenuScreen>(_mainMenuScreen);
            _mainMenuScreenMediator.Initialize();
            
            _mainMenuScreen.FadeInAsync().Forget();
            _loadingScreen.FadeOutAsync().Forget();
        }

        private void AddScreenHandlers()
        {
            _mainMenuScreen.OnStartGameClicked += GoToGameState;
        }

        private void GoToGameState()
        {
            _rootStatesHandler.GoToGameplayState(new GameplayStatePayload(_levelServiceInfo.GetCurrentLevel(), GameplayStatePayload.TransitionSource.MainMenu));
        }
    }
}
