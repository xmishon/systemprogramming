using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Lesson4
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class Character : NetworkBehaviour
    {
        #region protectedVariables
        
        protected Action OnUpdateAction { get; set; }
        protected abstract FireAction fireAction { get; set; }
        [SyncVar] protected Vector3 serverPosition;
        [SyncVar] protected Quaternion serverRotation;

        #endregion


        #region protectedMethods

        protected virtual void Initiate()
        {
            OnUpdateAction += Movement;
        }

        [Command]
        protected void CmdUpdateTransform(Vector3 position, Quaternion rotation)
        {
            serverPosition = position;
            serverRotation = rotation;
        }

        #endregion


        #region privateMethods

        private void Update()
        {
            OnUpdate();
        }

        private void OnUpdate()
        {
            OnUpdateAction?.Invoke();
        }

        #endregion


        #region publicMethods

        public abstract void Movement();

        #endregion
    }
}