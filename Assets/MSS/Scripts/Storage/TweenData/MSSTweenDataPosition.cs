using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSTweenDataPosition : MSSTweenDataBase
    {
        public override string tweenName { get { return "Position"; } }

        [SerializeField]
        public Vector3 tweenValue;

        /*
        public override void OnGUI()
        {
            base.OnGUI();
            MSSEditorUtils.DrawGenericProperty(ref tweenValue, this);
        }
        */
    }
}