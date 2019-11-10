using UnityEngine;
using UnityEditor;

namespace Obel.MSS.Editor
{
    public static class EditorConfig
    {
        #region Subclasses

        public static class Content
        {
            public static GUIContent IconToolbarMinus = EditorGUIUtility.TrIconContent("Toolbar Minus", "Remove from list");
            public static GUIContent IconToolbarPlus = EditorGUIUtility.TrIconContent("Toolbar Plus", "Add to list");
            public static GUIContent IconRecord = new GUIContent(EditorResources.IconRecord);
            public static GUIContent IconReturn = new GUIContent(EditorResources.IconReturn);
        }

        public static class Sizes
        {
            public const float SingleLine = 18;
            public const float Offset = 4;

            public static float LineHeight => SingleLine + Offset;
        }

        public static class Styles
        {
            public static GUIStyle PreButton = "RL FooterButton";
            public static GUIStyle Foldout = "Foldout";
            public static GUIStyle MiniLabel = "MiniLabel";

            public static GUIStyle IconButton = new GUIStyle(PreButton)
            { 
                padding = new RectOffset(4, 4, 4, 4)
            };

            public static GUIStyle GreyMiniLabel = new GUIStyle(MiniLabel)
            {
                normal = { textColor = Color.grey }
            };
        }

        public static class Colors
        {
            public static Color Red = new Color(1, 0.65f, 0.65f, 1);
            public static Color Green = new Color(0.65f, 1, 0.65f, 1);
            public static Color Grey = new Color(0.35f, 0.35f, 0.35f, 1);
            public static Color LightGrey = Color.white * 0.65f;
        }

        #endregion
    }
}