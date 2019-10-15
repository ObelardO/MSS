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

        private static readonly List<EditorAction> Actions = new List<EditorAction>();

        #endregion

        #region Subclasses

        private struct EditorAction
        {
            public Action Action { get; }
            public Object Recordable { get; }
            public string Reason { get; }

            public EditorAction(Action action, Object recordable = null, string reason = null)
            {
                Action = action;
                Recordable = recordable;
                Reason = reason ?? "Action";
            }
        }

        #endregion

        #region Public methods

        public static void Add(Action action, Object recordable = null, string reason = null)
        {
            Debug.Log($"[MSS] [Editor] [Actions] Registered: {reason} {recordable?.name ?? "without UNDO"}");
            Actions.Add(new EditorAction(action, recordable, reason));
        }

        public static void Process()
        {
            if (Actions.Count == 0) return;

            for (var i = Actions.Count - 1; i >= 0; i--)
            {
                if (Actions[i].Recordable != null)
                {
                    //Undo.RecordObject(Actions[i].Recordable, $"[MSS] {Actions[i].Reason}");
                    Debug.Log("[MSS] [Editor] [Actions] Undo recording: " + Actions[i].Recordable.name);
                    Undo.RegisterCompleteObjectUndo(Actions[i].Recordable, $"[MSS] {Actions[i].Reason}");
                }

                try
                {
                    Debug.Log("[MSS] [Editor] [Actions] Do: " + Actions[i].Reason);
                    Actions[i].Action.Invoke();
                }
                catch
                {
                    Debug.LogWarning("[MSS] [Editor] [Actions] Something wrong with action: " + Actions[i].Reason);
                }

                Debug.Log("[MSS] [Editor] [Actions] Done.");
                Actions.RemoveAt(i);
            }
        }

        public static void Clear() => Actions.Clear();

        #endregion
    }
}