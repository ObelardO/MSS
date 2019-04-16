using System;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [ExecuteInEditMode, DisallowMultipleComponent, AddComponentMenu("MSS/Item")]
    public class MSSItem : MonoBehaviour
    {
        [SerializeField]
        public int dataBaseID = int.MaxValue;

        [SerializeField]
        public MSSStateGroup stateGroup;

        private void Awake()
        {

        }
    }
}
