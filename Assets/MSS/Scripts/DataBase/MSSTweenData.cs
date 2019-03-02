using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    public class MSSTweenData : ScriptableObject
    {
        public virtual string tweenName { get; }

        public void OnEnable()
        {
            //hideFlags = HideFlags.HideInHierarchy;
        }
    }
}
