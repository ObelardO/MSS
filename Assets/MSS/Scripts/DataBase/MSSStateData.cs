using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    public class MSSStateData : ScriptableObject
    {
        [SerializeField]    
        public string stateName;

        [SerializeField]
        public List<MSSTweenData> tweensData;

        public void OnEnable ()
        {
	        if (tweensData == null)
                tweensData = new List<MSSTweenData>();

            //hideFlags = HideFlags.HideInHierarchy;
        }
    }
}
