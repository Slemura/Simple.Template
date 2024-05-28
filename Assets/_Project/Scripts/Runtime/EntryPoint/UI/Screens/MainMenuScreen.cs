using System;
using RpDev.Runtime.UI;
using RpDev.Services.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RpDev.EntryPoint.UI
{
    public class MainMenuScreen : UIScreen
    {
        [SerializeField] private Button _startGameButton;
        
        [SerializeField] private SimpleToggleButton _musicButton;
        [SerializeField] private SimpleToggleButton _soundButton;
        
        public event Action OnStartGameClicked;

        public SimpleToggleButton MusicButton => _musicButton;
        public SimpleToggleButton SoundButton => _soundButton;

        protected override void Awake()
        {
            base.Awake();
            _startGameButton.onClick.AddListener(StartGameClick);
        }

        private void StartGameClick()
        {
            OnStartGameClicked?.Invoke();
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveListener(StartGameClick);
        }
    }
}