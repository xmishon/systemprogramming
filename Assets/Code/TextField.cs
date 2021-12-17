using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace LessonThree
{
    public class TextField : MonoBehaviour
    {
        #region privateFields

        [SerializeField] private TextMeshProUGUI textObject;
        [SerializeField] private Scrollbar scrollbar;

        private List<string> messages = new List<string>();

        #endregion

        #region publicMethods

        public void ReceiveMessage(object message)
        {
            messages.Add(message.ToString());
            float value = (messages.Count - 1) * scrollbar.value;
            scrollbar.value = Mathf.Clamp(value, 0, 1);
            UpdateText();
        }

        #endregion


        #region privateMethods

        private void Start()
        {
            scrollbar.onValueChanged.AddListener((float value) => UpdateText());
        }

        private void UpdateText()
        {
            string text = "";
            int index = (int)(messages.Count * scrollbar.value);
            for (int i = index; i < messages.Count; i++)
            {
                text += messages[i] + "\n";
            }
            textObject.text = text;
        }

        #endregion
    }

}
