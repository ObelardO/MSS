using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{

    public interface IMSSTweenData
    {
        /*
        void Set<T>(T val) where T : struct;
        T Get<T>() where T : struct;
        void GetType();
        */
    }

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
