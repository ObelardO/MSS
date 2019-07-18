using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class TweenBase : Tween, ITween
    {
        [SerializeField, HideInInspector] private Vector3 s_Value;
        public Vector3 Value => s_Value;
    }
}

