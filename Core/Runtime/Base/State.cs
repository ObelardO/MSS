using System;
using UnityEngine;
using Obel.MSS.Base;

namespace Obel.MSS
{
    [System.Serializable]
    public class State : Collection<Tween>
    {
        #region Properties

        public Group Group => (Group)Parent;

        public float Delay;

        public float Duration = 1;

        public override string Name
        {
            get
            {
                if (IsClosedState) return "closed";
                if (IsOpenedState) return "opened";
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
            base.Name = "new state";
        }

        #endregion

        #region Public methods

        public T CreateTween<T>() where T : Tween, new() => (T)Add(new T());

        public void Capture() => ForEachEnabled(i => i.Capture());

        public void Apply() => ForEachEnabled(i => i.Apply());

        public void Select()
        {
            Debug.Log($"State {Name} selected!");
        }

        #endregion
    }
}