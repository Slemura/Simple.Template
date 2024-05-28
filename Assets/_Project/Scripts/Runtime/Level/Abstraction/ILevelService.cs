using RpDev.Level.Data;

namespace RpDev.Level
{
    public interface ILevelInitService
    {
        void InitLevels(LevelInfo[] levels);
    }

    public interface ILevelServiceInfo
    {
        LevelInfo GetCurrentLevel();
    }

    public interface ILevelService
    {
        void GoToNextLevel();
    }
}