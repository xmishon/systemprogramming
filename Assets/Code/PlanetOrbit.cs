﻿using Network;
using UnityEngine;

namespace Mechanics
{
    class PlanetOrbit : NetworkMovableObject
    {
        #region protectedFields

        protected override float _speed => _smoothTime;

        #endregion


        #region privateFields

        [SerializeField] private Transform _aroundPoint;
        [SerializeField] private float _smoothTime = 0.3f;
        [SerializeField] private float _circleInSecond = 1.0f;

        [SerializeField] private float _offsetSin = 1.0f;
        [SerializeField] private float _offsetCos = 1.0f;
        [SerializeField] private float _rotationSpeed;

        private float _dist;
        private float _currentAng;
        private Vector3 _currentPositionSmoothVelocity;
        private float _currentRotationAngle;
        private const float _circleRadians = Mathf.PI * 2;

        #endregion


        #region protectedMethods

        protected override void SendToServer()
        {
            _serverPosition = transform.position;
            _serverEuler = transform.eulerAngles;
        }

        protected override void FromServerUpdate()
        {
            if (!isClient)
            {
                return;
            }
            transform.position = Vector3.SmoothDamp(transform.position, _serverPosition, ref _currentPositionSmoothVelocity, _speed);
            transform.rotation = Quaternion.Euler(_serverEuler);
        }

        #endregion


        #region privateMethods

        private void Start()
        {
            if (isServer)
            {
                _dist = (transform.position - _aroundPoint.position).magnitude;
            }
            Initiate(UpdatePhase.FixedUpdate);
        }

        protected override void HasAuthorityMovement()
        {
            if (!isServer)
            {
                return;
            }

            var p = _aroundPoint.position;
            p.x += Mathf.Sin(_currentAng) * _dist * _offsetSin;
            p.z += Mathf.Cos(_currentAng) * _dist * _offsetCos;
            transform.position = p;
            _currentRotationAngle += Time.deltaTime * _rotationSpeed;
            _currentRotationAngle = Mathf.Clamp(_currentRotationAngle, 0, 361); // Magic numbers
            if (_currentRotationAngle >= 360) // Magic numbers
            {
                _currentRotationAngle = 0;
            }
            transform.rotation = Quaternion.AngleAxis(_currentRotationAngle, transform.up);
            _currentAng += _circleRadians * _circleInSecond * Time.deltaTime;

            SendToServer();
        }

        #endregion
    }
}
