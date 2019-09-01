namespace Obel.MSS
{
    [System.Serializable]
    public class StatesGroup : Collection<State>
    {
        #region Properties

        public State ClosedState { get => Get((int)DefaultState.Closed); }
        public State OpenedState { get => Get((int)DefaultState.Opened); }

        #endregion
    }
}