using Obel.MSS;

namespace Obel.MSS.Modules.Eases
{
    public class EaseQuint
    {
        #region Init

        #if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        #elif UNITY_STANDALONE
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        #endif
        private static void ApplicationStart()
        {
            Ease.Add(QuintIn, "Quint/In");
            Ease.Add(QuintOut, "Quint/Out");
            Ease.Add(QuintInOut, "Quint/InOut");
        }

        #endregion

        #region Private methods

        private static float QuintIn(float t, float d)
        {
            return (t /= d) * t * t * t * t;
        }

        private static float QuintOut(float t, float d)
        {
            return (t = t / d - 1) * t * t * t * t + 1;
        }

        private static float QuintInOut(float t, float d)
        {
            if ((t /= d / 2) < 1) return 0.5f * t * t * t * t * t;
            return 0.5f * ((t -= 2) * t * t * t * t + 2);
        }

        /*
        public static float QuintOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2) return QuintOut(t * 2, b, c / 2, d);
            return QuintIn((t * 2) - d, b + c / 2, c / 2, d);
        }
        */

        #endregion
    }
}