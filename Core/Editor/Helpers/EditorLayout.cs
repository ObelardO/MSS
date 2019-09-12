using System;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal static class EditorLayout
    {
        #region Properties

        private static Vector2 startPosition;
        private static Rect rect;
        private static float FixedWidth = 30;
        private static float SingleLine => EditorConfig.Sizes.singleLine;
        private static float Offset => EditorConfig.Sizes.offset;
        private static Color storedColor;

        #endregion

        #region Public methods

        public static void SetPosition(float x, float y)
        {
            SetPosition(new Vector2(x, y));
        }

        public static void SetPosition(Vector2 position)
        {
            rect = new Rect(position.x + Offset, position.y, FixedWidth, SingleLine);
            startPosition = position;
        }

        public static void SetSize(Vector2 size)
        {
            rect.width = size.x;
            rect.height = size.y;
        }

        public static void SetWidth(float width)
        {
            FixedWidth = width;
        }

        public static void Control(Action<Rect> drawCallback)
        {
            Control(new Vector2(FixedWidth, SingleLine), drawCallback);
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
            rect.width = size.x;
            rect.height = size.y;
            drawCallback(rect);
            rect.x += Offset + rect.width;
        }

        public static void Space()
        {
            Space(Offset);
        }

        public static void Space(float height)
        {
            rect.x = startPosition.x + Offset;
            rect.y += height + rect.height;
        }

        public static void PushColor()
        {
            storedColor = GUI.color;
        }

        public static void PullColor()
        {
            GUI.color = storedColor;
        }

        #endregion
    }
}

