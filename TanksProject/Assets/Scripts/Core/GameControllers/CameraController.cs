using Tanks.General.Controllers;
using UnityEngine;

namespace Tanks.Core.GameControllers
{
    internal sealed class CameraController : ICameraController, IStart, ILateUpdate
    {
        private Transform _cameraTransform;
        private float _rotationAngleX;
        private float _distance;
        private Transform _target;

        public void Start()
        {
            _rotationAngleX = 60f;
            _distance = 35f;
        }

        public void LateUpdate()
        {
            if (_target == null)
                return;
            Quaternion rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -_distance) + _target.position;
            _cameraTransform.rotation = rotation;
            _cameraTransform.position = position;
        }

        public void Initialize(Camera camera)
        {
            _cameraTransform = camera.transform;
        }

        public void SetTarget(Transform target) => _target = target;
        
    }
}