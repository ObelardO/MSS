using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSState : MSSCollection<MSSTween>
    {
        [SerializeField]
        public string stateName;

        private void OnEnable()
        {
            Init();
        }
    }
}
