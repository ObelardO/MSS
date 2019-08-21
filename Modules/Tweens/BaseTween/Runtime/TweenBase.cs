using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class TweenBase : GenericTween<int>//, ISerializable
    {
        public int instanceID;

        public override void Capture()
        {
            //ganeObject.
            Debug.Log("Captureing BASE tween");
        }
        /*
        public TweenBase()
        {

        }

        protected TweenBase(SerializationInfo info, StreamingContext context)
        {
            Value = (int)info.GetValue("s_Value", typeof(int));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("s_value", Value);
        }
        */
    }
}