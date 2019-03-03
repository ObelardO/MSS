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

        public static void OnGUI(MSSStateData stateData)
        {
            MSSEditorUtils.DrawGenericProperty(ref stateData.stateName, "name", stateData);

            stateData.ForEach(tweenData => MSSTweenDataEditor.OnGUI(stateData, tweenData, new Vector3()));

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

        public static void RemoveTweenData<T>(T tween, MSSStateData stateData, bool useRectording = true) where T : MSSTweenData
        {
            if (useRectording) Undo.RecordObject(stateData, "[MSS] Remove a tween");
            stateData.Remove(tween, false);
            MSSDataBaseEditor.RemoveAsset(tween);
        }

        public static void RemoveTweensData(MSSStateData stateData)
        {
            stateData.ForEach(tweenData => RemoveTweenData(tweenData, stateData));
        }

        #endregion
    }
}