using UnityEngine;

namespace Obel.MSS
{
    [AddComponentMenu("MSS/States")]
    public class States : MonoBehaviour
    {
        #region Properties

        public StatesGroup statesGroup;

        #endregion

        #region Unity methods

        private void Reset() => statesGroup = null;

        private void OnEnable() { }

        private void OnDisable() { }

        #endregion
    }
}