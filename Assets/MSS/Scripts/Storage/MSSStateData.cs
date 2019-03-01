using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSStateData : ScriptableObject
    {
        [SerializeField]    
        public string stateName;

        [SerializeField]
        public List<MSSTweenDataBase> tweensData;

        public void OnEnable ()
        {
	        if (tweensData == null)
                tweensData = new List<MSSTweenDataBase>();

            //hideFlags = HideFlags.HideInHierarchy;
        }
    }
}
