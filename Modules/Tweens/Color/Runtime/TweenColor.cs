using System;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class TweenColor : GenericTween<Color>
    {
        public override void OnInit()
        {
            Debug.Log("[MSS] [Tweens] Say hello to new color tween!");
        }

        public override void Capture(GameObject gameObject)
        {
            Value = gameObject.GetComponent<Light>()?.color ?? Color.white;
        }
    }
}

