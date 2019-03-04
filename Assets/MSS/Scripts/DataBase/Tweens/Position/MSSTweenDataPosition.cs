using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSTweenDataPosition : MSSTweenData//, IMSSTweenData
    {
        public override string tweenName => "Position";

        [SerializeField]
        public Vector3 tweenValue;
    }
} 