#define MSS
#define MSS_1_6_0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    public static class MSSCore
    {

    }   

    [DisallowMultipleComponent]
    public class MSSCoreBehaviour : MonoBehaviour
    {
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
