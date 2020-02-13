using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS.Data
{
    [Serializable]
    public sealed class Group : Collection<State>
    {
        #region Properties

        public State ClosedState => Get((int)DefaultState.Closed);
        public State OpenedState => Get((int)DefaultState.Opened);

        public IReadOnlyList<State> States => Items;
        public IReadOnlyList<State> EnabledStates => EnabledItems;

        [field: SerializeField]
        public States StatesComponent { private set; get; }

        public GameObject GameObject => StatesComponent.gameObject;

        #endregion

        #region Init

        /*
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
        */


        public Group(States states)
        {
            Debug.Log(states);

            StatesComponent = states;

            CreateState();
            CreateState();
        }

        #endregion

        #region Public methods

        public State CreateState() => Create();

        public void RemoveState(State state) => Remove(state);

        public override int GetHashCode() => GameObject.GetHashCode();

        #endregion
    }
}