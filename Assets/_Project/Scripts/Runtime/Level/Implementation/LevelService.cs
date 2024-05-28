using RpDev.Level.Data;
using RpDev.Level.Level;
using RpDev.UserData;

namespace RpDev.Level
{
    public class LevelService : ILevelService, ILevelInitService, ILevelServiceInfo
    {
        private readonly UserDataHandler _userDataHandler;
        private readonly LevelGetStrategy _levelGetStrategy;
        
        private LevelInfo[] _levels;

        public LevelService(UserDataHandler userDataHandler)
        {
            _userDataHandler = userDataHandler;
            _levelGetStrategy = new LevelRandomGetStrategy();
        }

        public void InitLevels(LevelInfo[] levels)
        {
            _levels = levels;
        }
        
        public LevelInfo GetCurrentLevel()
        {
            return _levels[_userDataHandler.LastLevelIndex];
        }
        
        public void GoToNextLevel()
        {
            var nextLevelIndex = _levelGetStrategy.GetLevelIndex(_userDataHandler.LastLevelIndex,
                _userDataHandler.PassedLevels, _levels.Length);
            _userDataHandler.SetupLastLevelIndex(nextLevelIndex);
            _userDataHandler.IncrementPassedLevelIndex();
        }
    }
}