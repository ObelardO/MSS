using System;
using UnityEngine;
using Obel.MSS.Base;
using UnityEditor.Graphs;

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

        #region Public methods

        //public virtual void Capture<T>(ref T property) where T : struct { }
        public virtual void Capture() { }

        /*
        public ref struct GetProperty()
        {

        }
        */


        #endregion

        #region Init

        public Tween()
        {
            if (Ease.DefaultFunc != null) EaseFunc = Ease.DefaultFunc;

            Debug.Log("BC New tween: " + Name);
        }

        #endregion
    }

    [Serializable]
    public class GenericTween<C, V> : Tween 
        where C : Component
        where V : struct
    {
        #region Properties

        public State State => (State)Parent;

        [SerializeField/* SerializeReference*/] private C _component;
        public C Component => _component ?? (_component = State.Group.gameObject.GetComponent<C>());

        public V Value;

        #endregion

        #region Init
        /*
        public GenericTween()
        {
            Debug.Log("SC New tween: " + Name);
        }
        */
        #endregion
    }
}

