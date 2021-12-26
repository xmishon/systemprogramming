﻿using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SpaceShipSettings", menuName = "Geekbrains/Settings/Space Ship Settings")]
    class SpaceShipSettings : ScriptableObject
    {
        #region publicFields

        public float Acceleration => _acceleration;
        public float ShipSpeed => _shipSpeed;
        public float Faster => _faster;
        public float NormalFov => _normalFov;
        public float FasterFov => _fasterFov;
        public float ChangeFovSpeed => _changeFovSpeed;


        #endregion


        #region privateFields

        [SerializeField, Range(0.01f, 0.1f)] private float _acceleration;
        [SerializeField, Range(1f, 2000f)] private float _shipSpeed;
        [SerializeField, Range(1f, 5f)] private float _faster;
        [SerializeField, Range(0.01f, 179f)] private float _normalFov = 60;
        [SerializeField, Range(0.01f, 179f)] private float _fasterFov = 30;
        [SerializeField, Range(0.1f, 5f)] private float _changeFovSpeed = 0.5f;

        #endregion


        #region pulbicMethods



        #endregion


        #region privateMethods



        #endregion
    }
}
