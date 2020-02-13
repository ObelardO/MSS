using System;
using UnityEngine;
using Obel.MSS.Data;
using System.Collections.Generic;

namespace Obel.MSS.Base
{ 
    internal sealed class Core : Singleton<Core>
    {
        private struct ProcessingGroup
        {

            public Group Group;

            public override int GetHashCode() => Group.GetHashCode();
        }

        public List<States> StatesComponents = new List<States>();

        public static void SelectState(State state)
        {
            Debug.Log("SELECT STATE: " + state.Name);

            if (Instance.StatesComponents.Contains(state.Group.StatesComponent)) return;

            Instance.StatesComponents.Add(state.Group.StatesComponent);
        }

        public static void SelectState(Group group, string stateName)
        {
            foreach (var state in group.EnabledStates)
            {
                if (state.Name.Equals(stateName, StringComparison.InvariantCultureIgnoreCase))
                {
                    SelectState(state);
                    return;
                }
            }

            Debug.LogWarning($"[MSS] [Core] Object \"{group.GameObject.name}\" doesn't contains \"{stateName}\" state!");
        }
    }
}
 