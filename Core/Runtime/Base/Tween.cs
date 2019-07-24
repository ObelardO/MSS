using System;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class Tween : CollectionItem
    {

    }

    public class GenericTween<T> : Tween
    {
        [SerializeField, HideInInspector] private T s_Value;
        public T Value
        {
            set => s_Value = value;
            get => s_Value;
        }
    }
}

