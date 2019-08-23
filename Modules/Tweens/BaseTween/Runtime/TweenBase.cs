using System;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class TweenBase : GenericTween<int>
    {
        public int instanceID;

        public override void OnInit()
        {
            Debug.Log("SAY HELLO TO BASE TWEEN");
        }

        public override void Capture(GameObject gameObject)
        {
            Value = gameObject.GetInstanceID();

            Debug.Log("Captureing BASE tween from " + gameObject.name);
        }
    }
}