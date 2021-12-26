using System;
using UnityEngine;
using UnityEngine.Networking;
using Unity;

namespace Network
{
#pragma warning disable 618
    public abstract class NetworkMovableObject : NetworkBehaviour
#pragma warning restore 618
    {
        #region protectedFields

        protected abstract float _speed { get; }
        protected Action _onUpdateAction { get; set; }
        protected Action _onFixedUpdateAction { get; set; }
        protected Action _onLateUpdateAction { get; set; }
        protected Action _onPreRenderAction { get; set; }
        protected Action _onPostRenderAction { get; set; }

#pragma warning disable 618

        [SyncVar] protected Vector3 _serverPosition;
        [SyncVar] protected Vector3 _serverEuler;

#pragma warning restore 618


        #endregion


        #region privateFields



        #endregion


        #region pulbicMethods

        public override void OnStartAuthority()
        {
            Initiate();
        }

        #endregion


        #region protectedMethods

        protected virtual void Initiate(UpdatePhase updatePhase = UpdatePhase.Update)
        {
            switch (updatePhase)
            {
                case UpdatePhase.Update:
                    _onUpdateAction += Movement;
                    break;
                case UpdatePhase.FixedUpdate:
                    _onFixedUpdateAction += Movement;
                    break;
                case UpdatePhase.LateUpdate:
                    _onLateUpdateAction += Movement;
                    break;
                case UpdatePhase.PostRender:
                    _onPostRenderAction += Movement;
                    break;
                case UpdatePhase.PreRender:
                    _onPreRenderAction += Movement;
                    break;
            }
        }

        protected virtual void Movement()
        {
            if (hasAuthority)
            {
                HasAuthorityMovement();
            }
            else
            {
                FromServerUpdate();
            }
        }

        protected abstract void HasAuthorityMovement();

        protected abstract void FromServerUpdate();

        protected abstract void SendToServer();

        #endregion


        #region privateMethods

        private void Update()
        {
            _onUpdateAction?.Invoke();
        }

        private void LateUpdate()
        {
            _onLateUpdateAction?.Invoke();
        }

        private void FixedUpdate()
        {
            _onFixedUpdateAction?.Invoke();
        }

        private void OnPreRender()
        {
            _onPreRenderAction?.Invoke();
        }

        private void OnPostRender()
        {
            _onPostRenderAction?.Invoke();
        }

        #endregion
    }
}
