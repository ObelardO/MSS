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
        public static void OnGUI(MSSStateData stateData)
        {
            MSSEditorUtils.DrawGenericProperty(ref stateData.stateName, "name", stateData);

            stateData.tweensData.ToList().ForEach(tweenData => MSSTweenDataEditor.OnGUI(stateData, tweenData, new Vector3()));

            if (GUILayout.Button("Add position")) AddTweenData<MSSTweenDataPosition>(stateData);
            if (GUILayout.Button("Add rotation")) AddTweenData<MSSTweenDataRotation>(stateData);
            if (GUILayout.Button("Delete state")) MSSDataBaseEditor.RemoveStateData(stateData);

            EditorGUILayout.Space();
        }


        public static void RemoveTweensData(MSSStateData stateData)
        {
            stateData.tweensData.ToList().ForEach(t => RemoveTweenData(t, stateData, false));
        }

        public static void AddTweenData<T>(MSSStateData stateData) where T : MSSTweenDataBase
        {
            Undo.RecordObject(stateData, "[MSS] Add a new tween");
            stateData.tweensData.Add(MSSDataBaseEditor.SaveAsset<T>(OnTweenDataInstanced, "[MSS][TWEEN]"));
        }

        private static void OnTweenDataInstanced(MSSTweenDataBase tween)
        {
            //tween.parentStateData = this;
        }

        public static void RemoveTweenData<T>(T tween, MSSStateData stateData, bool useRectording = true) where T : MSSTweenDataBase
        {
            if (useRectording) Undo.RecordObject(stateData, "[MSS] Remove a tween");
            stateData.tweensData.Remove(tween);
            MSSDataBaseEditor.RemoveAsset(tween);
        }
    }
}