#define MSS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    public static class MSSCore
    {

    }   

    public class MSSCoreBehaviour : MonoBehaviour
    {
        public string a = string.Empty;

        private static MSSCoreBehaviour _instance;
        [HideInInspector] public static MSSCoreBehaviour instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("MSS Behaviour");
                    _instance = gameObject.AddComponent<MSSCoreBehaviour>();
                    DontDestroyOnLoad(gameObject);
                }

                return _instance;
            }
        
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
