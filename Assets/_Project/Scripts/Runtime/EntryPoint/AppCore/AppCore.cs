using Cysharp.Threading.Tasks;
using RpDev.Bootstrap.States;
using RpDev.EntryPoint.States;
using RpDev.Gameplay.States;
using RpDev.Gameplay.States.Payload;
using RpDev.RootStateHandler;
using RpDev.Services.AsyncStateMachine;
using UnityEngine;
using VContainer.Unity;

namespace RpDev.EntryPoint
{
    public class AppCore : IInitializable, IRootStatesHandler
    {
        private readonly StateMachine<GameRootStateTrigger> _stateMachine;

        public AppCore(StateMachine<GameRootStateTrigger> stateMachine)
        {
            Application.targetFrameRate = 60;
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            DefineStateMachine();
            
            _stateMachine.Start(default).Forget();
        }
        
        public void GoToMainMenuState()
        {
            _stateMachine.Dispatch(GameRootStateTrigger.MainMenu, default).Forget();
        }
        
        public void GoToGameplayState(GameplayStatePayload gameplayStatePayload)
        {
            _stateMachine.Dispatch(GameRootStateTrigger.Gameplay, gameplayStatePayload, default).Forget();
        }
        
        public void GoToGameOverState(GameOverStatePayload gameOverStatePayload)
        {
            _stateMachine.Dispatch(GameRootStateTrigger.GameOver, gameOverStatePayload, default).Forget();
        }

        public void GoToGameWinState(GameplayStatePayload gameplayStatePayload)
        {
            _stateMachine.Dispatch(GameRootStateTrigger.GameWin, gameplayStatePayload, default).Forget();
        }

        private void DefineStateMachine()
        {
            _stateMachine.SetInitialState<BootstrapState>();

            _stateMachine.Configure<BootstrapState>()
                .AllowTransition<MainMenuState>(GameRootStateTrigger.MainMenu);
            
            _stateMachine.Configure<MainMenuState>()
                .AllowTransition<GameplayState>(GameRootStateTrigger.Gameplay);

            _stateMachine.Configure<GameplayState>()
                .AllowTransition<GameOverState>(GameRootStateTrigger.GameOver)
                .AllowTransition<GameWinState>(GameRootStateTrigger.GameWin);
            
            _stateMachine.Configure<GameOverState>()
                .AllowTransition<GameplayState>(GameRootStateTrigger.Gameplay);

            _stateMachine.Configure<GameWinState>()
                .AllowTransition<MainMenuState>(GameRootStateTrigger.MainMenu)
                .AllowTransition<GameplayState>(GameRootStateTrigger.Gameplay);
        }
    }
}
