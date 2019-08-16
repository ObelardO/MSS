using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    //internal sealed static class Ease

    public class EaseQuad
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad), InitializeOnLoadMethod]
        private static void ApplicationStart()
        {
            Ease.Add(QuadIn, "Quad/In");
            Ease.Add(QuadOut, "Quad/Out");
            Ease.Add(QuadInOut, "Quad/InOut");
            Ease.Add(QuadOutIn, "Quad/OutIn");
        }

        public static float QuadIn(float t, float b, float c, float d)
        {
            return c * (t /= d) * t + b;
        }

        public static float QuadOut(float t, float b, float c, float d)
        {
            return -c * (t /= d) * (t - 2) + b;
        }

        public static float QuadInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t + b;

            return -c / 2 * ((--t) * (t - 2) - 1) + b;
        }

        public static float QuadOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2) return QuadOut(t * 2, b, c / 2, d);
            return QuadIn((t * 2) - d, b + c / 2, c / 2, d);
        }
    }
}


