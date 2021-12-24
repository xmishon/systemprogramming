using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Lesson4
{
    public abstract class FireAction : MonoBehaviour
    {
        #region publicFields

        public string countBullet = string.Empty;

        #endregion


        #region protectedFields

        protected Queue<GameObject> bullets = new Queue<GameObject>();
        protected Queue<GameObject> ammunition = new Queue<GameObject>();
        protected bool reloading = false;

        #endregion


        #region privateFields

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private int startAmmunition = 20;

        #endregion


        #region publicMethods

        public virtual async void Reloading()
        {
            bullets = await Reload();
        }

        #endregion


        #region protectedMethods

        protected virtual void Start()
        {
            for (var i = 0; i < startAmmunition; i++)
            {
                GameObject bullet;
                if (bulletPrefab == null)
                {
                    bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    bullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                }
                else
                {
                    bullet = Instantiate(bulletPrefab);
                }
                bullet.SetActive(false);
                ammunition.Enqueue(bullet);
            }
        }

        protected virtual void Shooting()
        {
            if (bullets.Count == 0)
            {
                Reloading();
            }
        }

        #endregion

        #region privateMethods

        private async Task<Queue<GameObject>> Reload()
        {
            if (!reloading)
            {
                reloading = true;
                StartCoroutine(ReloadingAnim());
                return await Task.Run(delegate {
                    var cage = 10;
                    if (bullets.Count < cage)
                    {
                        Thread.Sleep(3000);
                        var bullets = this.bullets;
                        while (bullets.Count > 0)
                        {
                            ammunition.Enqueue(bullets.Dequeue());
                        }
                        cage = Mathf.Min(cage, ammunition.Count);
                        if (cage > 0)
                        {
                            for (int i = 0; i < cage; i++)
                            {
                                var sphere = ammunition.Dequeue();
                                bullets.Enqueue(sphere);
                            }
                        }
                    }
                    reloading = false;
                    return bullets; 
                });
            }
            else
            {
                return bullets;
            }
        }

        private IEnumerator ReloadingAnim()
        {
            while (reloading)
            {
                countBullet = " | ";
                yield return new WaitForSeconds(0.01f);
                countBullet = @" \ ";
                yield return new WaitForSeconds(0.01f);
                countBullet = "---";
                yield return new WaitForSeconds(0.01f);
                countBullet = " / ";
                yield return new WaitForSeconds(0.01f);
            }
            countBullet = bullets.Count.ToString();
            yield return null;
        }

        #endregion
    }
}
