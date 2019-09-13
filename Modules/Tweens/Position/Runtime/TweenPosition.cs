using System;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class TweenPosition : GenericTween<Vector3>
    {
        public float position;

        public override void OnInit()
        {
            Debug.Log("[MSS] [Tweens] Say hello to new position tween");
        }

        public override void Capture(GameObject gameObject)
        {
            Value = gameObject.transform.position;
        }
    }
}

