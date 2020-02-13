using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS.Data
{
    [Serializable]
    public class State : Collection<Tween>
    {
        #region Properties

        public Group Group => (Group)Parent;

        public GameObject GameObject => Group.GameObject;

        public float Delay;

        public float Duration = 1;

        private static readonly string closedName = "closed";
        private static readonly string openedName = "opened";
        private static readonly string defaultName = "new state";

        public override string Name
        {
            get
            {
                if (IsClosedState) return closedName;
                if (IsOpenedState) return openedName;
                return base.Name;
            }
            set
            {
                if (!IsDefaultState) base.Name = value;
            }

        }

        public bool IsClosedState => this == Group?.ClosedState;
        public bool IsOpenedState => this == Group?.OpenedState;
        public bool IsDefaultState => IsClosedState || IsOpenedState;

        #endregion

        #region Init

        public State()
        {
            base.Name = defaultName;
        }

        #endregion

        #region Public methods

        public T CreateTween<T>() where T : Tween, new() => (T)Add(new T());

        public bool RemoveTween<T>(T tween) where T : Tween => Remove(tween);

        public void Capture() => ForEachEnabled(i => i.Capture());

        public void Apply() => ForEachEnabled(i => i.Apply());

        #endregion
    }
}