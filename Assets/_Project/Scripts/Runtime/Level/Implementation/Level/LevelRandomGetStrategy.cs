using UnityEngine;

namespace RpDev.Level.Level
{
    public class LevelRandomGetStrategy : LevelGetStrategy
    {
        public override int GetLevelIndex(int lastLevelIndex, int totalPassedLevels, int levelsCount)
        {
            var nextLevel = totalPassedLevels + 1;

            if (nextLevel < levelsCount) return nextLevel;
            
            do
            {
                nextLevel = Random.Range(0, levelsCount - 1);
            } 
            while (nextLevel == lastLevelIndex);

            return nextLevel;
        }
    }
}