using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [System.Serializable, ExecuteInEditMode, DisallowMultipleComponent, AddComponentMenu("MSS/Item")]
    public class MSSItem : MonoBehaviour
    {
        /// <summary>List of all contained states in this item</summary>
        public List<MSSState> states = new List<MSSState>();

        /// <summary>Count of states list</summary>
        public int count { get { return states.Count; } }

        /// <summary>States list</summary>
        public MSSState this[int i] { get { return i >= 0 && i < count ? states[i] : null; } }

        /// <summary>Last added state</summary>
        public MSSState last { get { return count == 0 ? null : this[count - 1]; } }

        public void AddState()
        {
            states.Add(new MSSState(this));
        }

        public bool RemoveState(MSSState removingState)
        {
            foreach(MSSState state in states) if (state == removingState) { states.Remove(removingState); return true; }
            return false;
        }
    }
}
