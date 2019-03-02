using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    public class MSSDataBase : ScriptableObject
    {
        [SerializeField]
        public List<MSSStateGroupData> stateGroupsData;

        public void OnEnable()
        {
            if (stateGroupsData == null)
                stateGroupsData = new List<MSSStateGroupData>();
        }
    }
}
