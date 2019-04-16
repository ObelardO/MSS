using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSStateGroup : MSSCollection<MSSState>
    {
        [SerializeField]
        public int objectID;

        private void OnEnable()
        {
            Init();
        }
    }
}
