using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    public class MSSStateGroupData : ScriptableObject
    {
        [SerializeField]
        public int objectID;

        [SerializeField]
        public List<MSSStateData> statesData;

        private void OnEnable()
        {
            if (statesData == null)
                statesData = new List<MSSStateData>();

            //hideFlags = HideFlags.HideInHierarchy;

        }
    }
}
