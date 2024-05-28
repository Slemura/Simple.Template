namespace RpDev.Level.Level
{
    public class LevelFromStartGetStrategy : LevelGetStrategy
    {
        public override int GetLevelIndex(int lastLevelIndex, int totalPassedLevels, int levelsCount)
        {
            var nextLevelIndex = lastLevelIndex + 1;
            
            if(lastLevelIndex >= levelsCount)
                nextLevelIndex = 0;

            return nextLevelIndex;
        }
    }
}