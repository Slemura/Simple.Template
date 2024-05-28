using RpDev.Level.Data;

namespace RpDev.Gameplay.States.Payload
{
    public class GameplayStatePayload
    {
        public LevelInfo LevelInfo { get; }
        public TransitionSource Source { get; }

        public GameplayStatePayload(LevelInfo levelInfo, TransitionSource source)
        {
            LevelInfo = levelInfo;
            Source = source;
        }
        
        public enum TransitionSource
        {
            Continue,
            Restart,
            MainMenu,
            Win
        }
    }
}