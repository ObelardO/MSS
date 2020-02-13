using System;
using UnityEngine;
using Obel.MSS.Data;

namespace Obel.MSS.Modules.Tweens
{
    [Serializable]
    public class TweenBase : GenericTween<Transform, Vector3>
    {
        #region Public methods

        public override void OnInit()
        {
            base.OnInit();
            Debug.Log("[MSS] [Tween] Say hello to new base tween!");
        }

        //TODO make it by REF (C# 7.2)
        public override void Capture() => Value = Component.position;

        public override void Apply() => Component.position = Value;

        #endregion
    }
}