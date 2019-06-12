using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Obel.MSS.Editor
{

    [CustomPropertyDrawer(typeof(State))]
    public class DrawerState : PropertyDrawer
    {
        private static GUIContent contentLabel = new GUIContent("Name"),
                                  delayLabel = new GUIContent("Delay"),
                                  durationLabel = new GUIContent("Duration"),
                                  testLabel = new GUIContent("Test");

        public static StateEditorValues editorValues;

        private SerializedProperty property;
        private GUIContent label;
        private Rect rect;

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            this.rect = rect;
            this.label = label;
            this.property = property;

            DrawHeader();
            DrawProperties();
        }

        private void DrawHeader()
        {
            Rect rectBackground = new Rect(rect.x, rect.y, rect.width, rect.height - 6);
            EditorGUI.DrawRect(rectBackground, Color.white * 0.4f);

            Rect rectFoldOutBack = new Rect(rect.x, rect.y, rect.width, 20);
            GUI.Box(rectFoldOutBack, GUIContent.none, GUI.skin.box);

            Rect rectStateTabColor = new Rect(rect.x, rect.y, 2, 20);
            Color tabColor = Color.gray;
            if (editorValues.state.isOpenedState) tabColor = HelperEditor.Colors.greenColor;
            if (editorValues.state.isClosedState) tabColor = HelperEditor.Colors.redColor;
            EditorGUI.DrawRect(rectStateTabColor, tabColor);

            Rect rectToggle = new Rect(rect.x + 5, rect.y, 20, 20);

            if (editorValues.state.isDefaultState)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.Toggle(rectToggle, GUIContent.none, true);
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                EditorGUI.PropertyField(rectToggle, property.FindPropertyRelative("_enabled"), GUIContent.none);
            }

            Rect rectFoldout = new Rect(rect.x + 34, rect.y + 2, rect.width - 54, 20);
            editorValues.foldout.target = EditorGUI.Foldout(rectFoldout, editorValues.foldout.target,
                new GUIContent(editorValues.state.name), true, HelperEditor.Styles.Foldout/*, GUI.skin.label*/);
        }

        private void DrawProperties()
        {
            if (editorValues.foldout.faded == 0) return;

            HelperEditor.Colors.PushGUIColor();

            GUI.color *= editorValues.foldout.faded;

            //EditorGUI.BeginProperty(rect, label, property);
            EditorGUI.BeginDisabledGroup(editorValues.foldout.faded < 0.2f || !editorValues.state.enabled);

            Rect layOut = rect;

            //Rect rectName = new Rect(rect.x + 4, rect.y + 26, rect.width - 8, 16);

            layOut = new Rect(layOut.x + 4, layOut.y + 20, layOut.width - 126, 16);
            EditorGUI.LabelField(layOut, "Name", HelperEditor.Styles.greyMiniLabel);

            layOut.x += rect.width - 126;
            EditorGUI.LabelField(layOut, "Delay", HelperEditor.Styles.greyMiniLabel);

            layOut.x += 126 / 2 - 2;
            EditorGUI.LabelField(layOut, "Duration", HelperEditor.Styles.greyMiniLabel);

            layOut = new Rect(rect.x + 4, layOut.y + 18, rect.width - 126 - 2, 16);
            EditorGUI.BeginDisabledGroup(editorValues.state.isDefaultState);
            EditorGUI.PropertyField(layOut, property.FindPropertyRelative("_name"), GUIContent.none);
            EditorGUI.EndDisabledGroup();

            layOut.width = 126 / 2 - 6;
            layOut.x += rect.width - 126 + 2;
            EditorGUI.PropertyField(layOut, property.FindPropertyRelative("delay"), GUIContent.none);

            layOut.x += 126 / 2 - 4;
            EditorGUI.PropertyField(layOut, property.FindPropertyRelative("duration"), GUIContent.none);

            EditorGUI.EndDisabledGroup();

            //EditorGUI.EndProperty();

            HelperEditor.Colors.PullGUIColor();


       

            // EditorGUILayout.EndFadeGroup();
            /*
            EditorGUI.PropertyField(delayRect, property.FindPropertyRelative("delay"), delayLabel);
            EditorGUI.PropertyField(durationRect, property.FindPropertyRelative("duration"), durationLabel);
            */


            /*
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
            */
        }
    }
}