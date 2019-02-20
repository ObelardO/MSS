using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    public class MSSItem : MonoBehaviour
    {
        public List<MSSState> states = new List<MSSState>();

        [SerializeField] private MSSTweenData tweenData;
    }
}
