using System;
using RpDev.Runtime.UI;
using RpDev.Services.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RpDev.Runtime.Screens
{
    public class GamePlayScreen : UIScreen
    {
        [SerializeField] private GameplayTimeCounterView _gameplayTimeCounterView;
        [SerializeField] private SimpleToggleButton _musicButton;
        [SerializeField] private SimpleToggleButton _soundButton;
        [SerializeField] private Button _winButton;
        [SerializeField] private Button _looseButton;
        
        public GameplayTimeCounterView LevelTimeCounterView => _gameplayTimeCounterView;

        public SimpleToggleButton MusicButton => _musicButton;
        public SimpleToggleButton SoundButton => _soundButton;
        
        public event Action WinButtonClicked;
        public event Action LooseButtonClicked;

        private void Start()
        {
            _winButton.onClick.AddListener(OnWinButtonClicked);
            _looseButton.onClick.AddListener(OnLooseButtonClicked);
        }

        private void OnLooseButtonClicked()
        {
            LooseButtonClicked?.Invoke();
        }

        private void OnWinButtonClicked()
        {
            WinButtonClicked?.Invoke();
        }

        private void OnDestroy()
        {
            _winButton.onClick.RemoveListener(OnWinButtonClicked);
            _looseButton.onClick.RemoveListener(OnLooseButtonClicked);
        }
    }
}