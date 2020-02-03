using System;
using UnityEngine;
using UnityEngine.Events;
using Obel.MSS.Node;

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
        
        private void Reset() => Group = new Group(gameObject);

        private void Awake() => Group = Group ?? new Group(gameObject); // ??= not supported in unity?

        private void OnEnable() => Group.Enabled = true;

        private void OnDisable() => Group.Enabled = false;

        #endregion

        #region Public methods

        public void Open() => Core.SelectState(Group.OpenedState);

        public void Close() => Core.SelectState(Group.ClosedState);

        public void Select(string stateName) => Core.SelectState(Group, stateName);

        #endregion
    }
}