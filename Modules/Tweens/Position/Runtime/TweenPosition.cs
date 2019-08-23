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
            Debug.Log("SAY HELLO TO POSITION TWEEN");
        }

        public override void Capture(GameObject gameObject)
        {
            Value = gameObject.transform.position;

            Debug.Log("Captureing position tween from " + gameObject.name);
        }
    }
}

