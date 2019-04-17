using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Obel.MSS.Editor
{
    public static class MSSStateGroupEditor
    {
        #region GUI

        public static void OnGUI(MSSStateGroup stateGroup)
        {
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("STATE GROUP", EditorStyles.boldLabel);
                if (GUILayout.Button("x")) MSSBaseEditor.RemoveStateGroups(stateGroup);
            EditorGUILayout.EndHorizontal();

            if (stateGroup == null) return;

            stateGroup.ForEach(state => MSSStateEditor.OnGUI(stateGroup, state));

            if (GUILayout.Button("Add state")) AddState(stateGroup);

            EditorGUILayout.Space();
        }

        #endregion

        #region Collection editor

        public static void AddState(MSSStateGroup stateGroup)
        {
            Undo.RecordObject(stateGroup, "[MSS] Add state");
            stateGroup.Add(MSSBaseEditor.SaveAsset<MSSState>(StateInstanced, "[MSS][State]"));
        }

        private static void StateInstanced(MSSState state)
        {
            state.name = state.stateName;
        }

        public static void RemoveState(MSSStateGroup stateGroup, MSSState state, bool useUndo = true)
        {
            MSSStateEditor.RemoveTweens(state, useUndo);
            if (useUndo) Undo.RecordObject(stateGroup, "[MSS] Remove state");
            stateGroup.Remove(state, false);
            MSSBaseEditor.RemoveAsset(state);
        }

        public static void RemoveStates(MSSStateGroup stateGroup, bool useUndo = true)
        {
            stateGroup.ForEach(state => RemoveState(stateGroup, state, useUndo));
        }

        #endregion
    }
}