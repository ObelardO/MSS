using System;
using System.Collections.Generic;
using UnityEditor;
using Object = UnityEngine.Object;
using Debug = UnityEngine.Debug;

namespace Obel.MSS.Editor
{
    internal static class EditorActions
    {
        #region Properties

        private static List<EditorAction> actions = new List<EditorAction>();

        #endregion

        #region Subclasses

        private struct EditorAction
        {
            public Action action;
            public Object recordeble;
            public string reason;
        }

        #endregion

        #region Public methods

        public static void Add(Action action, Object recordable = null, string reason = null)
        {
            Debug.Log($"[MSS] [Editor] [Actions] Registred: {reason} {recordable?.name ?? "without UNDO"}");
            actions.Add(new EditorAction()
            {
                action = action,
                recordeble = recordable,
                reason = reason ?? "Action"
            });
        }

        public static void Process()
        {
            if (actions.Count == 0) return;

            for (int i = actions.Count - 1; i >= 0; i--)
            {
                if (actions[i].recordeble != null)
                {
                    //Undo.RecordObject(actions[i].recordeble, $"[MSS] {actions[i].reason}");
                    Debug.Log("[MSS] [Editor] [Actions] Undo recording: " + actions[i].recordeble.name);
                    Undo.RegisterCompleteObjectUndo(actions[i].recordeble, $"[MSS] {actions[i].reason}");
                }

                try
                {
                    Debug.Log("[MSS] [Editor] [Actions] Do: " + actions[i].reason);
                    actions[i].action.Invoke();
                }
                catch 
                {
                    Debug.LogWarning("[MSS] [Editor] [Actions] Something wrong with action: " + actions[i].reason);
                }

                Debug.Log("[MSS] [Editor] [Actions] Done.");
                actions.RemoveAt(i);
            }
        }

        public static void Clear() => actions.Clear();

        #endregion
    }
}