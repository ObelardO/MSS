using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    
    public interface IMSSTweenValue<T> where T : struct
    {
        T tweenValue { set; get; }
    }
    
    [Serializable]
    public class MSSTween : MSSCollectionItem
    {
        public virtual string title { get; }
      
        public void OnEnable()
        {
            //hideFlags = HideFlags.HideInHierarchy;
        }

#if UNITY_EDITOR
        public virtual void OnGUI()
        {

        }
#endif

    }

}
