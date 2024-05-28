using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.Gameplay.States.Payload;
using RpDev.Gameplay.UI;
using RpDev.Level;
using RpDev.RootStateHandler;
using RpDev.UserData;
using RpDev.Services.AsyncStateMachine.Abstractions;
using RpDev.Services.UI;
using RpDev.Services.UI.Mediators;

namespace RpDev.Gameplay.States
{
    public class GameWinState : StateBase<GameplayStatePayload>
    {
        private readonly Stack<IDisposable> _disposables = new();
        private readonly IRootStatesHandler _rootStatesHandler;
        private readonly IUIService _uiService;
        private readonly UserDataHandler _userDataHandler;
        private readonly ILevelServiceInfo _levelService;
        private readonly UIMediatorFactory _uiMediatorFactory;

        private GameWinScreen _winScreen;
        private GameWinScreenMediator _winScreenMediator;
        private GameplayStatePayload _payload;

        public GameWinState(IRootStatesHandler rootStatesHandler,
            IUIService uiService,
            UserDataHandler userDataHandler,
            ILevelServiceInfo levelService,
            UIMediatorFactory uiMediatorFactory)
        {
            _rootStatesHandler = rootStatesHandler;
            _uiService = uiService;
            _userDataHandler = userDataHandler;
            _levelService = levelService;
            _uiMediatorFactory = uiMediatorFactory;
        }

        public override async UniTask OnEnter(GameplayStatePayload payload, CancellationToken cancellationToken)
        {
            _payload = payload;
            await ShowScreen();
        }

        public override async UniTask OnExit(CancellationToken cancellationToken)
        {
            await _winScreen.FadeOutAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _winScreen.OnMainMenuButtonClicked -= OnMainMenuButtonClicked;
            _winScreen.OnNextLevelButtonClicked -= OnNextLevelButtonClicked;

            _uiService.DestroyScreen(_winScreen);
            _winScreenMediator.Dispose();
        }

        private async UniTask ShowScreen()
        {
            _winScreen = await _uiService.SpawnScreen<GameWinScreen>();
            _winScreen.OnMainMenuButtonClicked += OnMainMenuButtonClicked;
            _winScreen.OnNextLevelButtonClicked += OnNextLevelButtonClicked;

            _winScreenMediator = _uiMediatorFactory.Create<GameWinScreenMediator, GameWinScreen>(_winScreen);
            _winScreenMediator.Initialize();

            _winScreen.SetupInfo("Level data: " + _payload.LevelInfo.LevelId + "\n Level index: " + _userDataHandler.PassedLevels);
            await _winScreen.FadeInAsync();
        }

        private void OnNextLevelButtonClicked()
        {
            _rootStatesHandler.GoToGameplayState(new GameplayStatePayload(_levelService.GetCurrentLevel(),
                GameplayStatePayload.TransitionSource.Win));
        }

        private void OnMainMenuButtonClicked()
        {
            _rootStatesHandler.GoToMainMenuState();
        }
    }
}