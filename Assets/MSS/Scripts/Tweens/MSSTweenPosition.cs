using System;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSTweenPosition : MSSTween//, IMSSTweenValue<Vector3>
    {
        public override string title => "Position";

        [HideInInspector] public Vector3 tweenValue;
        //public Vector3 tweenValue { get => _tweenValue; set => _tweenValue = value; }
    }
} 