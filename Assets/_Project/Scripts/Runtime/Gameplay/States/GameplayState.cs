using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.Gameplay.Model;
using RpDev.Gameplay.States.Payload;
using RpDev.Gameplay.UI;
using RpDev.Level;
using RpDev.RootStateHandler;
using RpDev.UserData;
using RpDev.Runtime.Screens;
using RpDev.Services.UI;
using RpDev.Services.UI.Mediators;
using RpDev.Services.AsyncStateMachine.Abstractions;

namespace RpDev.Gameplay.States
{
    public class GameplayState : StateBase<GameplayStatePayload>
    {
        private readonly IUIService _iuiService;
        private readonly IRootStatesHandler _rootStatesHandler;
        private readonly UserDataHandler _userDataHandler;
        private readonly ILevelService _levelService;
        private readonly UIMediatorFactory _uiMediatorFactory;
        private readonly GameplayModel _gameplayModel;
        private readonly Stack<IDisposable> _disposables = new();

        private GamePlayScreen _gameplayScreen;
        private GamePlayScreenMediator _gameplayScreenMediator;
        private GameplayStatePayload _payload;

        public GameplayState(IUIService iuiService,
            IRootStatesHandler rootStatesHandler,
            UserDataHandler userDataHandler,
            ILevelService levelService,
            UIMediatorFactory uiMediatorFactory,
            GameplayModel gameplayModel)
        {
            _iuiService = iuiService;
            _rootStatesHandler = rootStatesHandler;
            _userDataHandler = userDataHandler;
            _levelService = levelService;
            _uiMediatorFactory = uiMediatorFactory;
            _gameplayModel = gameplayModel;
        }

        public override async UniTask OnEnter(GameplayStatePayload payload, CancellationToken cancellationToken)
        {
            _payload = payload;
            AddListeners();
            await ShowGameplayScreen();
        }

        public override async UniTask OnExit(CancellationToken cancellationToken)
        {
            await _gameplayScreen.FadeOutAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _gameplayModel.OnLoseLevel -= OnLevelLose;
            _gameplayModel.OnWinLevel -= OnLevelWin;

            while (_disposables.Count > 0)
                _disposables.Pop()?.Dispose();

            _iuiService.DestroyScreen(_gameplayScreen);

            _gameplayScreenMediator = null;
            _payload = null;
        }

        private void AddListeners()
        {
            _gameplayModel.OnLoseLevel += OnLevelLose;
            _gameplayModel.OnWinLevel += OnLevelWin;
        }

        private void OnLevelWin()
        {
            _levelService.GoToNextLevel();
            _rootStatesHandler.GoToGameWinState(_payload);
        }

        private void OnLevelLose()
        {
            _rootStatesHandler.GoToGameOverState(new GameOverStatePayload(_payload.LevelInfo));
        }

        private async UniTask ShowGameplayScreen()
        {
            _gameplayScreen = await _iuiService.SpawnScreen<GamePlayScreen>();
            _gameplayScreenMediator =
                _uiMediatorFactory.Create<GamePlayScreenMediator, GamePlayScreen>(_gameplayScreen);
            _gameplayScreenMediator.Initialize();
            _gameplayScreenMediator.SetupLevelInfo("Level data: " + _payload.LevelInfo.LevelId + "\n Level index: " + _userDataHandler.PassedLevels);

            _disposables.Push(_gameplayScreenMediator);

            await _gameplayScreen.FadeInAsync();
        }
    }
}