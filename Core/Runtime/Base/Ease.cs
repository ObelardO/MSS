using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.AccessControl;

namespace Obel.MSS
{
    public static class Ease
    {


     
        public delegate void EaseDelegate(float t, float b, float c, float d);

        //private EaseDelegate linEaseDelegate = Linear;

        

        /*public static EaseDelegate Get(string easePath)
        {
            //return Linear;

        }
    */

        private class EaseInfo
        {
            public Func<float, float, float, float, float> ease;
            public string path;

            public EaseInfo(Func<float, float, float, float, float> ease, string path)
            {
                this.ease = ease;
                this.path = path;
            }
        }



        //private static Dictionary<string, EaseInfo> eases = new Dictionary<string, EaseInfo>();

        private static List<EaseInfo> eases;

        public static void Add(Func<float, float, float, float, float> ease, string path)
        {
            eases.Add(new EaseInfo(ease, path));

            Debug.Log("Added ease " + path);
        }

        public static Func<float, float, float, float, float> Get(string path)
        {
            return eases.Where(e => e.path.Equals(path)).FirstOrDefault()?.ease;
        }

        public static Func<float, float, float, float, float> GetFirst()
        {
            return eases.FirstOrDefault()?.ease;
        }

        public static float Linear(float t, float b, float c, float d)
        {
            return c * t / d + b;
        }

