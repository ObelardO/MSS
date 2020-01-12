using System;
using UnityEngine;
using Obel.MSS.Base;

namespace Obel.MSS
{
    [Serializable]
    public class Group : Collection<State>
    {
        #region Properties

        public State ClosedState => Get((int)DefaultState.Closed);
        public State OpenedState => Get((int)DefaultState.Opened);

        [field: SerializeField]
        public GameObject gameObject { private set; get; }

        #endregion

        #region Init

        public Group(GameObject gameObject)
        {
            CreateState();
            CreateState();

            this.gameObject = gameObject;
        }

        #endregion

        #region Public methods

        public State CreateState() => Create();

        public void Select(string stateName)
        {
            //ForEachEnabled(s => { if (s.Name.Equals(stateName, StringComparison.InvariantCultureIgnoreCase)) Select(s); });
            foreach (var state in Items)
                if (state.Enabled && state.Name.Equals(stateName, StringComparison.InvariantCultureIgnoreCase)) state.Select();
        }

        public void Select(State state) => state.Select();

        public void Open() => Select(OpenedState);

        public void Close() => Select(ClosedState);

        #endregion
    }
}