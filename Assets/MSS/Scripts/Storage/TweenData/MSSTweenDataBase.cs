using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSTweenDataBase : ScriptableObject
    {
        public virtual string tweenName { get; }

        [SerializeField]
        public MSSStateData parentStateData;

        public void OnEnable()
        {
            //hideFlags = HideFlags.HideInHierarchy;
        }
    }
}
