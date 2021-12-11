using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace lessonOne
{
    public class LessonOne : MonoBehaviour
    {
        #region privateFields

        [SerializeField] GameObject prefabPopup;

        #endregion


        #region publicMethods

        public void TryBuyItem()
        {
            GameObject newPopup = Instantiate(prefabPopup);
            SomePopup popupScript = newPopup.GetComponent<SomePopup>();
            popupScript.OnClose += CompletePurhase;

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;
            popupScript.ActivatePopup(ct);
        }

        #endregion


        #region privateMethods

        private void CompletePurhase(bool completed)
        {
            if (completed)
                Debug.Log("Purchase made");
            else
                Debug.Log("Purchase declined");
        }

        #endregion


        #region UnityMethods

        private void Start()
        {
            TryBuyItem();
        }

        #endregion
    }
}