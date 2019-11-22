using System;
using UnityEngine;
using UnityEditor;

namespace Obel.MSS.Editor
{
    public static class EditorLayout
    {
        #region Properties

        private static Vector2 _startPosition;
        private static Rect _rect;
        private static float _fixedWidth = 30;
        private static float SingleLine => EditorConfig.Sizes.SingleLine;
        private static float Offset => EditorConfig.Sizes.Offset;
        private static Color _storedColor;

        #endregion

        #region Public methods

        public static void SetPosition(float x, float y)
        {
            SetPosition(new Vector2(x, y));
        }

        public static void SetPosition(Vector2 position)
        {
            _rect = new Rect(position.x + Offset, position.y, _fixedWidth, SingleLine);
            _startPosition = position;
        }

        public static void SetSize(Vector2 size)
        {
            _rect.width = size.x;
            _rect.height = size.y;
        }

        public static void SetWidth(float width)
        {
            _fixedWidth = width;
        }

        public static void Control(Action<Rect> drawCallback)
        {
            Control(new Vector2(_fixedWidth, SingleLine), drawCallback);
        }

        public static void Control(float width, Action<Rect> drawCallback)
        {
            Control(new Vector2(width, SingleLine), drawCallback);
        }

        public static void Control(float width, float height, Action<Rect> drawCallback)
        {
            Control(new Vector2(width, height), drawCallback);
        }

        public static void Control(Vector2 size, Action<Rect> drawCallback)
        {
            _rect.width = size.x;
            _rect.height = size.y;
            drawCallback(_rect);
            _rect.x += Offset + _rect.width;
        }

        public static void Space()
        {
            Space(Offset);
        }

        public static void Space(float height)
        {
            _rect.x = _startPosition.x + Offset;
            _rect.y += height + _rect.height;
        }

        public static void PushColor()
        {
            _storedColor = GUI.color;
        }

        public static void PullColor()
        {
            GUI.color = _storedColor;
        }

        #endregion

        #region Generic field methods

        public static Enum PropertyField(Rect rect, Enum value, Func<Rect, GUIContent, Enum, Enum> drawFunc,
            Action onChange = null, GUIContent content = null)
        {
            if (content == null) content = GUIContent.none;

            var tempValue = value;
            tempValue = drawFunc(rect, content, tempValue);

            if (value.Equals(tempValue)) return value;

            onChange?.Invoke();
            return tempValue;
        }

        public static T PropertyField<T>(Rect rect, T value, Func<Rect, GUIContent, T, T> drawFunc,
            Action onChange = null, GUIContent content = null) where T : struct
        {
            if (content == null) content = GUIContent.none;

            var tempValue = value;
            tempValue = drawFunc(rect, content, tempValue);

            if (value.Equals(tempValue)) return value;

            onChange?.Invoke();
            return tempValue;
        }

        // REF-version propertyField 
        public static void PropertyField<T>(Rect rect, ref T value, Func<Rect, GUIContent, T, T> drawFunc,
            Action onChange = null, GUIContent content = null) where T : struct
        {
            value = PropertyField(rect, value, drawFunc, onChange, content);
        }

        // propertyField with auto-layout
        public static T PropertyField<T>(T value, Func<Rect, GUIContent, T, T> drawFunc,
            Action onChange = null, GUIContent content = null) where T : struct
        {
            Control(r => value = PropertyField(r, value, drawFunc, onChange, content));
            return value;
        }

        // REF-version propertyField with auto-layout
        public static void PropertyField<T>(ref T value, Func<Rect, GUIContent, T, T> drawFunc,
            Action onChange = null, GUIContent content = null) where T : struct
        {
            value = PropertyField(value, drawFunc, onChange, content);
        }

        #endregion

        #region String field methods

        public static string PropertyField(Rect rect, string value, Action onChange = null, GUIContent content = null) 
        {
            if (content == null) content = GUIContent.none;

            var tempValue = value;
            tempValue = EditorGUI.TextField(rect, content, tempValue);

            if (value == null || value.Equals(tempValue)) return value;

            onChange?.Invoke();
            return tempValue;
        }

        public static void PropertyField(Rect rect, ref string value, Action onChange = null, GUIContent content = null)
        {
            value = PropertyField(rect, value, onChange, content);
        }

        public static string PropertyField(string value, Action onChange = null, GUIContent content = null)
        {
            Control(r => value = PropertyField(r, value, onChange, content));
            return value;
        }

        public static void PropertyField(ref string value, Action onChange = null, GUIContent content = null)
        {
            value = PropertyField(value, onChange, content);
        }

        #endregion
    }
}
