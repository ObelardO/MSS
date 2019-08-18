using System;
using UnityEngine;
using UnityEngine.Events;
//using static Obel.MSS.Ease;

namespace Obel.MSS
{
    public class Tween : CollectionItem
    {
        [SerializeField] private string s_Ease;
        //private Ease.EaseDelegate ease;
        //public Ease.EaseDelegate Ease { get => ease ?? (ease = Ease.Get(s_Ease)); }

        //

        private Func<float, float, float> ease;
        public Func<float, float, float> Ease
        {

            get
            {
                if (ease == null) ease = MSS.Ease.Get(s_Ease);

                return ease;
            } 

            set
            {
                ease = value;

                if (ease != null) s_Ease = ease.Method.Name;
            }
        }


        [SerializeField, HideInInspector]
        private bool s_Enabled = true;
        public bool Enabled
        {
            set => s_Enabled = value;
            get => s_Enabled;
        }
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

