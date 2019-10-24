using System;
using UnityEngine;
using Obel.MSS.Base;

namespace Obel.MSS
{
    [System.Serializable]
    public class Tween : CollectionItem
    {
        #region Properties

        [SerializeField] private string _ease;
        private Func<float, float, float> _easeFunc;
        public Func<float, float, float> EaseFunc
        {
            get => _easeFunc ?? (_easeFunc = Ease.Get(_ease));

            set
            {
                _easeFunc = value;
                if (_easeFunc != null) _ease = _easeFunc.Method.Name;
            }
        }

        public string EaseName => EaseFunc?.Method.Name ?? _ease;

        public Vector2 Range = Vector2.up;

        #endregion

        #region Public methods

        public virtual void Capture(GameObject gameObject) { }

        #endregion
    }

    [System.Serializable]
    public class GenericTween<T> : Tween where T : struct
    {
        #region Properties

        public T Value;

        #endregion
    }
}

