﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class MSSTweenDataPosition : MSSTweenData
    {
        public override string tweenName { get { return "Position"; } }

        [SerializeField]
        public Vector3 tweenValue;
    }
}