using System;
using UnityEngine;

namespace Obel.MSS.Modules.Tweens
{
    [Serializable]
    public class TweenRotation : GenericTween<Transform, Quaternion>
    {
        #region Properties

        public bool IsLocal = true;
        public bool IsQuaternion = true;

        #endregion

        #region Public methods

        public override void OnInit()
        {
            Debug.Log("[MSS] [Tween] Say hello to new rotation tween");
        }

        public override void Capture() => Value = IsLocal ? Component.localRotation : Component.rotation;

        public override void Apply()
        {
            if (IsLocal)
            {
                Component.localRotation = Value;
                return;
            }

            Component.rotation = Value;
        }

        #endregion
    }
}

