using System;
using UnityEngine;
using UnityEngine.Events;

namespace Obel.MSS
{
    [AddComponentMenu("MSS/States"), DisallowMultipleComponent]
    public class States : MonoBehaviour
    {
        #region Properties

        [SerializeReference]
        public Group Group;

        #endregion

        #region Unity methods

        private void Reset() => Group = new Group();

        private void Awake() => Group = Group ?? new Group();

        private void OnEnable() { }

        private void OnDisable() { }

        #endregion
    }
}