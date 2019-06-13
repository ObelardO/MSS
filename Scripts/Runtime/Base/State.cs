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
        private StatesBehaviour behaviour;  // TO DO STATE GROUP ???
        */

        public float delay;
        public float duration = 1;

        //public List<Tween> tweens = new List<Tween>();

        [SerializeField, HideInInspector]
        private string _name;
        public string stateName
        {
            private set { _name = value;}
            get
            {
                if (isClosedState) return "closed";
                if (isOpenedState) return "opened";
                return _name;
            }
        }

        [SerializeField, HideInInspector]
        private bool _enabled = true;
        public bool enabled
        {
            private set { _enabled = value; }
            get { return isDefaultState || _enabled; }
        }

        public bool isClosedState { get { return this == ((StatesGroup)parent).closedState; } }
        public bool isOpenedState { get { return this == ((StatesGroup)parent).openedState; } }
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