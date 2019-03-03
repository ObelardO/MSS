using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSStateGroupData : MSSDataBaseCollection<MSSStateData>

    {
        [SerializeField]
        public int objectID;

       //  public List<MSSDataBaseCollection> statesData;

        private void OnEnable()
        {
            if (items == null)
                items = new List<MSSStateData>();
        }
    }
}
