using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSTweenDataRotation : MSSTweenData, IMSSTweenDataValue<Vector3>
    {
        public override string tweenName => "Rotation";
        public override Type tweenValueType => typeof(Vector3);
        public override Type tweenDataType => typeof(MSSTweenDataRotation);

        [SerializeField] private Vector3 _tweenValue;
        public Vector3 tweenValue { get => _tweenValue; set => _tweenValue = value; }
    }
}