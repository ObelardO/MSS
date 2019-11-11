using System;
using UnityEngine;

namespace Obel.MSS.Modules.Tweens
{
    [Serializable]
    public class TweenPosition : GenericTween<Transform, Vector3>
    {
        #region Properties

        public bool IsLocal = true;

        #endregion

        #region Public methods

        public override void OnInit()
        {
            Debug.Log("[MSS] [Tween] Say hello to new position tween");
        }

        public override void Capture() => Value = IsLocal ? Component.localPosition : Component.position;

        public override void Apply()
        {
            if (IsLocal)
            {
                Component.localPosition = Value;
                return;
            }

            Component.position = Value;
        }

        #endregion
    }
}

