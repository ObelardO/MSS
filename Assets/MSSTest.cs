using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    public class MSSTest : MonoBehaviour
    {

        public Vector3 position1;
        public Vector3 position2;


        // Start is called before the first frame update
        void Start()
        {
            MSSCoreBehaviour.instance.a = "hello";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
