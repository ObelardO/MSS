using System;
using System.Collections.Generic;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Obel.MSS.Editor
{
    internal static class EditorActions
    {
        #region Properties

        private static List<EditorAction> actions = new List<EditorAction>();

        #endregion

        #region Subclasses

        private class EditorAction
        {
            public Action action;
            public Object recordeble;
            public string reason;
        }

        #endregion

        #region Public methods

        public static void Add(Action action, Object recordable = null, string reason = null)
        {
            UnityEngine.Debug.Log("Register action: " + reason + " " + recordable?.name ?? "null");
            actions.Add(new EditorAction()
            {
                action = action,
                recordeble = recordable,
                reason = string.Concat("[MSS] ", reason ?? "Action")
            });
        }

        public static void Process()
        {
            if (actions.Count == 0) return;

            for (int i = actions.Count - 1; i >= 0; i--)
            {
                //if (actions[i].recordeble is Object)
                //    Undo.RecordObject(actions[i].recordeble, actions[i].reason);

                actions[i].action.Invoke();
                actions.RemoveAt(i);
            }
        }

        public static void Clear()
        {
            actions.Clear();
        }

        #endregion
    }
}