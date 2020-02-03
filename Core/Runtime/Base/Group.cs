using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obel.MSS.Data;
using Obel.MSS;

namespace Obel.MSS
{
    [Serializable]
    public class Group : Collection<State>
    {
        #region Properties

        public State ClosedState => Get((int)DefaultState.Closed);
        public State OpenedState => Get((int)DefaultState.Opened);

        public IReadOnlyList<State> States => Items;
        public IReadOnlyList<State> EnabledStates => EnabledItems;

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

        #endregion
    }
}