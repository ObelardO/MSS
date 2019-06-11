using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Obel.MSS.Editor
{
    internal static class HelperEditor
    {
        public static class Content
        {
            public static GUIContent iconToolbarMinus = EditorGUIUtility.TrIconContent("Toolbar Minus", "Remove from list");
        }

        public static class Styles
        {
            public static GUIStyle preButton = "RL FooterButton";
            public static GUIStyle FoldOut = "BoldLabel";
        }

        public static class Colors
        {
            //public static Color foldOut = 
        }

        [InitializeOnLoadMethod]
        public static void Init()
        {
            //Styles.preButton = ;

            //Styles.FoldOut = "box";

            //Styles.FoldOut = new GUIStyle(GUI.skin.label);
            //Styles.FoldOut.fontStyle = FontStyle.Bold;



        }
    }
}


