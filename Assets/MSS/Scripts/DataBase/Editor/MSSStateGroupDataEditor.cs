using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Obel.MSS.Editor
{
    public static class MSSStateGroupDataEditor
    {
        #region GUI

        public static void OnGUI(MSSStateGroupData stateGroupData)
        {
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("STATE GROUP", EditorStyles.boldLabel);
                if (GUILayout.Button("x")) MSSDataBaseEditor.RemoveStateGroupsData(stateGroupData);
            EditorGUILayout.EndHorizontal();

            if (stateGroupData == null) return;

            stateGroupData.ForEach(stateData =>
            {
                MSSStateDataEditor.OnGUI(stateGroupData, stateData);
                EditorGUILayout.Space();
            });

            if (GUILayout.Button("Add state")) AddStateData(stateGroupData);

            EditorGUILayout.Space();
        }

        #endregion

        #region Collection editor

        public static void AddStateData(MSSStateGroupData stateGroupData)
        {
            Undo.RecordObject(stateGroupData, "[MSS] Add state");
            stateGroupData.Add(MSSDataBaseEditor.SaveAsset<MSSStateData>(StateDataInstanced, "[MSS][State]"));
        }

        private static void StateDataInstanced(MSSStateData stateData)
        {
            stateData.name = stateData.stateName;
        }

        public static void RemoveStateData(MSSStateGroupData stateGroupData, MSSStateData stateData, bool useUndo = true)
        {
            MSSStateDataEditor.RemoveTweensData(stateData, useUndo);
            if (useUndo) Undo.RecordObject(stateGroupData, "[MSS] Remove state");
            stateGroupData.Remove(stateData, false);
            MSSDataBaseEditor.RemoveAsset(stateData);
        }

        public static void RemoveStatesData(MSSStateGroupData stateGroupData, bool useUndo = true)
        {
            stateGroupData.ForEach(stateData => RemoveStateData(stateGroupData, stateData, useUndo));
        }

        #endregion
    }
}