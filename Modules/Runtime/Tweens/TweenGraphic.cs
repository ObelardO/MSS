using System;
using UnityEngine;
using UnityEngine.UI;
using Obel.MSS.Data;

namespace Obel.MSS.Modules.Tweens
{
    [Serializable]
    public class TweenGraphic : GenericTween<Graphic, Color>
    {
        #region Properties

        public bool IsRaycastTarget = true;

        #endregion

        #region Public methods

        public override void OnInit()
        {
            base.OnInit();
            Debug.Log("[MSS] [Tween] Say hello to new color tween!");
        }

        public override void Capture() => Value = Component.color;

        public override void Apply() => Component.color = Value;

        #endregion
    }
}

