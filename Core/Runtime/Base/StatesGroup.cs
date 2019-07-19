using System;

namespace Obel.MSS
{
    [Serializable]
    public class StatesGroup : Collection<State>
    {
        #region Properties

        public State ClosedState { get { return Get((int)DefaultState.Closed); } }
        public State OpenedState { get { return Get((int)DefaultState.Opened); } }

        #endregion
    }
}