using UnityEngine;

namespace Obel.MSS
{
    [System.Serializable]
    public class State : Collection<Tween>
    {
        #region Properties

        [SerializeField, HideInInspector]
        private float s_Delay;
        public float Delay
        {
            set => s_Delay = value;
            get => s_Delay;
        }

        [SerializeField, HideInInspector]
        private float s_Duration = 1;
        public float Duration
        {
            set => s_Duration = value;
            get => s_Duration;
        }

        [SerializeField, HideInInspector]
        private string s_Name = "NewState";
        public string Name
        {
            private set => s_Name = value;
            get
            {
                if (IsClosedState) return "closed";
                if (IsOpenedState) return "opened";
                return s_Name;
            }
        }

        [SerializeField, HideInInspector]
        private bool s_Enabled = true;
        public bool Enabled
        {
            private set => s_Enabled = value;
            get => IsDefaultState || s_Enabled;
        }

        public bool IsClosedState { get => this == ((StatesGroup)Parent).ClosedState; }
        public bool IsOpenedState { get => this == ((StatesGroup)Parent).OpenedState; }
        public bool IsDefaultState { get => IsClosedState || IsOpenedState; }

        #endregion
    }
}