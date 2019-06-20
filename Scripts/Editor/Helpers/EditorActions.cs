using System;
using System.Collections.Generic;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Obel.MSS.Editor
{
    internal static class EditorActions
    {
        private static List<EditorAction> actions = new List<EditorAction>();

        private class EditorAction
        {
            public Action action;
            public Object recordeble;
            public string reason;
        }

        public static void Add(Action action, Object recordable = null, string reason = null)
        {
            actions.Add(new EditorAction()
            {
                action = action,
                recordeble = recordable,
                reason = reason
            });
        }

        public static void Process()
        {
            if (actions.Count == 0) return;

            for (int i = actions.Count - 1; i >= 0; i--)
            {
                if (actions[i].recordeble != null)
                    Undo.RecordObject(actions[i].recordeble, actions[i].reason);

                actions[i].action.Invoke();
                actions.RemoveAt(i);
            }
        }

        public static void Clear()
        {
            actions.Clear();
        }
    }
}


