using System;
using UnityEngine;

namespace Obel.MSS.Node
{ 
    internal static class Core
    {
        public static void SelectState(State state)
        {
            Debug.Log("SELECT STATE: " + state.Name);
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

            Debug.LogWarning($"[MSS] [Core] Object \"{group.gameObject.name}\" doesn't contains \"{stateName}\" state!");
        }
    }
}