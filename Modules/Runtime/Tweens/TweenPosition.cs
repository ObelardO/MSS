using System;
using UnityEngine;

namespace Obel.MSS.Modules.Tweens
{
    [Serializable]
    public class TweenPosition : GenericTween<Transform, Vector3>
    {
        #region Public methods

        public override void OnInit()
        {
            Debug.Log("[MSS] [Tween] Say hello to new position tween");
        }

        public override void Capture() => Value = Component.position;

        #endregion
    }
}

