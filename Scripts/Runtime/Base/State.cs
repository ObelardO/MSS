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

        /*
        [SerializeField]
        private StatesBehaviour behaviour;

        public float delay;
        public float duration = 1;

        public List<Tween> tweens = new List<Tween>();

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

        
        [SerializeField, HideInInspector]
        private int _id;
        public int id
        {
            private set { _id = value; }
            get { return _id; }
        }


        [NonSerialized]
        private static int idCounter;

        public bool isClosedState { get { return this == behaviour.closedState; } }
        public bool isOpenedState { get { return this == behaviour.openedState; } }
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