        /*

        public static float NoneZero(float t, float b, float c, float d)
        {
            return 0;
        }

        public static float NoneOne(float t, float b, float c, float d)
        {
            return 1;
        }

        public static float Custom(float t, float b, float c, float d)
        {
            return 0;
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
        

        public static float CubicIn(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * t + b;
        }

        public static float CubicOut(float t, float b, float c, float d)
        {
            return c * ((t = t / d - 1) * t * t + 1) + b;
        }

        public static float CubicInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t + 2) + b;
        }

        public static float CubicOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2) return CubicOut(t * 2, b, c / 2, d);
            return CubicIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        public static float QuartIn(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * t * t + b;
        }

        public static float QuartOut(float t, float b, float c, float d)
        {
            return -c * ((t = t / d - 1) * t * t * t - 1) + b;
        }

        public static float QuartInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t * t + b;
            return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
        }

        public static float QuartOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2) return QuartOut(t * 2, b, c / 2, d);
            return QuartIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        public static float QuintIn(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * t * t * t + b;
        }

        public static float QuintOut(float t, float b, float c, float d)
        {
            return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
        }

        public static float QuintInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
        }

        public static float QuintOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2) return QuintOut(t * 2, b, c / 2, d);
            return QuintIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        public static float SineIn(float t, float b, float c, float d)
        {
            return -c * Mathf.Cos(t / d * (Mathf.PI / 2)) + c + b;
        }

        public static float SineOut(float t, float b, float c, float d)
        {
            return c * Mathf.Sin(t / d * (Mathf.PI / 2)) + b;
        }

        public static float SineInOut(float t, float b, float c, float d)
        {
            return -c / 2 * (Mathf.Cos(Mathf.PI * t / d) - 1) + b;
        }

        public static float SineOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2) return SineOut(t * 2, b, c / 2, d);
            return SineIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        public static float ExpoIn(float t, float b, float c, float d)
        {
            return (t == 0) ? b : c * Mathf.Pow(2, 10 * (t / d - 1)) + b - c * 0.001f;
        }

        public static float ExpoOut(float t, float b, float c, float d)
        {
            return (t == d) ? b + c : c * 1.001f * (-Mathf.Pow(2, -10 * t / d) + 1) + b;
        }

        public static float ExpoInOut(float t, float b, float c, float d)
        {
            if (t == 0) return b;
            if (t == d) return b + c;
            if ((t /= d / 2) < 1) return c / 2 * Mathf.Pow(2, 10 * (t - 1)) + b - c * 0.0005f;
            return c / 2 * 1.0005f * (-Mathf.Pow(2, -10 * --t) + 2) + b;
        }

        public static float ExpoOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2) return ExpoOut(t * 2, b, c / 2, d);
            return ExpoIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        public static float CircIn(float t, float b, float c, float d)
        {
            return -c * (Mathf.Sqrt(1 - (t /= d) * t) - 1) + b;
        }

        public static float CircOut(float t, float b, float c, float d)
        {
            return c * Mathf.Sqrt(1 - (t = t / d - 1) * t) + b;
        }

        public static float CircInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1) return -c / 2 * (Mathf.Sqrt(1 - t * t) - 1) + b;
            return c / 2 * (Mathf.Sqrt(1 - (t -= 2) * t) + 1) + b;
        }

        public static float CircOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2) return CircOut(t * 2, b, c / 2, d);
            return CircIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        public static float ElasticIn(float t, float b, float c, float d)
        {
            if (t == 0) return b;
            if ((t /= d) == 1) return b + c;
            float p = d * .3f;
            float s = 0;
            float a = 0;
            if (a == 0f || a < Mathf.Abs(c))
            {
                a = c;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(c / a);
            }
            return -(a * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
        }

        public static float ElasticOut(float t, float b, float c, float d)
        {
            if (t == 0) return b;
            if ((t /= d) == 1) return b + c;
            float p = d * .3f;
            float s = 0;
            float a = 0;
            if (a == 0f || a < Mathf.Abs(c))
            {
                a = c;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(c / a);
            }
            return (a * Mathf.Pow(2, -10 * t) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) + c + b);
        }

        public static float ElasticInOut(float t, float b, float c, float d)
        {
            if (t == 0) return b;
            if ((t /= d / 2) == 2) return b + c;
            float p = d * (.3f * 1.5f);
            float s = 0;
            float a = 0;
            if (a == 0f || a < Mathf.Abs(c))
            {
                a = c;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(c / a);
            }
            if (t < 1) return -.5f * (a * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
            return a * Mathf.Pow(2, -10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) * .5f + c + b;
        }

        public static float ElasticOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2) return ElasticOut(t * 2, b, c / 2, d);
            return ElasticIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        public static float BackIn(float t, float b, float c, float d)
        {
            float s = 1.70158f;
            return c * (t /= d) * t * ((s + 1) * t - s) + b;
        }

        public static float BackOut(float t, float b, float c, float d)
        {
            float s = 1.70158f;
            return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
        }

        public static float BackInOut(float t, float b, float c, float d)
        {
            float s = 1.70158f;
            if ((t /= d / 2) < 1) return c / 2 * (t * t * (((s *= (1.525f)) + 1) * t - s)) + b;
            return c / 2 * ((t -= 2) * t * (((s *= (1.525f)) + 1) * t + s) + 2) + b;
        }

        public static float BackOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2) return BackOut(t * 2, b, c / 2, d);
            return BackIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        public static float BounceIn(float t, float b, float c, float d)
        {
            return c - BounceOut(d - t, 0, c, d) + b;
        }

        public static float BounceOut(float t, float b, float c, float d)
        {
            if ((t /= d) < (1 / 2.75f))
            {
                return c * (7.5625f * t * t) + b;
            }
            else if (t < (2 / 2.75f))
            {
                return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
            }
            else if (t < (2.5f / 2.75f))
            {
                return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
            }
            else
            {
                return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
            }
        }

        public static float InOutBounce(float t, float b, float c, float d)
        {
            if (t < d / 2) return BounceIn(t * 2, 0, c, d) * .5f + b;
            else return BounceOut(t * 2 - d, 0, c, d) * .5f + c * .5f + b;
        }

        public static float OutInBounce(float t, float b, float c, float d)
        {
            if (t < d / 2) return BounceOut(t * 2, b, c / 2, d);
            return BounceIn((t * 2) - d, b + c / 2, c / 2, d);
        }
        */
    }

}