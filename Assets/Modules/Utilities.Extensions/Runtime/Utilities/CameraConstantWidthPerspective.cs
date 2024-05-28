using UnityEngine;

namespace RpDev.Utilities
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class CameraConstantWidthPerspective : MonoBehaviour
    {
        [SerializeField] private Vector2Int _referenceResolution = new Vector2Int(1440, 2560);
        [SerializeField] private float _objectRadius = 3f; // set in inspector
        [SerializeField] private Transform _target; // set in inspector

        private Camera _camera;
        private float _targetAspectRatio;
        private float _initialFOV;

        private void Awake()
        {
            _camera = GetComponent<Camera>();

            _initialFOV = _camera.fieldOfView;

            _targetAspectRatio = (float)_referenceResolution.x / _referenceResolution.y;
        }

        private void Start()
        {
            UpdateFieldOfView();
        }

        private void Update()
        {
            if (Application.isEditor)
                UpdateFieldOfView();
        }

        private void UpdateFieldOfView()
        {
            /*float aspectRatio = (float)Screen.width / Screen.height;
            float newFOV = _initialFOV * (_targetAspectRatio / aspectRatio);

            _camera.fieldOfView = newFOV;*/
            var aspectRatio = (float)Screen.width / Screen.height;

            if (aspectRatio > 0.6f) return;

            var dist = Vector3.Distance(_target.position, _camera.transform.position);
            var fov = Mathf.Asin(_objectRadius / dist) * Mathf.Rad2Deg * 2f;

            _camera.fieldOfView = fov;
        }
    }
}