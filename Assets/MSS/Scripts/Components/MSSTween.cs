using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    public enum MSSTweenEase
    {
        Linear, NoneZero, NoneOne, Custom,
        InQuad, OutQuad, InOutQuad, OutInQuad,
        InCubic, OutCubic, InOutCubic, OutInCubic,
        InQuart, OutQuart, InOutQuart, OutInQuart,
        InQuint, OutQuint, InOutQuint, OutInQuint,
        InSine, OutSine, InOutSine, OutInSine,
        InExpo, OutExpo, InOutExpo, OutInExpo,
        InCirc, OutCirc, InOutCirc, OutInCirc,
        InElastic, OutElastic, InOutElastic, OutInElastic,
        InBack, OutBack, InOutBack, OutInBack,
        InBounce, OutBounce, InOutBounce, OutInBounce
    }

    public enum MSSTweenState
    {
        closed,
        opening,
        closing
    }

    public enum MSSTweenEffect
    {
        position, rotation, scale, transform
    }



    [System.Serializable]
    public abstract class MSSTween
    {
        //string name { get; }

        //MSSState state { get; set; }

        public int a;

        //public string name { get { return "tween"; } }

        public MSSState state { get; set; }
    }

    [System.Serializable]
    public class MSSTweenPosition : MSSTween
    {
        //public string name { get { return state.gameObject.name; } }

        //public MSSState state { get; set; }

        public Vector3 tweenValue;

        public MSSTweenPosition(MSSState state)
        {
            this.state = state;
            this.tweenValue = state.gameObject.transform.localPosition;
        }
    }

    public static class MSSTweenEases
    {
        #region Easing

        private static float Linear(float t, float d)
        {
            return t / d;
        }

        private static float NoneZero(float t, float d)
        {
            return 0;
        }

        private static float NoneOne(float t, float d)
        {
            return 1;
        }

        private static float Custom(float t, float d)
        {
            return 0;
        }

        private static float InQuad(float t, float d)
        {
            return (t /= d) * t;
        }

        private static float OutQuad(float t, float d)
        {
            return -(t /= d) * (t - 2);
        }

        private static float InOutQuad(float t, float d)
        {
            if ((t /= d / 2) < 1) return 0.5f * t * t;

            return -0.5f * ((--t) * (t - 2) - 1);
        }

        private static float OutInQuad(float t, float d)
        {
            if (t < d / 2) return OutQuad(t * 2, d);
            return InQuad((t * 2) - d, d);
        }

        private static float InCubic(float t, float d)
        {
            return (t /= d) * t * t;
        }

        private static float OutCubic(float t, float d)
        {
            return (t = t / d - 1) * t * t + 1;
        }

        private static float InOutCubic(float t, float d)
        {
            if ((t /= d / 2) < 1) return 0.5f * t * t * t;
            return 0.5f * ((t -= 2) * t * t + 2);
        }

        private static float OutInCubic(float t, float d)
        {
            if (t < d / 2) return OutCubic(t * 2, d);
            return InCubic((t * 2) - d, d);
        }

        private static float InQuart(float t, float d)
        {
            return (t /= d) * t * t * t;
        }

        private static float OutQuart(float t, float d)
        {
            return -((t = t / d - 1) * t * t * t - 1);
        }

        private static float InOutQuart(float t, float d)
        {
            if ((t /= d / 2) < 1) return 0.5f * t * t * t * t;
            return -0.5f * ((t -= 2) * t * t * t - 2);
        }

        private static float OutInQuart(float t, float d)
        {
            if (t < d / 2) return OutQuart(t * 2, d);
            return InQuart((t * 2) - d, d);
        }

        private static float InQuint(float t, float d)
        {
            return (t /= d) * t * t * t * t;
        }

        private static float OutQuint(float t, float d)
        {
            return ((t = t / d - 1) * t * t * t * t + 1);
        }

        private static float InOutQuint(float t, float d)
        {
            if ((t /= d / 2) < 1) return 0.5f * t * t * t * t * t;
            return 0.5f * ((t -= 2) * t * t * t * t + 2);
        }

        private static float OutInQuint(float t, float d)
        {
            if (t < d / 2) return OutQuint(t * 2, d);
            return InQuint((t * 2) - d, d);
        }

        private static float InSine(float t, float d)
        {
            return -Mathf.Cos(t / d * (Mathf.PI / 2)) + 1;
        }

        private static float OutSine(float t, float d)
        {
            return Mathf.Sin(t / d * (Mathf.PI / 2));
        }

        private static float InOutSine(float t, float d)
        {
            return -0.5f * (Mathf.Cos(Mathf.PI * t / d) - 1);
        }

        private static float OutInSine(float t, float d)
        {
            if (t < d / 2) return OutSine(t * 2, d);
            return InSine((t * 2) - d, d);
        }

        private static float InExpo(float t, float d)
        {
            return (t == 0) ? 0 : Mathf.Pow(2, 10 * (t / d - 1)) - 0.001f;
        }

        private static float OutExpo(float t, float d)
        {
            return (t == d) ? 1 : 1.001f * (-Mathf.Pow(2, -10 * t / d) + 1);
        }

        private static float InOutExpo(float t, float d)
        {
            if (t == 0) return 0;
            if (t == d) return 1;
            if ((t /= d / 2) < 1) return 0.5f * Mathf.Pow(2, 10 * (t - 1)) - 0.0005f;
            return 0.5f * 1.0005f * (-Mathf.Pow(2, -10 * --t) + 2);
        }

        private static float OutInExpo(float t, float d)
        {
            if (t < d / 2) return OutExpo(t * 2, d);
            return InExpo((t * 2) - d, d);
        }

        private static float InCirc(float t, float d)
        {
            return -(Mathf.Sqrt(1 - (t /= d) * t) - 1);
        }

        private static float OutCirc(float t, float d)
        {
            return Mathf.Sqrt(1 - (t = t / d - 1) * t);
        }

        private static float InOutCirc(float t, float d)
        {
            if ((t /= d / 2) < 1) return -0.5f * (Mathf.Sqrt(1 - t * t) - 1);
            return 0.5f * (Mathf.Sqrt(1 - (t -= 2) * t) + 1);
        }

        private static float OutInCirc(float t, float d)
        {
            if (t < d / 2) return OutCirc(t * 2, d);
            return InCirc((t * 2) - d, d);
        }

        private static float InElastic(float t, float d)
        {
            if (t == 0) return 0;
            if ((t /= d) == 1) return 1;
            float p = d * .3f;
            float s = 0;
            float a = 0;
            if (a == 0f || a < 1)
            {
                a = 1;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(1 / a);
            }
            return -(a * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p));
        }

        private static float OutElastic(float t, float d)
        {
            if (t == 0) return 0;
            if ((t /= d) == 1) return 1;
            float p = d * .3f;
            float s = 0;
            float a = 0;
            if (a == 0f || a < 1)
            {
                a = 1;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(1 / a);
            }
            return a * Mathf.Pow(2, -10 * t) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) + 1;
        }

        private static float InOutElastic(float t, float d)
        {
            if (t == 0) return 0;
            if ((t /= d / 2) == 2) return 1;
            float p = d * (.3f * 1.5f);
            float s = 0;
            float a = 0;
            if (a == 0f || a < 1)
            {
                a = 1;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(1 / a);
            }
            if (t < 1) return -.5f * (a * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p));
            return a * Mathf.Pow(2, -10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) * .5f + 1;
        }

        private static float OutInElastic(float t, float d)
        {
            if (t < d / 2) return OutElastic(t * 2, d);
            return InElastic((t * 2) - d, d);
        }

        private static float InBack(float t, float d)
        {
            float s = 1.70158f;
            return (t /= d) * t * ((s + 1) * t - s);
        }

        private static float OutBack(float t, float d)
        {
            float s = 1.70158f;
            return (t = t / d - 1) * t * ((s + 1) * t + s) + 1;
        }

        private static float InOutBack(float t, float d)
        {
            float s = 1.70158f;
            if ((t /= d / 2) < 1) return 0.5f * (t * t * (((s *= (1.525f)) + 1) * t - s));
            return 0.5f * ((t -= 2) * t * (((s *= (1.525f)) + 1) * t + s) + 2);
        }

        private static float OutInBack(float t, float d)
        {
            if (t < d / 2) return OutBack(t * 2, d);
            return InBack((t * 2) - d, d);
        }

        private static float InBounce(float t, float d)
        {
            return 1 - OutBounce(d - t, d);
        }

        private static float OutBounce(float t, float d)
        {
            if ((t /= d) < (1 / 2.75f))
            {
                return 7.5625f * t * t;
            }
            else if (t < (2 / 2.75f))
            {
                return 7.5625f * (t -= (1.5f / 2.75f)) * t + .75f;
            }
            else if (t < (2.5f / 2.75f))
            {
                return 7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f;
            }
            else
            {
                return 7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f;
            }
        }

        private static float InOutBounce(float t, float d)
        {
            if (t < d / 2) return InBounce(t * 2, d) * .5f;
            else return OutBounce(t * 2 - d, d) * .5f + .5f;
        }

        private static float OutInBounce(float t, float d)
        {
            if (t < d / 2) return OutBounce(t * 2, d);
            return InBounce((t * 2) - d, d);
        }

        #endregion

        private delegate float EaseDelegate(float t, float d);

        private static EaseDelegate[] eases = new EaseDelegate[]
        {
            Linear, NoneZero, NoneOne, Custom,
            InQuad, OutQuad, InOutQuad, OutInQuad,
            InCubic, OutCubic, InOutCubic, OutInCubic,
            InQuart, OutQuart,  InOutQuart, OutInQuart,
            InQuint, OutQuint, InOutQuint, OutInQuint,
            InSine, OutSine, InOutSine, OutInSine,
            InExpo, OutExpo, InOutExpo, OutInExpo,
            InCirc, OutCirc, InOutCirc, OutInCirc,
            InElastic, OutElastic, InOutElastic, OutInElastic,
            InBack, OutBack, InOutBack, OutInBack,
            InBounce, OutBounce,  InOutBounce, OutInBounce
        };

        public static float EaseValue(float time, float duration, MSSTweenEase tweenType) { return eases[(int)tweenType](time, duration); }
    }
}
