using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Lesson4
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerCharacter : Character
    {
        #region privateFields

        [Range(0, 100)] [SerializeField] private int health = 100;
        [Range(0.5f, 10.0f)] [SerializeField] private float movingSpeed = 8.0f;
        [SerializeField] private float acceleration = 3.0f;
        private const float gravity = -9.8f;
        private CharacterController characterController;
        private MouseLook mouseLook;
        private Vector3 currentVelocity;

        #endregion


        #region protectedVariables

        protected override FireAction fireAction { get; set; }

        #endregion


        #region protectedMethods

        protected override void Initiate()
        {
            base.Initiate();
            fireAction = gameObject.AddComponent<RayShooter>();
            fireAction.Reloading();
            characterController = GetComponentInChildren<CharacterController>();
            characterController ??= gameObject.AddComponent<CharacterController>();
            mouseLook = GetComponentInChildren<MouseLook>();
            mouseLook ??= gameObject.AddComponent<MouseLook>();
        }

        #endregion


        #region publicMethods

        public override void Movement()
        {
            // Отключение камеры на аватаре, владельцем которого не являемся
            if (mouseLook != null && mouseLook.PlayerCamera != null)
            {
                mouseLook.PlayerCamera.enabled = hasAuthority;
            }

            // Если являемся владельцем аватара - передаем управление пользователю
            if (hasAuthority)
            {
                var moveX = Input.GetAxis("Horizontal") * movingSpeed;
                var moveZ = Input.GetAxis("Vertical") * movingSpeed;
                var movement = new Vector3(moveX, 0, moveZ);
                movement = Vector3.ClampMagnitude(movement, movingSpeed);
                movement *= Time.deltaTime;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    movement *= acceleration;
                }

                movement.y = gravity;
                movement = transform.TransformDirection(movement);
                characterController.Move(movement);
                mouseLook.Rotation();

                CmdUpdateTransform(transform.position, transform.rotation);
            }
            // Иначе двигаем объект в зависимости от пришедшего с сервера значения serverPosition
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, serverPosition, ref currentVelocity, movingSpeed * Time.deltaTime);
                transform.rotation = serverRotation;
            }
        }

        #endregion


        #region privateMethods

        private void Start()
        {
            Initiate();
        }

        private void OnGUI()
        {
            if (Camera.main == null)
            {
                return;
            }

            var info = $"Health: {health}\nClip: {fireAction.countBullet}";
            var size = 12;
            var bulletCountSize = 50;
            var posX = Camera.main.pixelWidth / 2 - size / 4;
            var posY = Camera.main.pixelWidth / 2 - size / 2;
            var posXBul = Camera.main.pixelWidth - bulletCountSize * 2;
            var posYBul = Camera.main.pixelHeight - bulletCountSize;
            GUI.Label(new Rect(posX, posY, size, size), "+");
            GUI.Label(new Rect(posXBul, posYBul, bulletCountSize * 2, bulletCountSize * 2), info);
        }

        #endregion
    }
}