using System;
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
            if (editorValues.state.isOpenedState) tabColor = EditorConfig.Colors.greenColor;
            if (editorValues.state.isClosedState) tabColor = EditorConfig.Colors.redColor;
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
                

                /* TODO
                EditorGUI.PropertyField(rectToggle, property.FindPropertyRelative("_enabled"), GUIContent.none);
                */
            }

            Rect rectFoldout = new Rect(rect.x + 34, rect.y + 2, rect.width - 54, 20);
            editorValues.foldout.target = EditorGUI.Foldout(rectFoldout, editorValues.foldout.target,
                new GUIContent(editorValues.state.stateName + " | " + editorValues.state.id), true,
                EditorConfig.Styles.Foldout /*, GUI.skin.label*/);

        }

        private Rect LayOutRect;
        private float LayOutOffset = 4;

        private void LayOutControl(float width, Action control)
        {
            LayOutControl(new Vector2(width, 16), control);
        }

        private void LayOutControl(float width, float height, Action control)
        {
            LayOutControl(new Vector2(width, height), control);
        }

        private void LayOutControl(Vector2 size, Action control)
        {
            LayOutRect.width = size.x;
            LayOutRect.height = size.y;
            control();
            LayOutRect.x += LayOutOffset + LayOutRect.width;
        }

        private void LayOutSpace()
        {
            LayOutSpace(LayOutOffset);
        }

        private void LayOutSpace(float height)
        {
            LayOutRect.x = rect.x + LayOutOffset;
            LayOutRect.y += height + LayOutRect.height;
        }

        private void DrawProperties()
        {
            if (editorValues.foldout.faded == 0) return;

            EditorConfig.Colors.PushGUIColor();

            GUI.color *= editorValues.foldout.faded;

            //EditorGUI.BeginProperty(rect, label, property);
            EditorGUI.BeginDisabledGroup(editorValues.foldout.faded < 0.2f || !editorValues.state.enabled);

            LayOutRect = new Rect(rect.x + LayOutOffset, rect.y + 20, rect.width - LayOutOffset * 2, 300);

            float timeFieldWidth = 54;
            float nameFieldWidth = rect.width - timeFieldWidth * 2 - LayOutOffset * 4;
            GUIStyle FieldStyle = EditorConfig.Styles.greyMiniLabel;

            /* TODO

            LayOutControl(nameFieldWidth,
                () => { EditorGUI.LabelField(LayOutRect, "Name", FieldStyle); });

            LayOutControl(timeFieldWidth,
                () => { EditorGUI.LabelField(LayOutRect, "Delay", FieldStyle); });

            LayOutControl(timeFieldWidth,
                () => { EditorGUI.LabelField(LayOutRect, "Duration", FieldStyle); });

            LayOutSpace();

            LayOutControl(nameFieldWidth, () =>
            {
                EditorGUI.BeginDisabledGroup(editorValues.state.isDefaultState);
                EditorGUI.PropertyField(LayOutRect, property.FindPropertyRelative("_name"), GUIContent.none);
                EditorGUI.EndDisabledGroup();
            });

            LayOutControl(timeFieldWidth, () =>
            {
                EditorGUI.PropertyField(LayOutRect, property.FindPropertyRelative("delay"), GUIContent.none);
            });

            LayOutControl(timeFieldWidth, () =>
            {
                EditorGUI.PropertyField(LayOutRect, property.FindPropertyRelative("duration"), GUIContent.none);
            });


            */

            EditorGUI.EndDisabledGroup();

            //EditorGUI.EndProperty();



            EditorConfig.Colors.PullGUIColor();
        }
    }
}



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


        /*
        }
    }
}

    */