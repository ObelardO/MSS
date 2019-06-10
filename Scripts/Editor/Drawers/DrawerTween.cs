using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEditorInternal;

namespace Obel.MSS.Editor
{
    [CustomPropertyDrawer(typeof(Tween))]
    public class DrawerTween : PropertyDrawer
    {

        private static GUIContent contentLabel = new GUIContent("Name");

        public static Tween drawingTween;

        //private static 

        //private ReorderableList tweensReorderableList;

        // Draw the property inside the given rect
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            //EditorGUI.BeginProperty(rect, label, property);

            // Draw label
            //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            //var indent = EditorGUI.indentLevel;
            //EditorGUI.indentLevel = 0;

            EditorGUI.DrawRect(new Rect(rect.x + 2, rect.y + 2, rect.width - 2, rect.height - 6), Color.white * 0.4f);

            // Calculate rects
            Rect nameRect = new Rect(rect.x + 4, rect.y + 4, rect.width - 8 - 30, 16);
            Rect removeButtonRect = new Rect(rect.width + 8, rect.y + 4, 28, 16);

            //Rect delayRect = new Rect(rect.x + 4, rect.y + 22, rect.width - 8, 16);
            //Rect durationRect = new Rect(rect.x + 4, rect.y + 40, rect.width - 8, 16);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), contentLabel);

            if (GUI.Button(removeButtonRect, "-"))
            {
                DrawerState.drawingState.tweens.Remove(drawingTween);
            }

            //EditorGUI.PropertyField(delayRect, property.FindPropertyRelative("delay"), delayLabel);
            //EditorGUI.PropertyField(durationRect, property.FindPropertyRelative("duration"), durationLabel);

            //SerializedProperty tweensProperty = property.FindPropertyRelative("tweens");

            //for (int i = 0; i < tweensProperty.arraySize; i++)
            //    EditorGUI.PropertyField(new Rect(rect.x + 4, rect.y + 58 + 80 * i, rect.width - 8, 80), tweensProperty.GetArrayElementAtIndex(i));

            //Rect tweensRect = new Rect(rect.x + 4, rect.y + 58, rect.width - 8, tweensProperty.arraySize * 18 + 18);

            /*

            EditorGUI.PropertyField(tweensRect, tweensProperty);



            ReorderableList tweensReorderableList = new ReorderableList(property.serializedObject, property.FindPropertyRelative("tweens"), true, true, true, true);


            tweensReorderableList.drawHeaderCallback = (Rect headerRect) =>
            {
                EditorGUI.LabelField(headerRect, "Tweens");
            };

            tweensReorderableList.elementHeightCallback = (int index) =>
            {
                return 80f;
            };

            tweensReorderableList.drawElementCallback = (Rect tweenRect, int index, bool isActive, bool isFocused) =>
            {
                SerializedProperty tweenProperty = tweensReorderableList.serializedProperty.GetArrayElementAtIndex(index);

                EditorGUI.PropertyField(new Rect(tweenRect.x + 16, tweenRect.y, tweenRect.width - 16, tweenRect.height), tweenProperty, true);
            };

            tweensReorderableList.DoList(tweensRect);
            */

            // Set indent back to what it was
            //EditorGUI.indentLevel = indent;

            //EditorGUI.EndProperty();
        }

        /*
        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Tweens");
        }
        */

        /*
        private void DrawState(Rect rect, int index, bool isActive, bool isFocused)
        {

        }

        private float GetElementHeight(int index)
        {
            return 200;
        }
        */

    }
}