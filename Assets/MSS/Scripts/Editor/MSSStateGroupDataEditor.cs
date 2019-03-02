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
            EditorGUILayout.LabelField("STATE GROUP", EditorStyles.boldLabel);

            stateGroupData.statesData.ToList().ForEach(stateData =>
            {
                MSSStateDataEditor.OnGUI(stateData);
                OnStateDataGUI(stateGroupData, stateData);

                EditorGUILayout.Space();
            });

            if (GUILayout.Button("Add state")) AddStateData(stateGroupData);

            EditorGUILayout.Space();
        }

        #endregion

        #region Collection editor

        private static void OnStateDataGUI(MSSStateGroupData stateGroupData, MSSStateData stateData)
        {
            if (GUILayout.Button("Delete state")) MSSStateGroupDataEditor.RemoveStateData(stateGroupData, stateData);
        }

        public static void AddStateData(MSSStateGroupData stateGroupData)
        {
            Undo.RecordObject(stateGroupData, "[MSS] Add state");
            stateGroupData.statesData.Add(MSSDataBaseEditor.SaveAsset<MSSStateData>(StateDataInstanced, "[MSS][State]"));
        }

        private static void StateDataInstanced(MSSStateData stateData)
        {
            stateData.name = stateData.stateName;
        }

        public static void RemoveStateData(MSSStateGroupData stateGroupData, MSSStateData stateData)
        {
            Undo.RecordObject(stateGroupData, "[MSS] Remove state");
            stateGroupData.statesData.Remove(stateData);
            MSSDataBaseEditor.RemoveAsset(stateData);
        }

        #endregion
    }
}