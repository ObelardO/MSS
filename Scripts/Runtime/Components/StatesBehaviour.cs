﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Obel.MSS
{
    public enum DefaultState { Closed, Opened }

    [DisallowMultipleComponent, AddComponentMenu("MSS/States")]
    #if UNITY_2018_3_OR_NEWER
        [ExecuteAlways]
    #else
        [ExecuteInEditMode]
    #endif
    public class StatesBehaviour : MonoBehaviour
    {
        public StatesGroup statesGroup;

        private void Reset()
        {
            statesGroup = null;
        }

        private void OnEnable() { }

        private void OnDisable() { }
    }
}