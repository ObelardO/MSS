using System;
using UnityEngine;

namespace Obel.MSS.Modules.Tweens
{
    [Serializable]
    public class TweenPosition : GenericTween<Vector3>
    {
        public override void OnInit()
        {
            Debug.Log("[MSS] [Tween] Say hello to new position tween");
        }

        public override void Capture(GameObject gameObject)
        {
            Value = gameObject.transform.position;
        }
    }
}

