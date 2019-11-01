using System;
using UnityEngine;
using UnityEngine.UI;

namespace Obel.MSS.Modules.Tweens
{
    [Serializable]
    public class TweenColor : GenericTween<Graphic, Color>
    {
        #region Public methods

        public override void OnInit()
        {
            Debug.Log("[MSS] [Tween] Say hello to new color tween!");
        }

        public override void Capture() => Value = Component.color;

        #endregion
    }
}

