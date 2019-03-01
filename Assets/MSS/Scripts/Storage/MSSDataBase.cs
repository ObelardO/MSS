using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSDataBase : ScriptableObject
    {
        [SerializeField]
        public List<MSSStateData> statesData;

        public void OnEnable()
        {
            if (statesData == null)
                statesData = new List<MSSStateData>();

            //hideFlags = HideFlags.HideInHierarchy;
        }
    }
}
