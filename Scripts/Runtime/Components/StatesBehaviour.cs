using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Obel.MSS
{
    public enum DefaultState { closed, opened }

    [DisallowMultipleComponent, AddComponentMenu("MSS/States")]
    #if UNITY_2018_3_OR_NEWER
        [ExecuteAlways]
    #else
        [ExecuteInEditMode]
    #endif
    public class StatesBehaviour : MonoBehaviour
    {


        /*
        [SerializeField]
        public List<State> states = new List<State>();

        public State closedState { get { return Get((int)DefaultState.closed); } }
        public State openedState { get { return Get((int)DefaultState.opened); } }

        [SerializeField] private int idCounter;

        public State Add(string name)
        {
            states.Add(new State(this, name, ++idCounter));

            return states[states.Count - 1];
        }

        public State Get(int id)
        {
            if (id < 0 || id > states.Count - 1) return null;

            return states[id];
        }

        private void Init()
        {
            if (states.Count == 0)
            {
                Add("closed");
                Add("opened");
            }
        }

        private void Reset()
        {
            Init();
        }

        private void Awake()
        {
            Init();
        }
        
        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }
        */
    }
}