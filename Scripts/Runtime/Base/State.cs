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
        public string title;

        /*
        [SerializeField]
        private StatesBehaviour behaviour;  // TODO STATE GROUP ???
        */

        [SerializeField, HideInInspector]
        private float m_delay;
        public float delay
        {
            set { m_delay = value; }
            get { return m_delay; }
        }

        [SerializeField, HideInInspector]
        private float m_duration;
        public float duration
        {
            set { m_duration = value; }
            get { return m_duration; }
        }


        //public List<Tween> tweens = new List<Tween>();

        [SerializeField, HideInInspector]
        private string m_name;
        public string stateName
        {
            private set { m_name = value;}
            get
            {
                if (isClosedState) return "closed";
                if (isOpenedState) return "opened";
                return m_name;
            }
        }

        [SerializeField, HideInInspector]
        private bool m_enabled = true;
        public bool enabled
        {
            private set { m_enabled = value; }
            get { return isDefaultState || m_enabled; }
        }

        public bool isClosedState { get { return false /* this == ((StatesGroup)parent).closedState*/; } }
        public bool isOpenedState { get { return false /* this == ((StatesGroup)parent).openedState*/; } }
        public bool isDefaultState { get { return isClosedState || isOpenedState; } }

        /*
        public State(StatesBehaviour behaviour, string name, int id)
        {
            if (behaviour == null)
            {
                Debug.LogWarning("[MSS]\"behaviour\" parameter can't be a null!");
                return;
            }

            idCounter++;

            this.behaviour = behaviour;
            this.stateName = name;
            this.id = id;
        }
        */
    }
}