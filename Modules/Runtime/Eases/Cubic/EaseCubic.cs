using UnityEngine;
using Obel.MSS.Base;

namespace Obel.MSS.Modules.Eases
{
    public class EaseCubic
    {
        #region Init

        #if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        #else
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        #endif
        private static void ApplicationStart()
        {
            Ease.Add(CubicIn, "Cubic/In");
            Ease.Add(CubicOut, "Cubic/Out");
            Ease.Add(CubicInOut, "Cubic/InOut");
        }

        #endregion

        #region Private methods

        private static float CubicIn(float t, float d)
        {
            return (t /= d) * t * t;
        }

        private static float CubicOut(float t, float d)
        {
            return (t /= d - 1) * t * t + 1;
        }

        private static float CubicInOut(float t, float d)
        {
            if ((t /= d / 2) < 1) return 0.5f * t * t * t;
            return 0.5f * ((t -= 2) * t * t + 2);
        }
        /*
        private static float CubicOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2) return CubicOut(t * 2, b, c / 2, d);
            return CubicIn((t * 2) - d, b + c / 2, c / 2, d);
        }
        */
        #endregion
    }
}