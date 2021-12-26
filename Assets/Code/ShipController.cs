using UnityEngine;
using UnityEngine.Networking;

namespace Character
{
    public class ShipController
    {
        #region publicFields

        public string PlayerName
        {
            get => _playerName;
            set => _playerName = value;
        }

        #endregion


        #region privateFields

        [SyncVar] private string _playerName;

        #endregion


        #region pulbicMethods



        #endregion


        #region privateMethods



        #endregion
    }
}
