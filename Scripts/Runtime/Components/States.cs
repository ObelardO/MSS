using UnityEngine;

namespace Obel.MSS
{
    public enum DefaultState { Closed, Opened }

    [DisallowMultipleComponent, AddComponentMenu("MSS/States")]
    #if UNITY_2018_3_OR_NEWER
        [ExecuteAlways]
    #else
        [ExecuteInEditMode]
    #endif
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