using System;

namespace RpDev.Gameplay.Model
{
    public class GameplayModel
    {
        public event Action OnWinLevel;
        public event Action OnLoseLevel;
        
        public void WinLevel()
        {
            OnWinLevel?.Invoke();
        }

        public void LoseLevel()
        {
            OnLoseLevel?.Invoke();
        }
    }
}