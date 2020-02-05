using System;
using UnityEngine;

namespace Obel.MSS.Node
{ 
    internal static class Core
    {
        private struct ProcessingGroup
        {
            public Group Group;

            public override int GetHashCode() => Group.gameObject.GetHashCode();
        }

        public static void SelectState(State state)
        {
            Debug.Log("SELECT STATE: " + state.Name);
        }

        /*
        public override void OnInit()
        {
            int TweenTypedDetected = 0;

            foreach (var typedTween in State.TypedTweens)
            {
                if (typedTween.ContainsKey(GetType())) TweenTypedDetected++;
            }

            Debug.Log("ALLREADY CONTAINS " + GetType() + "   " + TweenTypedDetected);

            if (TweenTypedDetected == 0)
            {
                var typedTween = new Dictionary<Type, Tween>();
                typedTween.Add(GetType(), this);

                State.TypedTweens.Add(typedTween);
            }
        }
        */

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
 