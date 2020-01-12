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
        
        private void Reset() => Group = new Group(gameObject);

        private void Awake() => Group = Group ?? new Group(gameObject);

        private void OnEnable() => Group.Enabled = true;

        private void OnDisable() => Group.Enabled = false;

        #endregion

        #region Public methods

        public void Open() => Group.Open();

        public void Close() => Group.Close();

        public void Select(string stateName) => Group.Select(stateName);

        #endregion
    }
}