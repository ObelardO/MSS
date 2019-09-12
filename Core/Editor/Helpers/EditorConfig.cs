using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor;

namespace Obel.MSS.Editor
{
    internal static class EditorConfig
    {
        #region Subclasses

        public static class Content
        {
            public static GUIContent iconToolbarMinus = EditorGUIUtility.TrIconContent("Toolbar Minus", "Remove from list");
            public static GUIContent iconToolbarPlus = EditorGUIUtility.TrIconContent("Toolbar Plus", "Add to list");
        }

        public static class Sizes
        {
            public const float singleLine = 18;
            public const float offset = 4;

            public static float LineHeight => singleLine + offset;
        }

        public static class Styles
        {
            public static GUIStyle preButton = "RL FooterButton";
            public static GUIStyle Foldout = "Foldout"/* "BoldLabel" */;
            public static GUIStyle miniLabel = "MiniLabel"/*"SearchCancelButton"*//*"SearchTextField"*//*"MiniLabel"*/;

            public static GUIStyle greyMiniLabel = new GUIStyle(miniLabel)
            {
                normal = { textColor = Color.grey}
            };

            /*
            internal static void Init()
            {
                Foldout.fontStyle = FontStyle.Bold;
            }
            */
        }

        public static class Colors
        {
            public static Color red = new Color(1, 0.65f, 0.65f, 1);
            public static Color green = new Color(0.65f, 1, 0.65f, 1);
            public static Color grey = new Color(0.35f, 0.35f, 0.35f, 1);
            public static Color lightGrey = Color.white * 0.65f;
        }

        #endregion
    }
}