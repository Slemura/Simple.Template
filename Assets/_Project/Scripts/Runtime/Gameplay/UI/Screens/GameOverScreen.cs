using System;
using RpDev.Runtime.UI;
using RpDev.Services.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RpDev.Runtime.Screens
{
    public class GameOverScreen : UIScreen
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _restartGameButton;
        
        [SerializeField] private TMP_Text _infoText;
        
        [SerializeField] private SimpleToggleButton _musicButton;
        [SerializeField] private SimpleToggleButton _soundButton;
        
        public event Action StartGameClicked;
        public event Action RestartGameClicked;
        
        public SimpleToggleButton MusicButton => _musicButton;
        public SimpleToggleButton SoundButton => _soundButton;

        public void SetupInfo(string info)
        {
            _infoText.text = info;
        }
        
        private void Start()
        {
            _startGameButton.onClick.AddListener(StartGame);
            _restartGameButton.onClick.AddListener(RestartGame);
        }

        private void StartGame()
        {
            StartGameClicked?.Invoke();
        }

        private void RestartGame()
        {
            RestartGameClicked?.Invoke();
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveListener(StartGame);
            _restartGameButton.onClick.RemoveListener(RestartGame);
        }
    }
}