using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEditor;
using System;
using Object = UnityEngine.Object;

namespace Obel.MSS.Editor
{
    public static class MSSEditorUtils
    {

        public static void DrawMessageBox(string message, MessageType type = MessageType.Info)
        {
            EditorGUILayout.HelpBox(message, type);
        }

        /*
        #region Properties via reflections

        private delegate void SetValue<T>(T value);
        private delegate T GetValue<T>();

        public static void DrawProperty<T>(T obj, string propertyName)
        {
            if (!PropertyIsValid(obj, propertyName))
            {
                MSSEditorUtils.DrawMessageBox("invalid property: " + propertyName, MessageType.Warning);
                return;
            }

            MSSEditorUtils.DrawMessageBox("Valid!", MessageType.Info);
        }

        private static bool PropertyIsValid<T>(T obj, string propertyName)
        {
            Type type = typeof(T);
            PropertyInfo property = type.GetProperty(propertyName);
            return property != null;
        }

        #endregion
        */

        #region Generic properties drawer

        public static void DrawGenericProperty<T>(ref T propertyValue)
        {
            DrawGenericProperty(ref propertyValue, Color.white, null, null);
        }

        public static void DrawGenericProperty<T>(ref T propertyValue, Object recordingObject = null)
        {
            DrawGenericProperty(ref propertyValue, Color.white, null, recordingObject);
        }

        public static void DrawGenericProperty<T>(ref T propertyValue, Color propertyColor, Object recordingObject = null)
        {
            DrawGenericProperty(ref propertyValue, propertyColor, null, recordingObject);
        }

        public static void DrawGenericProperty<T>(ref T propertyValue, string propertyName)
        {
            DrawGenericProperty(ref propertyValue, Color.white, new GUIContent(propertyName), null);
        }

        public static void DrawGenericProperty<T>(ref T propertyValue, string propertyName, Object recordingObject = null)
        {
            DrawGenericProperty(ref propertyValue, Color.white, new GUIContent(propertyName), recordingObject);
        }

        public static void DrawGenericProperty<T>(ref T propertyValue, GUIContent propertyName = null, Object recordingObject = null)
        {
            DrawGenericProperty(ref propertyValue, Color.white, propertyName, recordingObject);
        }

        public static void DrawGenericProperty<T>(ref T propertyValue, Color propertyColor, GUIContent propertyName = null, Object recordingObject = null)
        {
            System.Type propertyType = typeof(T);

            EditorGUI.BeginChangeCheck();
            GUI.changed = false;
            if (propertyName != null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel(propertyName);
            }
            GUI.color = propertyColor;

            object value = propertyValue;
            object enteredValue = propertyValue;

            T displayedValue = (T)value;

            if (propertyType == typeof(float)) enteredValue = EditorGUILayout.FloatField((float)(object)displayedValue);
            else if (propertyType == typeof(bool)) enteredValue = EditorGUILayout.Toggle((bool)(object)displayedValue);
            else if (propertyType == typeof(int)) enteredValue = EditorGUILayout.IntField((int)(object)displayedValue);
            else if (propertyType == typeof(string)) enteredValue = EditorGUILayout.TextField((string)(object)displayedValue);
            else if (propertyType == typeof(Vector2)) enteredValue = EditorGUILayout.Vector2Field(string.Empty, (Vector2)(object)displayedValue);
            else if (propertyType == typeof(Vector3)) enteredValue = EditorGUILayout.Vector3Field(string.Empty, (Vector3)(object)displayedValue);
            else if (propertyType == typeof(Vector4)) enteredValue = EditorGUILayout.Vector4Field(string.Empty, (Vector4)(object)displayedValue);
            else if (propertyType == typeof(AnimationCurve)) enteredValue = EditorGUILayout.CurveField((AnimationCurve)(object)displayedValue);
            //else if (propertyType == typeof(Transform)) enteredValue = EditorGUILayout.ObjectField(string.Empty, (Transform)(object)propertyValue, typeof(Transform));

            if (EditorGUI.EndChangeCheck() || enteredValue != (object)displayedValue)
            {
                if (recordingObject != null) Undo.RecordObject(recordingObject, "[MSS] property");
                propertyValue = (T)enteredValue;
            }

            if (propertyName != null) EditorGUILayout.EndHorizontal();
            GUI.color = Color.white;
        }

        #endregion
    }
}