using RpDev.Services.Persistence.PersistenceHandlers;
using VContainer.Unity;

namespace RpDev.UserData
{
    public class UserDataHandler : IStartable
    {
        private const string FileName = "user_data";

        private readonly UserData _userData = new UserData();
        private readonly PersistenceHandler<UserData> _persistenceHandler;

        public int LastLevelIndex => _userData.LastLevelIndex;
        public int PassedLevels => _userData.PassedLevels;

        public UserDataHandler()
        {
            _persistenceHandler = new PlayerPrefsPersistenceHandler<UserData>(FileName);
        }

        public void Start()
        {
            _persistenceHandler.TryLoadInto(_userData);
        }

        public void Reset()
        {
            _userData.LastLevelIndex = default;
            _persistenceHandler.Save(_userData);
        }

        public void IncrementPassedLevelIndex()
        {
            _userData.PassedLevels++;
            _persistenceHandler.Save(_userData);
        }
        
        public void SetupLastLevelIndex(int index)
        {
            _userData.LastLevelIndex = index;
            _persistenceHandler.Save(_userData);
        }
    }
}