namespace RpDev.Level.Level
{
    public abstract class LevelGetStrategy
    {
        public abstract int GetLevelIndex(int lastLevelIndex, int totalPassedLevels, int levelsCount);
    }
}