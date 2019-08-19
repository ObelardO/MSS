using System;
using UnityEngine;

namespace Obel.MSS
{
    public class Tween : CollectionItem
    {
        #region Properties

        [SerializeField] private string s_Ease;
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
        public string EaseName => s_Ease;

        [SerializeField, HideInInspector]
        private bool s_Enabled = true;
        public bool Enabled
        {
            set => s_Enabled = value;
            get => s_Enabled;
        }

        #endregion
    }

    public class GenericTween<T> : Tween where T : struct
    {
        #region Properties

        [SerializeField, HideInInspector] private T s_Value;
        public T Value
        {
            set => s_Value = value;
            get => s_Value;
        }

        #endregion
    }
}

