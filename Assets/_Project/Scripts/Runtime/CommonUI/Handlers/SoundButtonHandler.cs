using System;
using RpDev.Services;
using RpDev.Runtime.UI;
using VContainer;

namespace RpDev.UI.Handlers
{
    public class SoundButtonHandler : IDisposable
    {
        private GamePreferences _preferences;
        private SimpleToggleButton _soundButton;
        
        [Inject]
        public void AddDependencies(GamePreferences preferences)
        {
            _preferences = preferences;
        }

        public void AddSoundButtonView(SimpleToggleButton musicButton)
        {
            _soundButton = musicButton;
            _soundButton.OnClicked += OnSoundButtonClicked;
            
            _soundButton.UpdateVisuals(_preferences.IsSoundEnabled.Value);
        }

        private void OnSoundButtonClicked()
        {
            _preferences.IsSoundEnabled.Value = !_preferences.IsSoundEnabled.Value;
            _soundButton.UpdateVisuals(_preferences.IsSoundEnabled.Value);
        }

        public void Dispose()
        {
            _soundButton.OnClicked -= OnSoundButtonClicked;
        }
    }
}