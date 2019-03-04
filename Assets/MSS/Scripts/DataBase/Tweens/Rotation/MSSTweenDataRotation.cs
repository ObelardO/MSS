using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSTweenDataRotation : MSSTweenData//, IMSSTweenData
    {
        public override string tweenName => "Rotation";

        [SerializeField]
        public Vector3 tweenValue;
    }
}