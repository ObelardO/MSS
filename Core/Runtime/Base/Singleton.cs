using System;
using UnityEngine;

namespace Obel.MSS.Base
{
    [DisallowMultipleComponent]
    internal abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static System.Object _lock = new System.Object();

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<T>();
                            
                        if (_instance == null) _instance = new GameObject("[SINGLE] " + typeof(T)).AddComponent<T>(); 
                    }

                    DontDestroyOnLoad(_instance);

                    return _instance;
                }
            }
        }

        public virtual void Awake()
        {
            if (_instance != null && _instance != this) DestroyImmediate(gameObject);
        }
    }
}


