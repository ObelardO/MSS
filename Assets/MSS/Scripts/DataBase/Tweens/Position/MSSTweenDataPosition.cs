using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSTweenDataPosition : MSSTweenData, IMSSTweenDataValue<Vector3>
    {
        public override string tweenName => "Position";
        public override Type tweenValueType => typeof(Vector3);
        public override Type tweenDataType => typeof(MSSTweenDataPosition);

        [SerializeField] private Vector3 _tweenValue;
        public Vector3 tweenValue { get => _tweenValue; set => _tweenValue = value; }
    }
} 