using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    //[Serializable]
    public class MSSDataBase : ScriptableObject
    {


        [SerializeField]
        public Dictionary<int, MSSStateGroupData> stateGroupsData;

        //[SerializeField]
        //public List<MSSStateData> statesData;

        public void OnEnable()
        {
            //if (statesData == null)
            //    statesData = new List<MSSStateData>();

            if (stateGroupsData == null)
                stateGroupsData = new Dictionary<int, MSSStateGroupData>();

            //hideFlags = HideFlags.HideInHierarchy;
        }
    }
}
