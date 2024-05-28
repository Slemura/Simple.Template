using UnityEngine;

namespace RpDev.Services.AudioService
{
    public class AudioServicePresenter : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}