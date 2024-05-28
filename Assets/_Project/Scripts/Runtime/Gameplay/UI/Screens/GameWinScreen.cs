using System;
using RpDev.Runtime.UI;
using RpDev.Services.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RpDev.Gameplay.UI
{
    public class GameWinScreen : UIScreen
    {
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _nextLevelButton;

        [SerializeField] private TMP_Text _infoText;

        [SerializeField] private SimpleToggleButton _musicButton;
        [SerializeField] private SimpleToggleButton _soundButton;
        
        public event Action OnMainMenuButtonClicked;
        public event Action OnNextLevelButtonClicked;

        public SimpleToggleButton MusicButton => _musicButton;
        public SimpleToggleButton SoundButton => _soundButton;

        public void SetupInfo(string info)
        {
            _infoText.text = info;
        } 
        
        private void Start()
        {
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
            _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
        }

        private void OnNextLevelButtonClick()
        {
            OnNextLevelButtonClicked?.Invoke();
        }

        private void OnMainMenuButtonClick()
        {
            OnMainMenuButtonClicked?.Invoke();
        }
    }
}
