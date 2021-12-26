using Data;
using UnityEngine;

namespace Main
{
    class SettingsContainer : Singleton<SettingsContainer>
    {
        #region publicFields

        public SpaceShipSettings SpaceShipSettings => _spaceShipSettings;

        #endregion


        #region privateFields

        [SerializeField] private SpaceShipSettings _spaceShipSettings;

        #endregion


        #region pulbicMethods



        #endregion


        #region privateMethods



        #endregion
    }
}
