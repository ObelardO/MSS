using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class State
    {
        public string name = "new state";
        public float delay;
        public float duration;

        public List<Tween> tweens = new List<Tween>();
    }
}