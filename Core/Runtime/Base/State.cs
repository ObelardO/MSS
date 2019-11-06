using Obel.MSS.Base;
using UnityEngine;

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
    }
}