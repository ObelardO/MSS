using System;
using UnityEngine;

namespace Obel.MSS.Modules.Tweens
{
    [Serializable]
    public class TweenRotation : GenericTween<Transform, Vector3>
    {
        #region Properties

        public bool IsLocal = true;
        public RotationMode Mode;

        public enum RotationMode { Quaternion, Euler }

        #endregion

        #region Public methods

        public override void OnInit()
        {
            Debug.Log("[MSS] [Tween] Say hello to new rotation tween");
        }

        public override void Capture() => Value = IsLocal ? Component.localEulerAngles : Component.eulerAngles;

        public override void Apply()
        {
            if (IsLocal)
            {
                Component.localEulerAngles = Value;
                return;
            }

            Component.eulerAngles = Value;
        }

        #endregion
    }
}

