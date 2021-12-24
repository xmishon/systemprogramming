﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Lesson4
{
    public class RayShooter : FireAction
    {
        #region protectedFields

        

        #endregion


        #region privateFields

        private Camera camera;

        #endregion


        #region publicMethods



        #endregion


        #region protectedMethods

        protected override void Start()
        {
            base.Start();
            camera = GetComponentInChildren<Camera>();
        }

        protected override void Shooting()
        {
            base.Shooting();
            if(bullets.Count > 0)
            {
                StartCoroutine(Shoot());
            }
        }

        #endregion

        #region privateMethods

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shooting();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reloading();
            }
            if (Input.anyKey && !Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        private IEnumerator Shoot()
        {
            if (reloading)
            {
                yield break;
            }
            var point = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0);
            var ray = camera.ScreenPointToRay(point);
            if (!Physics.Raycast(ray, out var hit))
            {
                yield break;
            }

            var shoot = bullets.Dequeue();
            countBullet = bullets.Count.ToString();
            ammunition.Enqueue(shoot);
            shoot.SetActive(true);
            shoot.transform.position = hit.point;
            shoot.transform.parent = hit.transform;
            yield return new WaitForSeconds(2.0f);
            shoot.SetActive(false);
        }

        #endregion
    }
}
