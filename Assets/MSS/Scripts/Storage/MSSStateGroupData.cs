using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    //[Serializable]
    public class MSSStateGroupData : ScriptableObject
    {
        [SerializeField]
        public List<MSSStateData> statesData;

        private void OnEnable()
        {
            if (statesData == null)
                statesData = new List<MSSStateData>();
        }
    }
}
