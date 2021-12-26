using Character;
using UnityEngine;
using UnityEngine.Networking;

namespace Main
{
    class SolarSystemNetworkManager : NetworkManager
    {
        #region publicFields



        #endregion


        #region privateFields

        [SerializeField] private string _playerName;

        #endregion


        #region pulbicMethods

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
        {
            var spawnTransform = GetStartPosition();
            var player = Instantiate(playerPrefab, spawnTransform.position, spawnTransform.rotation);
            player.GetComponent<ShipController>().PlayerName = _playerName;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }

        #endregion


        #region privateMethods



        #endregion
    }
}
