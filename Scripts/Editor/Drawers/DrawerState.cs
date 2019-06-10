using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Obel.MSS.Editor
{
    [CustomPropertyDrawer(typeof(State))]
    public class DrawerState : PropertyDrawer
    {
        private static GUIContent contentLabel = new GUIContent("Name"),
                                  delayLabel = new GUIContent("Delay"),
                                  durationLabel = new GUIContent("Duration");

        public static State drawingState;

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            SerializedProperty tweensProperty = property.FindPropertyRelative("tweens");


            Rect nameRect = new Rect(rect.x + 4, rect.y + 4, rect.width - 8, 16);
            Rect delayRect = new Rect(rect.x + 4, rect.y + 22, rect.width - 8, 16);
            Rect durationRect = new Rect(rect.x + 4, rect.y + 40, rect.width - 8, 16);
            Rect addButtonRect = new Rect(rect.x + 4, rect.y + 78 + 80 * tweensProperty.arraySize, rect.width - 8, 20);

            EditorGUI.DrawRect(new Rect(rect.x + 2, rect.y + 2, rect.width - 2, rect.height - 6), Color.white * 0.4f);
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), contentLabel);
            EditorGUI.PropertyField(delayRect, property.FindPropertyRelative("delay"), delayLabel);
            EditorGUI.PropertyField(durationRect, property.FindPropertyRelative("duration"), durationLabel);

            for (int i = 0; i < tweensProperty.arraySize; i++)
            {
                DrawerTween.drawingTween = drawingState.tweens[i];

                EditorGUI.PropertyField(new Rect(rect.x + 4, rect.y + 58 + 80 * i, rect.width - 8, 80),
                    tweensProperty.GetArrayElementAtIndex(i));
            }

            if (GUI.Button(addButtonRect, "Add Tween"))
            {
                StatesBehaviour behaviour = property.serializedObject.targetObject as StatesBehaviour;
                Debug.Log(behaviour.name);

                drawingState.tweens.Add(new Tween());
            }
        }
    }
}