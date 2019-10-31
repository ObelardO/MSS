using System;
using UnityEngine;

namespace Obel.MSS.Modules.Tweens
{
    [Serializable]
    public class TweenBase : GenericTween<Transform, Vector3>
    {
        #region Public methods

        public override void OnInit()
        {
            Debug.Log("[MSS] [Tween] Say hello to new base tween!");
        }

        public override void Capture(GameObject gameObject) => Value = gameObject.transform.position;

        #endregion
    }
}