using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class StatesGroup : DBCollection<State>
    {
        #region Properties

        public State ClosedState { get { return Get((int)DefaultState.Closed); } }
        public State OpenedState { get { return Get((int)DefaultState.Opened); } }

        #endregion
    }
}