using UnityEngine;

namespace Obel.MSS
{
    public enum DefaultState { Closed, Opened }

    public class States : MonoBehaviour
    {
        #region Properties

        public StatesGroup statesGroup;

        #endregion

        #region Unity methods

        private void Reset() { statesGroup = null; }

        private void OnEnable() { }

        private void OnDisable() { }

        #endregion
    }
}