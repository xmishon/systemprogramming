using System.Collections;
using System.Collections.Generic;
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
