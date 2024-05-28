using JetBrains.Annotations;
using RpDev.Services.Preferences.Values;

namespace RpDev.Services
{
    [UsedImplicitly]
    public class GamePreferences
    {
        public readonly PrefBool IsSoundEnabled = new PrefBool("sound_enabled", true);
        public readonly PrefBool IsMusicEnabled = new PrefBool("music_enabled", true);

        public void ResetAll()
        {
            IsSoundEnabled.SetToDefault();
            IsMusicEnabled.SetToDefault();
        }
    }
}