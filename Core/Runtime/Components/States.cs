using System;
using UnityEngine;
using UnityEngine.Events;
using Unity.Entities;

namespace Obel.MSS
{
    [AddComponentMenu("MSS/States"), DisallowMultipleComponent, RequiresEntityConversion, RequireComponent(typeof(ConvertToEntity))]
    public class States : MonoBehaviour, IConvertGameObjectToEntity
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

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new StatesData() { Group = this.Group };
            dstManager.AddComponentData(entity, data);
        }

        #endregion
    }
}