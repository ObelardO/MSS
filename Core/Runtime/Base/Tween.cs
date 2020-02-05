﻿using System;
using UnityEngine;
using Obel.MSS.Data;
using System.Collections.Generic;

namespace Obel.MSS
{
    [Serializable]
    public class Tween : CollectionItem
    {
        #region Properties

        public State State => (State)Parent;

        [SerializeField] private string _easeName;
        private Func<float, float, float> _easeFunc;
        public Func<float, float, float> EaseFunc
        {
            get => _easeFunc ?? (_easeFunc = Ease.Get(_easeName));

            set
            {
                _easeFunc = value;
                if (_easeFunc != null) _easeName = _easeFunc.Method.Name;
            }
        }

        public string EaseName => EaseFunc?.Method.Name ?? _easeName;

        public Vector2 Range = Vector2.up;

        #endregion

        #region Init

        public Tween()
        {
            EaseFunc = Ease.DefaultFunc;
        }

        #endregion

        #region Public methods

        public virtual void Capture() { }

        public virtual void Apply() { }

        #endregion
    }

    [Serializable]
    public abstract class GenericTween<C, V> : Tween
        where C : Component
        where V : struct
    {
        #region Properties

        [SerializeField] private C _component;
        public C Component => _component ? _component : _component = State.Group.gameObject.GetComponent<C>();

        public V Value;

        public override bool Enabled => Component && base.Enabled;

        #endregion
    }
}

