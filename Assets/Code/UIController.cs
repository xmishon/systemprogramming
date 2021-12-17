using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LessonThree
{
    public class UIController : MonoBehaviour
    {
        #region privateFields

        [SerializeField] private Button buttonStartServer;
        [SerializeField] private Button buttonShutDownServer;
        [SerializeField] private Button buttonConnectClient;
        [SerializeField] private Button buttonDisconnectClient;
        [SerializeField] private Button buttonSendMessage;
        [SerializeField] private TMP_InputField inputField;

        [SerializeField] private TextField textField;
        [SerializeField] private Server server;
        [SerializeField] private Client client;

        #endregion


        #region privateMethods

        private void Start()
        {
            buttonStartServer.onClick.AddListener(() => StartServer());
            buttonShutDownServer.onClick.AddListener(() => ShutDownServer());
            buttonConnectClient.onClick.AddListener(() => Connect());
            buttonDisconnectClient.onClick.AddListener(() => Disconnect());
            buttonSendMessage.onClick.AddListener(() => SendMessage());
            client.onMessageReceive += ReceiveMessage;
        }

        private void StartServer()
        {
            server.StartServer();
        }

        private void ShutDownServer()
        {
            server.ShutDownServer();
        }

        private void Connect()
        {
            client.Connect();
        }

        private void Disconnect()
        {
            client.Disconnect();
        }

        private void SendMessage()
        {
            client.SendMessage(inputField.text);
            inputField.text = "";
        }

        #endregion


        #region publicMethods

        public void ReceiveMessage(object message)
        {
            textField.ReceiveMessage(message);
        }

        #endregion
    }
}