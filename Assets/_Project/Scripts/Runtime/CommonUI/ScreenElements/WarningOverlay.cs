using DG.Tweening;
using UnityEngine;

namespace RpDev.Runtime.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class WarningOverlay : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _smoothing = 1;

        private CanvasGroup _canvasGroup;

        public void UpdateWarningLevel(float warningNormalized)
        {
            var targetValue = warningNormalized;
            targetValue =  Mathf.Clamp(targetValue, 0, 0.6f);
            targetValue /= 0.7f;
            targetValue = _curve.Evaluate(targetValue);
            
            _canvasGroup.DOKill();
            _canvasGroup.DOFade(targetValue, _smoothing).SetEase(_curve).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }
    }
}