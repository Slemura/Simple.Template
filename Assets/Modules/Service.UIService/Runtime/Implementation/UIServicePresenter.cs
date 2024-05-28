using UnityEngine;

namespace RpDev.Services.UI
{
    [DisallowMultipleComponent]
    public class UIServicePresenter : MonoBehaviour
    {
        [SerializeField] private Transform _screenRoot;
        public Transform ScreenRoot => _screenRoot;
    }
}