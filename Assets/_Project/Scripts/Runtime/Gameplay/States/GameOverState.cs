using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.Gameplay.States.Payload;
using RpDev.Gameplay.UI;
using RpDev.RootStateHandler;
using RpDev.Runtime.Screens;
using RpDev.UserData;
using RpDev.Services.UI;
using RpDev.Services.AsyncStateMachine.Abstractions;
using RpDev.Services.UI.Mediators;

namespace RpDev.Gameplay.States
{
    public class GameOverState : StateBase<GameOverStatePayload>
    {
        private readonly IUIService _iuiService;
        private readonly IRootStatesHandler _rootStatesHandler;
        private readonly UserDataHandler _userDataHandler;
        private readonly UIMediatorFactory _uiMediatorFactory;
        private GameOverScreen _gameOverScreen;
        private GameOverStatePayload _payload;
        private GameOverScreenMediator _gameOverScreenMediator;

        public GameOverState(IUIService iuiService,
            IRootStatesHandler rootStatesHandler,
            UserDataHandler userDataHandler,
            UIMediatorFactory uiMediatorFactory)
        {
            _iuiService = iuiService;
            _rootStatesHandler = rootStatesHandler;
            _userDataHandler = userDataHandler;
            _uiMediatorFactory = uiMediatorFactory;
        }

        public override async UniTask OnEnter(GameOverStatePayload payload, CancellationToken cancellationToken)
        {
            _payload = payload;
            await ShowGameOverScreen();
            AddScreenHandlers();
        }

        public override async UniTask OnExit(CancellationToken cancellationToken)
        {
            await _gameOverScreen.FadeOutAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _gameOverScreen.StartGameClicked -= GoToGameState;
            _gameOverScreen.RestartGameClicked -= RestartGame;
            
            _gameOverScreenMediator.Dispose();
            _iuiService.DestroyScreen(_gameOverScreen);
        }

        private async UniTask ShowGameOverScreen()
        {
            _gameOverScreen = await _iuiService.SpawnScreen<GameOverScreen>();
            _gameOverScreenMediator = _uiMediatorFactory.Create<GameOverScreenMediator, GameOverScreen>(_gameOverScreen);
            _gameOverScreenMediator.Initialize();
            _gameOverScreen.SetupInfo("Level data: " + _payload.LevelData.LevelId + "\n Level index: " + _userDataHandler.PassedLevels);
            
            await _gameOverScreen.FadeInAsync();
        }

        private void AddScreenHandlers()
        {
            _gameOverScreen.StartGameClicked += GoToGameState;
            _gameOverScreen.RestartGameClicked += RestartGame;
        }

        private void GoToGameState()
        {
            TryToShowRewardedAndGoToGame().Forget();
        }

        private async UniTask TryToShowRewardedAndGoToGame()
        {
            _rootStatesHandler.GoToGameplayState(new GameplayStatePayload(_payload.LevelData, GameplayStatePayload.TransitionSource.Continue));
        }

        private void RestartGame()
        {
            _rootStatesHandler.GoToGameplayState(new GameplayStatePayload(_payload.LevelData, GameplayStatePayload.TransitionSource.Restart));
        }
    }
}