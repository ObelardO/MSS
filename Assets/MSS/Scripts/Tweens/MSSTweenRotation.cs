using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Obel.MSS
{
    public enum MSSRotationMode { Quaternion, Euler, Path }

    [Serializable]
    public class MSSTweenRotation : MSSTween//, IMSSTweenValue<Vector3>
    {
        public override string title => "Rotation";

        public Vector3 tweenValue;
        public MSSRotationMode rotationMode;

        //public Vector3 tweenValue { get => _tweenValue; set => _tweenValue = value; }
    }
}