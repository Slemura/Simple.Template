using DG.Tweening;
using TMPro;
using UnityEngine;

namespace RpDev.Runtime.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class GameplayTimeCounterView : MonoBehaviour
    {
        private TMP_Text _textComponent;
        private int _seconds;

        public void SetupLevelInfo(string levelInfo)
        {
            _textComponent.text = levelInfo;
        }

        private void Awake()
        {
            _textComponent = GetComponent<TMP_Text>();
        }
    }
}