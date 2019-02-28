using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [System.Serializable]
    public class MSSState
    {
        public string name;
        public GameObject gameObject;

        [SerializeField] public List<MSSTween> tweens = new List<MSSTween>();
        [SerializeField] public MSSTween tween;

        /// <summary>Count of tweens list</summary>
        public int count { get { return tweens.Count; } }

        /// <summary>Tweens list</summary>
        public MSSTween this[int i] { get { return i >= 0 && i < count ? tweens[i] : null; } }

        /// <summary>Last added tween</summary>
        public MSSTween last { get { return count == 0 ? null : this[count - 1]; } }

        /*
        public void InitTweensList()
        {
            
        }
        */

        public void AddTween()
        {
            tweens.Add(new MSSTweenPosition(this) as MSSTween);
        }

        public MSSState(GameObject gameObject, string name = "new state")
        {
            this.gameObject = gameObject;
            this.name = name;

            //InitTweensList();
        }
    }
}