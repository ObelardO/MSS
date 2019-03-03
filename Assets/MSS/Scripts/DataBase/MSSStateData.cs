using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSStateData : MSSDataBaseCollection<MSSTweenData>
    {
        [SerializeField]
        public string stateName;

        private void OnEnable()
        {
            if (items == null)
                items = new List<MSSTweenData>();
        }
    }
}
