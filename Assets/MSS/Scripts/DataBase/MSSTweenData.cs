using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSTweenData : MSSDataBaseCollectionItem
    {
        public virtual string tweenName { get; }

        public void OnEnable()
        {
            //hideFlags = HideFlags.HideInHierarchy;
        }
    }
}
