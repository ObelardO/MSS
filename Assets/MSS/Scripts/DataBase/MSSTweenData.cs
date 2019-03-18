using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    
    public interface IMSSTweenDataValue<T> where T : struct
    {
        T tweenValue { set; get; }
    }
    

    /*
    [Serializable]
    public class MSSTweenData<T> : MSSTweenData
    {
        public T tweenValue;

    }
    */


    [Serializable]
    public class MSSTweenData : MSSDataBaseCollectionItem
    {
        public virtual string tweenName { get; }

        public virtual Type tweenValueType { get; }
        public virtual Type tweenDataType { get; }

      
        public void OnEnable()
        {
            //hideFlags = HideFlags.HideInHierarchy;
        }
    }
}
