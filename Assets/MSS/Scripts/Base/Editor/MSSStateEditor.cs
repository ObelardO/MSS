using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Obel.MSS;
using System;
using UnityEditor.Graphs;

namespace Obel.MSS.Editor
{
    public static class MSSStateEditor
    {
        #region GUI

        private static MSSState currentState;

        public static void OnGUI(MSSStateGroup stateGroup, MSSState state)
        {
            currentState = state;

            EditorGUILayout.BeginVertical(GUI.skin.box);

            EditorGUILayout.BeginHorizontal();
                MSSEditorUtils.DrawGenericProperty(ref state.stateName, "name", state);
                if (GUILayout.Button("x")) MSSStateGroupEditor.RemoveState(stateGroup, state);
            EditorGUILayout.EndHorizontal();

            if (state == null) return;

            state.ForEach(tween => MSSTweenEditor.OnGUI(state, tween));

            EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Add position")) AddTween<MSSTweenPosition>(state);
                if (GUILayout.Button("Add rotation")) AddTween<MSSTweenRotation>(state);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            GUILayout.Space(3);

            /*
            GUILayout.BeginHorizontal();
            //GUILayout.FlexibleSpace();
            EditorGUI.BeginDisabledGroup(!_activeParent);
            _searchString = string.Empty;
            _searchString = GUILayout.TextField(_searchString, GUI.skin.FindStyle("SearchTextField"));
            var buttonStyle = _searchString == string.Empty ? GUI.skin.FindStyle("SearchCancelButtonEmpty") : GUI.skin.FindStyle("SearchCancelButton");
            if (GUILayout.Button("Add tween"))//, buttonStyle))
            {
                // Remove focus if cleared
                _searchString = string.Empty;
                GUI.FocusControl(null);
                _activeParent = true;
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.EndHorizontal();

            if (_activeParent)
            {
                //_className = _searchString;
                ListGUI();
            }
            else
            {
                //NewScriptGUI();
            }

            ListGUI();

            EditorGUILayout.Space();
            */
        }

        #endregion


        #region ListMenu
        /*
        private static string _searchString = string.Empty;
        private static bool _activeParent = true;
        private static Vector2 _scrollPosition;
        private static Styles _styles;
        */
        /*
        static void ListGUI()
        {
            if (_styles == null)
            {
                _styles = new Styles();
            }



            var rect = GUILayoutUtility.GetLastRect();
            rect.x = +1f;
            rect.y = 30f;
            rect.height -= 30f;
            rect.width -= 2f;
            GUILayout.BeginArea(rect);

            EditorGUI.DrawRect(rect, Color.red);

            rect = GUILayoutUtility.GetRect(10f, 25f);
            GUI.Label(rect, _searchString == string.Empty ? "Behaviour" : "Search");
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            var scripts = Resources.FindObjectsOfTypeAll<MonoScript>();
            var searchString = _searchString.ToLower();

                var buttonRect = GUILayoutUtility.GetRect(16f, 20f, GUILayout.ExpandWidth(true));
                if (GUI.Button(buttonRect, "Add position")) AddTween<MSSTweenPosition>(currentState);

                buttonRect = GUILayoutUtility.GetRect(16f, 20f, GUILayout.ExpandWidth(true));
                if (GUI.Button(buttonRect, "Add rotation")) AddTween<MSSTweenRotation>(currentState);
            
                        
            var rect2 = GUILayoutUtility.GetRect(16f, 20f, GUILayout.ExpandWidth(true));
            if (GUI.Button(rect2, "New Script"))
            {
                _activeParent = false;
            }

            GUI.Label(new Rect((float)((double)rect2.x + (double)rect2.width - 13.0), rect2.y + 4f, 13f, 13f), "");

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
        */


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