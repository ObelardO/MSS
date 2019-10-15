using System;
using Obel.MSS.Base;

namespace Obel.MSS
{
    [Serializable]
    public class StatesGroup : Collection<State>
    {
        #region Properties

        public State ClosedState => Get((int)DefaultState.Closed);
        public State OpenedState => Get((int)DefaultState.Opened);

        #endregion
    }
}