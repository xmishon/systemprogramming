using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace lessonOne
{
    public class Unit : MonoBehaviour
    {
        #region privateFields

        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth = 5;

        Coroutine recoverCoroutine;

        #endregion


        #region publicMethods

        public void ReceiveHealing()
        {
            if (recoverCoroutine != null)
                StopCoroutine(recoverCoroutine);
            recoverCoroutine = StartCoroutine(Recover());
        }

        #endregion


        #region privateMethods

        private async void Task1(CancellationToken ct)
        {
            if (ct.IsCancellationRequested)
            {
                Debug.Log("Task1 have been stopped by token");
                return;
            }
            await Task.Delay(1000);
            Debug.Log("Task1 finished");
            return;
        }

        private async void Task2(CancellationToken ct)
        {
            for (int i = 0; i < 60; i++)
            {
                if (ct.IsCancellationRequested)
                {
                    Debug.Log("Task2 have been stopped by token");
                    return;
                }
                await Task.Yield();
            }
            Debug.Log("Task2 finished");
        }

        private IEnumerator Recover()
        {
            for (int i = 0; i < 6; i++)
            {
                if (currentHealth <= maxHealth)
                {
                    currentHealth += 5;
                    if (currentHealth >= maxHealth)
                    {
                        currentHealth = maxHealth;
                    }
                    else
                    {
                        yield return new WaitForSeconds(0.5f);
                    }
                }    
            }
        }

        #endregion


        #region UnityMethods

        private void Start()
        {
            ReceiveHealing();

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Task task1 = new Task(() => Task1(cancellationToken));
            Task task2 = new Task(() => Task2(cancellationToken));
            task1.Start();
            task2.Start();
            //cancellationTokenSource.Cancel();

            cancellationTokenSource.Dispose();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit, 100.0f))
                {
                    Debug.Log("Raycast hit name: " + hit.transform.name);
                    ReceiveHealing();
                }
            }
        }

        #endregion
    }
}
