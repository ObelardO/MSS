using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [System.Serializable]
    public class MSSState
    {
        public string name;
        public MSSItem parent;

        [SerializeField]
        public List<MSSTweenData> tweens = new List<MSSTweenData>();

        public MSSState(MSSItem item)
        {
            parent = item;
            name = "State " + (parent.count + 1); 
        }
    }
}