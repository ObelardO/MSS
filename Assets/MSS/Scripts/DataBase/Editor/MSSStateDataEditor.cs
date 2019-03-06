using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Obel.MSS;

namespace Obel.MSS.Editor
{
    public static class MSSStateDataEditor
    {
        #region GUI

        public static void OnGUI(MSSStateGroupData stateGroupData, MSSStateData stateData)
        {
            EditorGUILayout.BeginHorizontal();
                MSSEditorUtils.DrawGenericProperty(ref stateData.stateName, "name", stateData);
                if (GUILayout.Button("x")) MSSStateGroupDataEditor.RemoveStateData(stateGroupData, stateData);
            EditorGUILayout.EndHorizontal();

            if (stateData == null) return;

            stateData.ForEach(tweenData => MSSTweenDataEditor.OnGUI(stateData, tweenData));

            SerializedObject  ssd = new SerializedObject(stateData);
            Debug.Log(ssd.FindProperty("items"));
            for (int i = 0; i < stateData.Count; i++)
            {
                EditorGUILayout.PropertyField(ssd.FindProperty("items").GetArrayElementAtIndex(i));
            }

            EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Add position")) AddTweenData<MSSTweenDataPosition>(stateData);
                if (GUILayout.Button("Add rotation")) AddTweenData<MSSTweenDataRotation>(stateData);
            EditorGUILayout.EndHorizontal();
        }

        #endregion

        #region Collection editor

        public static void AddTweenData<T>(MSSStateData stateData) where T : MSSTweenData
        {
            Undo.RecordObject(stateData, "[MSS] Add a new tween");
            stateData.Add(MSSDataBaseEditor.SaveAsset<T>(OnTweenDataInstanced, "[MSS][Tween]"));
        }

        private static void OnTweenDataInstanced(MSSTweenData tween)
        {

        }

        public static void RemoveTweenData<T>(T tween, MSSStateData stateData, bool useUndo = true) where T : MSSTweenData
        {
            if (useUndo) Undo.RecordObject(stateData, "[MSS] Remove a tween");
            stateData.Remove(tween, false);
            MSSDataBaseEditor.RemoveAsset(tween);
        }

        public static void RemoveTweensData(MSSStateData stateData, bool useUndo = true)
        {
            stateData.ForEach(tweenData => RemoveTweenData(tweenData, stateData, useUndo));
        }

        #endregion
    }
}