using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [System.Serializable]
    public class MSSState
    {
        public string name;

        [SerializeField]
        public List<MSSTweenData> tweens = new List<MSSTweenData>();
    }
}