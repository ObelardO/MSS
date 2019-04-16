using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Obel.MSS;

namespace Obel.MSS.Editor
{

    public class MSSCollectionEditor<T> where T : MSSCollectionItem
    {
        void Remove(T item)
        {

        }
    }

    public static class MSSStateEditor
    {
        #region GUI

        public static void OnGUI(MSSStateGroup stateGroup, MSSState state)
        {
            EditorGUILayout.BeginHorizontal();
                MSSEditorUtils.DrawGenericProperty(ref state.stateName, "name", state);
                if (GUILayout.Button("x")) MSSStateGroupEditor.RemoveState(stateGroup, state);
            EditorGUILayout.EndHorizontal();

            if (state == null) return;

            state.ForEach(tween => MSSTweenEditor.OnGUI(state, tween));

            SerializedProperty std = new SerializedObject(state).FindProperty("items");

            for (int i = 0; i < state.Count; i++) EditorGUILayout.PropertyField(std.GetArrayElementAtIndex(i));

            EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Add position")) AddTween<MSSTweenPosition>(state);
                if (GUILayout.Button("Add rotation")) AddTween<MSSTweenRotation>(state);
            EditorGUILayout.EndHorizontal();
        }

        #endregion

        #region Collection editor

        public static void AddTween<T>(MSSState state) where T : MSSTween
        {
            Undo.RecordObject(state, "[MSS] Add a new tween");
            state.Add(MSSBaseEditor.SaveAsset<T>(OnTweenInstanced, "[MSS][Tween]"));
        }

        private static void OnTweenInstanced(MSSTween tween)
        {

        }

        public static void RemoveTween<T>(T tween, MSSState state, bool useUndo = true) where T : MSSTween
        {
            if (useUndo) Undo.RecordObject(state, "[MSS] Remove a tween");
            state.Remove(tween, false);
            MSSBaseEditor.RemoveAsset(tween);
        }

        public static void RemoveTweens(MSSState stateData, bool useUndo = true)
        {
            stateData.ForEach(tweenData => RemoveTween(tweenData, stateData, useUndo));
        }

        #endregion
    }
}