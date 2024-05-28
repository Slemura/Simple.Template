using System;
using RpDev.Services;
using RpDev.Runtime.UI;
using VContainer;

namespace RpDev.UI.Handlers
{
    public class MusicButtonHandler : IDisposable
    {
        private GamePreferences _preferences;
        private SimpleToggleButton _musicButton;
        
        [Inject]
        public void AddDependencies(GamePreferences preferences)
        {
            _preferences = preferences;
        }

        public void AddMusicButtonView(SimpleToggleButton musicButton)
        {
            _musicButton = musicButton;
            _musicButton.OnClicked += OnMusicButtonClicked;
            
            _musicButton.UpdateVisuals(_preferences.IsMusicEnabled.Value);
        }

        private void OnMusicButtonClicked()
        {
            _preferences.IsMusicEnabled.Value = !_preferences.IsMusicEnabled.Value;
            _musicButton.UpdateVisuals(_preferences.IsMusicEnabled.Value);
        }

        public void Dispose()
        {
            _musicButton.OnClicked -= OnMusicButtonClicked;
        }
    }
}