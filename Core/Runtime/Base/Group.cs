using System;
using UnityEngine;
using Obel.MSS.Base;
using Unity.Entities;

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
    
        #endregion
    }
}