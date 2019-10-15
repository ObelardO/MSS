using System;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal static class EditorLayout
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
    }
}

