using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace Obel.MSS.Editor
{
    internal static class EditorConfig
    {
        public static class Content
        {
            public static GUIContent iconToolbarMinus = EditorGUIUtility.TrIconContent("Toolbar Minus", "Remove from list");
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

            internal static void Init()
            {
                Foldout.fontStyle = FontStyle.Bold;
            }
        }

        public static class Colors
        {
            //public static Color foldOut = 

            private static Color storedGUIColor = Color.white;

            public static Color redColor = new Color(1, 0.65f, 0.65f, 1);
            public static Color greenColor = new Color(0.65f, 1, 0.65f, 1);

            public static void PushGUIColor()
            {
                storedGUIColor = GUI.color;
            }

            public static void PullGUIColor()
            {
                GUI.color = storedGUIColor;
            }
        }

        [InitializeOnLoadMethod]
        public static void Init()
        {
            //Styles.Init();

            //Styles.preButton = ;

            //Styles.FoldOut = "box";

            //Styles.FoldOut = new GUIStyle(GUI.skin.label);
            //Styles.FoldOut.fontStyle = FontStyle.Bold;



        }

        /*
        public static object GetTargetObjectOfProperty(SerializedProperty prop)
        {
            if (prop == null) return null;

            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }
            return obj;
        }

        private static object GetValue_Imp(object source, string name)
        {
            if (source == null)
                return null;
            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                    return f.GetValue(source);

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                    return p.GetValue(source, null);

                type = type.BaseType;
            }
            return null;
        }

        private static object GetValue_Imp(object source, string name, int index)
        {
            var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
            if (enumerable == null) return null;
            var enm = enumerable.GetEnumerator();
            //while (index-- >= 0)
            //    enm.MoveNext();
            //return enm.Current;

            for (int i = 0; i <= index; i++)
            {
                if (!enm.MoveNext()) return null;
            }
            return enm.Current;
        }
        */
    }
}


