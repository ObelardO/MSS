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

        [SerializeField] public List<IMSSTween> tweens = new List<IMSSTween>();

        /*
        /// <summary>Count of tweens list</summary>
        [SerializeField] public int count { get { return tweens.Count; } }

        /// <summary>Tweens list</summary>
        public IMSSTween this[int i] { get { return i >= 0 && i < count ? tweens[i] : null; } }

        /// <summary>Last added tween</summary>
        public IMSSTween last { get { return count == 0 ? null : this[count - 1]; } }
        */

        public MSSState(GameObject gameObject, string name = "new state")
        {
            this.gameObject = gameObject;
            this.name = name;
        }
    }
}