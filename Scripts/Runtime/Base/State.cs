using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Obel.MSS
{
    [Serializable]
    public class State : DBCollection<Tween>
    {
        #region Properties

        [SerializeField, HideInInspector]
        private float s_Delay;
        public float delay
        {
            set { s_Delay = value; }
            get { return s_Delay; }
        }

        [SerializeField, HideInInspector]
        private float s_Duration = 1;
        public float Duration
        {
            set { s_Duration = value; }
            get { return s_Duration; }
        }

        [SerializeField, HideInInspector]
        private string s_Name = "NewState";
        public string Name
        {
            private set { s_Name = value; }
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
            private set { s_Enabled = value; }
            get { return IsDefaultState || s_Enabled; }
        }

        public bool IsClosedState { get { return this == ((StatesGroup)Parent).ClosedState; } }
        public bool IsOpenedState { get { return this == ((StatesGroup)Parent).OpenedState; } }
        public bool IsDefaultState { get { return IsClosedState || IsOpenedState; } }

        #endregion
    }
}