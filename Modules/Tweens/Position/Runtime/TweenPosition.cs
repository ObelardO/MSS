using System;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class TweenPosition : GenericTween<Vector3>
    {
        public float position;

        public override void Capture()
        {
            Debug.Log("Captureing position tween");
        }
    }
}

