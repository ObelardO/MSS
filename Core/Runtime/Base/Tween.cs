using UnityEngine;

namespace Obel.MSS
{
    public class Tween : CollectionItem
    {

    }

    public class GenericTween<T> : Tween where T : struct
    {
        [SerializeField, HideInInspector] private T s_Value;
        public T Value
        {
            set => s_Value = value;
            get => s_Value;
        }
    }
}

