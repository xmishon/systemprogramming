using UnityEngine;
using UnityEngine.Networking;


namespace Lesson4
{
    public class Player : NetworkBehaviour
    {
        #region privateVariables

        [SerializeField] private GameObject playerPrefab;
        private GameObject playerCharacter;

        #endregion


        #region privateMethods

        private void Start()
        {
            SpawnCharacter();
        }

        private void SpawnCharacter()
        {
            if (!isServer)
            {
                return;
            }

            playerCharacter = Instantiate(playerPrefab);
            NetworkServer.SpawnWithClientAuthority(playerCharacter, connectionToClient);
        }

        #endregion
    }
}
