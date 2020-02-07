using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obel.MSS.Data;

namespace Obel.MSS
{
    [Serializable]
    public class Group : Collection<State>
    {
        #region Properties

        public State ClosedState => Get((int)DefaultState.Closed);
        public State OpenedState => Get((int)DefaultState.Opened);

        public IReadOnlyList<State> States => Items;
        public IReadOnlyList<State> EnabledStates => EnabledItems;

        [field: SerializeField]
        public GameObject gameObject { private set; get; }

        #endregion

        #region Init


        [Serializable]
        public struct TypedTweens<T> where T : Tween
        {
            public Type TweenType;

            public List<T> Tweens;

            public override int GetHashCode() => TweenType.GetHashCode();
        }

        public void OnTweenCreated<T>(T tween)
        {
            Debug.Log("hi tween: " + tween.GetType());
        }

        public void OnTweenRemoved<T>(T tween)
        {
            Debug.Log("bye tween: " + tween.GetType());
        }


        public Group(GameObject gameObject)
        {
            this.gameObject = gameObject;

            CreateState();
            CreateState();
        }

        #endregion

        #region Public methods

        public State CreateState() => Create();

        public void RemoveState(State state) => Remove(state);

        public override int GetHashCode() => gameObject.GetHashCode();

        #endregion
    }
}