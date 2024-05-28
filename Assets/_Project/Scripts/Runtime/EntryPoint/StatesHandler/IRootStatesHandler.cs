using RpDev.Gameplay.States.Payload;

namespace RpDev.RootStateHandler
{
    public interface IRootStatesHandler
    {
        void GoToMainMenuState();
        void GoToGameplayState(GameplayStatePayload gameplayStatePayload);
        void GoToGameOverState(GameOverStatePayload gameOverStatePayload);
        void GoToGameWinState(GameplayStatePayload gameplayStatePayload);
    }
}