using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Obel.MSS
{
    [Serializable]
    public class MSSTweenRotation : MSSTween, IMSSTweenValue<Vector3>
    {
        public override string title => "Rotation";

        [SerializeField] private Vector3 _tweenValue;
        public Vector3 tweenValue { get => _tweenValue; set => _tweenValue = value; }
    }
}