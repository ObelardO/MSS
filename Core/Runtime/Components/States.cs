using UnityEngine;

namespace Obel.MSS
{
    [AddComponentMenu("MSS/States"), DisallowMultipleComponent]
    public class States : MonoBehaviour
    {
        #region Properties

        [SerializeReference]
        public StatesGroup Group;

        #endregion

        #region Unity methods

        private void Reset() => Group?.Clear();

        private void OnEnable() { }

        private void OnDisable() { }

        #endregion
    }
}