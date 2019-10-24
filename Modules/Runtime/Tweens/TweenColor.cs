using System;
using UnityEngine;

namespace Obel.MSS.Modules.Tweens
{
    [Serializable]
    public class TweenColor : GenericTween<Color>
    {
        #region Public methods

        public override void OnInit()
        {
            Debug.Log("[MSS] [Tween] Say hello to new color tween!");
        }

        public override void Capture(GameObject gameObject)
        {
            Value = gameObject.GetComponent<Light>()?.color ?? Color.white;
        }

        #endregion
    }
}

