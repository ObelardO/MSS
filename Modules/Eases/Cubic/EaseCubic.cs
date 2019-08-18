using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    public class EaseCubic
    {
#if UNITY_STANDALONE
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#endif
        private static void ApplicationStart()
        {
            Ease.Add(CubicIn, "Cubic/In");
            Ease.Add(CubicOut, "Cubic/Out");
            Ease.Add(CubicInOut, "Cubic/InOut");
        }

        public static float CubicIn(float t, float d)
        {
            return (t /= d) * t * t;
        }

        public static float CubicOut(float t, float d)
        {
            return ((t = t / d - 1) * t * t + 1);
        }

        public static float CubicInOut(float t, float d)
        {
            if ((t /= d / 2) < 1) return 0.5f * t * t * t;
            return 0.5f * ((t -= 2) * t * t + 2);
        }
    }
}


