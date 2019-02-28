﻿#define MSS
#define MSS_1_6_0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{

    public static class MSSCore
    {

    }   

    [DisallowMultipleComponent, ExecuteInEditMode]
    public class MSSCoreBehaviour : MonoBehaviour
    {
        [HideInInspector] public string a;

        private static MSSCoreBehaviour _instance;
        [HideInInspector] public static MSSCoreBehaviour instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject /*("MSS Behaviour");*/ { name = "MSS Behaviour", hideFlags = HideFlags.HideAndDontSave };
                    _instance = gameObject.AddComponent<MSSCoreBehaviour>();
                    if (Application.isPlaying)
                        DontDestroyOnLoad(gameObject);
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null) DestroyImmediate(this);
        }

        private void Update()
        {

        }

        private void FixedUpdate()
        {
            
        }

        private void LateUpdate()
        {
            
        }

    }
}
