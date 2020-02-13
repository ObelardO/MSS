using UnityEngine;
using Obel.MSS.Base;

namespace Obel.MSS.Modules.Eases
{
    public class EaseQuad
    {
        #region Init

        #if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        #else
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        #endif
        private static void ApplicationStart()
        {
            Ease.Add(QuadIn, "Quad/In", -1);
            Ease.Add(QuadOut, "Quad/Out", -1);
            Ease.Add(QuadInOut, "Quad/InOut", -2);
        }

        #endregion

        #region Private methods

        private static float QuadIn(float t, float d)
        {
            return (t /= d) * t;
        }

        private static float QuadOut(float t, float d)
        {
            return -(t /= d) * (t - 2);
        }

        private static float QuadInOut(float t, float d)
        {
            if ((t /= d / 2) < 1) return 0.5f * t * t;
            return -0.5f * ((--t) * (t - 2) - 1);
        }

        /*
        public static float QuadOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2) return QuadOut(t * 2, b, c / 2, d);
            return QuadIn((t * 2) - d, b + c / 2, c / 2, d);
        }
        */

        #endregion
    }
}


