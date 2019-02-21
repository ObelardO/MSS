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

    public static class MSSEases
    {
        #region Easing

        public static float Linear(float t, float d)
        {
            return t / d;
        }

        public static float NoneZero(float t, float d)
        {
            return 0;
        }

        public static float NoneOne(float t, float d)
        {
            return 1;
        }

        public static float Custom(float t, float d)
        {
            return 0;
        }

        public static float InQuad(float t, float d)
        {
            return (t /= d) * t;
        }

        public static float OutQuad(float t, float d)
        {
            return -(t /= d) * (t - 2);
        }

        public static float InOutQuad(float t, float d)
        {
            if ((t /= d / 2) < 1) return 0.5f * t * t;

            return -0.5f * ((--t) * (t - 2) - 1);
        }

        public static float OutInQuad(float t, float d)
        {
            if (t < d / 2) return OutQuad(t * 2, d);
            return InQuad((t * 2) - d, d);
        }

        public static float InCubic(float t, float d)
        {
            return (t /= d) * t * t;
        }

        public static float OutCubic(float t, float d)
        {
            return (t = t / d - 1) * t * t + 1;
        }

        public static float InOutCubic(float t, float d)
        {
            if ((t /= d / 2) < 1) return 0.5f * t * t * t;
            return 0.5f * ((t -= 2) * t * t + 2);
        }

        public static float OutInCubic(float t, float d)
        {
            if (t < d / 2) return OutCubic(t * 2, d);
            return InCubic((t * 2) - d, d);
        }

        public static float InQuart(float t, float d)
        {
            return (t /= d) * t * t * t;
        }

        public static float OutQuart(float t, float d)
        {
            return -((t = t / d - 1) * t * t * t - 1);
        }

        public static float InOutQuart(float t, float d)
        {
            if ((t /= d / 2) < 1) return 0.5f * t * t * t * t;
            return -0.5f * ((t -= 2) * t * t * t - 2);
        }

        public static float OutInQuart(float t, float d)
        {
            if (t < d / 2) return OutQuart(t * 2, d);
            return InQuart((t * 2) - d, d);
        }

        public static float InQuint(float t, float d)
        {
            return (t /= d) * t * t * t * t;
        }

        public static float OutQuint(float t, float d)
        {
            return ((t = t / d - 1) * t * t * t * t + 1);
        }

        public static float InOutQuint(float t, float d)
        {
            if ((t /= d / 2) < 1) return 0.5f * t * t * t * t * t;
            return 0.5f * ((t -= 2) * t * t * t * t + 2);
        }

        public static float OutInQuint(float t, float d)
        {
            if (t < d / 2) return OutQuint(t * 2, d);
            return InQuint((t * 2) - d, d);
        }

        public static float InSine(float t, float d)
        {
            return -Mathf.Cos(t / d * (Mathf.PI / 2)) + 1;
        }

        public static float OutSine(float t, float d)
        {
            return Mathf.Sin(t / d * (Mathf.PI / 2));
        }

        public static float InOutSine(float t, float d)
        {
            return -0.5f * (Mathf.Cos(Mathf.PI * t / d) - 1);
        }

        public static float OutInSine(float t, float d)
        {
            if (t < d / 2) return OutSine(t * 2, d);
            return InSine((t * 2) - d, d);
        }

        public static float InExpo(float t, float d)
        {
            return (t == 0) ? 0 : Mathf.Pow(2, 10 * (t / d - 1)) - 0.001f;
        }

        public static float OutExpo(float t, float d)
        {
            return (t == d) ? 1 : 1.001f * (-Mathf.Pow(2, -10 * t / d) + 1);
        }

        public static float InOutExpo(float t, float d)
        {
            if (t == 0) return 0;
            if (t == d) return 1;
            if ((t /= d / 2) < 1) return 0.5f * Mathf.Pow(2, 10 * (t - 1)) - 0.0005f;
            return 0.5f * 1.0005f * (-Mathf.Pow(2, -10 * --t) + 2);
        }

        public static float OutInExpo(float t, float d)
        {
            if (t < d / 2) return OutExpo(t * 2, d);
            return InExpo((t * 2) - d, d);
        }

        public static float InCirc(float t, float d)
        {
            return -(Mathf.Sqrt(1 - (t /= d) * t) - 1);
        }

        public static float OutCirc(float t, float d)
        {
            return Mathf.Sqrt(1 - (t = t / d - 1) * t);
        }

        public static float InOutCirc(float t, float d)
        {
            if ((t /= d / 2) < 1) return -0.5f * (Mathf.Sqrt(1 - t * t) - 1);
            return 0.5f * (Mathf.Sqrt(1 - (t -= 2) * t) + 1);
        }

        public static float OutInCirc(float t, float d)
        {
            if (t < d / 2) return OutCirc(t * 2, d);
            return InCirc((t * 2) - d, d);
        }

        public static float InElastic(float t, float d)
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

        public static float OutElastic(float t, float d)
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

        public static float InOutElastic(float t, float d)
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

        public static float OutInElastic(float t, float d)
        {
            if (t < d / 2) return OutElastic(t * 2, d);
            return InElastic((t * 2) - d, d);
        }

        public static float InBack(float t, float d)
        {
            float s = 1.70158f;
            return (t /= d) * t * ((s + 1) * t - s);
        }

        public static float OutBack(float t, float d)
        {
            float s = 1.70158f;
            return (t = t / d - 1) * t * ((s + 1) * t + s) + 1;
        }

        public static float InOutBack(float t, float d)
        {
            float s = 1.70158f;
            if ((t /= d / 2) < 1) return 0.5f * (t * t * (((s *= (1.525f)) + 1) * t - s));
            return 0.5f * ((t -= 2) * t * (((s *= (1.525f)) + 1) * t + s) + 2);
        }

        public static float OutInBack(float t, float d)
        {
            if (t < d / 2) return OutBack(t * 2, d);
            return InBack((t * 2) - d, d);
        }

        public static float InBounce(float t, float d)
        {
            return 1 - OutBounce(d - t, d);
        }

        public static float OutBounce(float t, float d)
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

        public static float InOutBounce(float t, float d)
        {
            if (t < d / 2) return InBounce(t * 2, d) * .5f;
            else return OutBounce(t * 2 - d, d) * .5f + .5f;
        }

        public static float OutInBounce(float t, float d)
        {
            if (t < d / 2) return OutBounce(t * 2, d);
            return InBounce((t * 2) - d, d);
        }

        delegate float EaseDelegate(float t, float d);

        static EaseDelegate[] eases = new EaseDelegate[]
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

        #endregion
    }

    
    public interface IMSSTween
    {
        object id { get; }

        void Update(float time);
    }
    

    public class MSSTween : IMSSTween
    {
        public string name;

        public object id { get; set; }


        //private T _tweenValue;
        //virtual public T tweenValue { get { return _tweenValue; } }

        /*
        private MSSTweenEase _easeB;
        public MSSTweenEase easeA { get { return _easeB; } }

        private MSSTweenEffect _effect;
        public MSSTweenEffect effect { get { return _effect; } }

        private MSSTweenState _state;

        private Component _component;
        public Component component { get { return _component; } }



        private float _currentTime;
        public float currentTime { get { return _currentTime; } }

        private float _duration;
        public float duration { get { return _duration; } }

        private float _delay;
        public float delay { get { return _delay; } }

        public MSSTween(MSSTweenEase ease, MSSTweenEffect effect, T tweenValue, Component component)
        {
            _ease = ease;
            _effect = effect;
            _state = state;

            _component = component;
            _tweenValue = tweenValue;
           
        }
        */

        public virtual void Update(float time)
        {

        }
    }

    
    public class MSSTweenPosition : MSSTween
    {
        Vector3 tweenValue;

        public MSSTweenPosition(GameObject gameObject)
        {
            this.tweenValue = gameObject.transform.localPosition;
        }
    }

    public class MSSTweenRotation : MSSTween
    {
        Quaternion tweenValue;

        public MSSTweenRotation(GameObject gameObject)
        {
            this.tweenValue = gameObject.transform.localRotation;
        }
    }


    public class MSSTweenScale : MSSTween
    {
        Vector3 tweenValue;

        public MSSTweenScale(GameObject gameObject)
        {
            this.tweenValue = gameObject.transform.localScale;
        }
    }

    public struct MSSTweenTransformContainer
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        public MSSTweenTransformContainer(GameObject gameObject)
        {
            position = gameObject.transform.localPosition;
            rotation = gameObject.transform.localRotation;
            scale = gameObject.transform.localScale;
        }
    }

    public class MSSTweenTransform : MSSTween
    {
        MSSTweenTransformContainer tweenValue;
        Transform transform;

        public MSSTweenTransform(MSSTweenTransformContainer tweenValue)
        {
            this.tweenValue = tweenValue;
        }
    }


    [System.Serializable]
    public class MSSTweenData
    {
        private GameObject gameObject;

        private MSSTweenEffect _tweenEffect;
        public MSSTweenEffect tweenEffect { get { return _tweenEffect; }
            set
            {
                switch (value)
                {
                    case MSSTweenEffect.position: tween = new MSSTweenPosition(gameObject); break;
                    case MSSTweenEffect.rotation: tween = new MSSTweenRotation(gameObject); break;
                    case MSSTweenEffect.scale: tween = new MSSTweenScale(gameObject); break;
                    case MSSTweenEffect.transform: tween = new MSSTweenTransform(new MSSTweenTransformContainer(gameObject)); break;

                }
            }
        }

        private MSSTween tween;

        public MSSTweenData(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}
