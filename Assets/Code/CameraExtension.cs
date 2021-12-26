using UnityEngine;

namespace Extensions
{
    public static class CameraExtension
    {
        #region pulbicMethods

        public static bool Visible(this Camera camera, Collider collider)
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, collider.bounds);
        }

        #endregion
    }
}
