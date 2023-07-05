using Core.Models;
using UnityEngine;
using Zenject;

namespace Core
{
    [RequireComponent(typeof(Camera))]
    public class CameraView : MonoBehaviour
    {
        private Transform _target;
        private Vector3 _offset;
        private float _smoothSpeed;

        [Inject]
        private void Construct(GameSettings settings)
        {
            _offset = settings.OffsetCamera;
            _smoothSpeed = settings.SmoothSpeedCamera;
        }

        public void InitTarget(Transform target)
        {
            _target = target;
        }

        private void Update()
        {
            if (_target == null) return;

            Vector3 desiredPosition = _target.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}