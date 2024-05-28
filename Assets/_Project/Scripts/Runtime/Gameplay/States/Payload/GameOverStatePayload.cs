using RpDev.Level.Data;

namespace RpDev.Gameplay.States.Payload
{
    public struct GameOverStatePayload
    {
        public LevelInfo LevelData { get; }

        public GameOverStatePayload(LevelInfo levelData)
        {
            LevelData = levelData;
        }
    }
}
