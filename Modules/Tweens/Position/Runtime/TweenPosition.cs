using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class TweenPosition : GenericTween<Vector3>//, ISerializable
    {
        public float position;
        /*
        public TweenPosition()
        {

        }

        protected TweenPosition(SerializationInfo info, StreamingContext context)
        {
            Value = (Vector3)info.GetValue("s_Value", typeof(Vector3));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("s_value", Value);
        }
        */
    }
}

