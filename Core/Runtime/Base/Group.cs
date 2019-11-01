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

        public Group(GameObject gameObject)
        {
            Create();
            Create();

            this.gameObject = gameObject;
        }
    }
}