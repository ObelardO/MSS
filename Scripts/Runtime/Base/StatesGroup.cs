using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class StatesGroup : DBCollection<State>
    {
        public int objectID;

        public State closedState { get { return Get((int)DefaultState.closed); } }
        public State openedState { get { return Get((int)DefaultState.opened); } }
    }
}