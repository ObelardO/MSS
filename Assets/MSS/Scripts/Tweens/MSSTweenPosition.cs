using System;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSTweenPosition : MSSTween, IMSSTweenValue<Vector3>
    {
        public override string title => "Position";

        [SerializeField] private Vector3 _tweenValue;
        public Vector3 tweenValue { get => _tweenValue; set => _tweenValue = value; }
    }
} 