using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
using System;
using Obel.MSS;
using UnityEditor.AnimatedValues;

namespace Obel.MSS.Editor
{

    public static class MSSEditorUtils
    {

        #region Colors

        public static readonly Color halfBlack = new Color(0f, 0f, 0f, 0.5f);

        #endregion

        #region Styles

        public static readonly GUIStyle styleBox = GUI.skin.box,
                                        stylehelpBox = EditorStyles.helpBox,
                                        styleLabel = GUI.skin.label,
                                        styleFoldout = EditorStyles.foldout;

        #endregion

        #region Callbacks

        private delegate void PanelElementCallbackDelegate();

        #endregion

        #region Drawing

        public static void DrawListPanel<T>(List<T> list, AnimBool state, string name, Action<T> onDrawItem, bool drawAddButton = false, Action onAddItem = null) where T : class
        {
            GUILayout.Space(3);

            EditorGUILayout.BeginVertical(styleBox);

            EditorGUILayout.BeginHorizontal();
            state.target = EditorGUILayout.Foldout(state.target, "   " + name, true, GUI.skin.label);
            if (drawAddButton && GUILayout.Button("Add new")) onAddItem();
            EditorGUILayout.EndHorizontal();

            if (state.faded > 0)
            {
                EditorGUILayout.BeginFadeGroup(state.faded);

                GUI.color = Color.white * state.faded;

                foreach (T item in list) onDrawItem(item);

                EditorGUILayout.EndFadeGroup();
            }

            EditorGUILayout.EndVertical();
        }

        public static void DrawPanel(AnimBool state, string name, Action onDraw, Action onDrowHeader)
        {
            DrawPanel(state, name, onDraw, null, null, onDrowHeader);
        }

        public static void DrawPanel(AnimBool state, string name, Action onDraw, GUIStyle foldStyle = null, GUIStyle backStyle = null, Action onDrowHeader = null)
        {
            GUILayout.Space(3);

            EditorGUILayout.BeginVertical(backStyle == null ? styleBox : backStyle);

            EditorGUILayout.BeginHorizontal();
            state.target = EditorGUILayout.Foldout(state.target, "   " + name, true, foldStyle == null ? styleLabel : foldStyle);
            if (onDrowHeader != null) onDrowHeader();
            EditorGUILayout.EndHorizontal();

            if (state.faded > 0)
            {
                EditorGUILayout.BeginFadeGroup(state.faded);

                GUI.color = Color.white * state.faded;

                onDraw();

                EditorGUILayout.EndFadeGroup();
            }

            EditorGUILayout.EndVertical();
        }

        public static void BeginBlackVertical()
        {
            GUI.backgroundColor = halfBlack;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUI.backgroundColor = Color.white;
        }

        #endregion

        public static T GetObjectBySerializedProperty<T>(FieldInfo fieldInfo, SerializedProperty property) where T : class
        {
            var obj = fieldInfo.GetValue(property.serializedObject.targetObject);
            if (obj == null) { return null; }

            T actualObject = null;
            if (obj.GetType().IsArray)
            {
                var index = Convert.ToInt32(new string(property.propertyPath.Where(c => char.IsDigit(c)).ToArray()));
                actualObject = ((T[])obj)[index];
            }
            else
            {
                actualObject = obj as T;
            }
            return actualObject;
        }
    }
}
