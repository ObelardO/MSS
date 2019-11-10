using System;
using UnityEngine;
using Obel.MSS.Base;

namespace Obel.MSS
{
    [Serializable]
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

        #region Init

        public Tween()
        {
            EaseFunc = Ease.DefaultFunc;

            Debug.Log("BC New tween: " + Name);
        }

        #endregion

        #region Public methods

        public virtual void Capture() { }

        public virtual void Apply() { }

        #endregion
    }

    [Serializable]
    public class GenericTween<C, V> : Tween 
        where C : Component
        where V : struct
    {
        #region Properties

        public State State => (State)Parent;

        [SerializeField] private C _component;
        public C Component => !_component ? _component = State.Group.gameObject.GetComponent<C>() : _component;

        public V Value;

        public override bool Enabled => !Component ? false : base.Enabled;

        #endregion
    }
}

