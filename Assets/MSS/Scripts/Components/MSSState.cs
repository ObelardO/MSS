using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    public class MSSState
    {
        public string name;
        public GameObject gameObject;

        public List<IMSSTween> tweens = new List<IMSSTween>();

        public MSSState(GameObject gameObject, string name = "new state")
        {
            this.gameObject = gameObject;
            this.name = name;
        }


    }
}