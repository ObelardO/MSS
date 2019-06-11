using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Obel.MSS.Editor
{
    using Object = UnityEngine.Object;

    public static class UserActions
    {
        private static List<UserAction> actions = new List<UserAction>();

        private class UserAction
        {
            public Action action;
            public Object recordeble;
            public string reason;
        }

        public static void Add(Action action, Object recordable = null, string reason = null)
        {
            actions.Add(new UserAction()
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


