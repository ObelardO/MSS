using System;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class TweenBase : GenericTween<Vector3>
    {
        public int instanceID;

        public override void Capture()
        {
            //ganeObject.
            Debug.Log("Captureing BASE tween");
        }
    }
}