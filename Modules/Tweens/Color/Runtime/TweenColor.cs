using System;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class TweenColor : GenericTween<Color>
    {
        public float position;

        public override void OnInit()
        {
            Debug.Log("SAY HELLO TO COLOR TWEEN");
        }

        public override void Capture(GameObject gameObject)
        {
            Value = gameObject.GetComponent<Light>()?.color ?? Color.white;

            Debug.Log("Captureing color tween from " + gameObject.name);
        }
    }
}

