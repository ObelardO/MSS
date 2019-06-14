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
        #region Properties

        private static readonly GUIContent contentLabel = new GUIContent("Name"),
                                           delayLabel = new GUIContent("Delay"),
                                           durationLabel = new GUIContent("Duration"),
                                           testLabel = new GUIContent("Test");

        public static StateEditorValues editorValues;

        private SerializedProperty property;
        private GUIContent label;
        private Rect rect;

        #endregion

        #region Inspector

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            this.rect = rect;
            this.label = label;
            this.property = property;

            editorValues.serializedState.Update();

            DrawHeader();
            DrawProperties();

            editorValues.serializedState.ApplyModifiedProperties();
        }

        private void DrawHeader()
        {
            Rect rectBackground = new Rect(rect.x, rect.y, rect.width, rect.height - 6);
            EditorGUI.DrawRect(rectBackground, Color.white * 0.4f);

            Rect rectFoldOutBack = new Rect(rect.x, rect.y, rect.width, 20);
            GUI.Box(rectFoldOutBack, GUIContent.none, GUI.skin.box);

            Rect rectStateTabColor = new Rect(rect.x, rect.y, 2, 20);
            Color tabColor = Color.gray;
            if (editorValues.state.IsOpenedState) tabColor = EditorConfig.Colors.greenColor;
            if (editorValues.state.IsClosedState) tabColor = EditorConfig.Colors.redColor;
            EditorGUI.DrawRect(rectStateTabColor, tabColor);

            Rect rectToggle = new Rect(rect.x + 5, rect.y, 20, 20);

            if (editorValues.state.IsDefaultState)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.Toggle(rectToggle, GUIContent.none, true);
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                EditorGUI.PropertyField(rectToggle, editorValues.serializedState.FindProperty("s_Enabled"), GUIContent.none);
            }

            Rect rectFoldout = new Rect(rect.x + 34, rect.y + 2, rect.width - 54, 20);
            editorValues.foldout.target = EditorGUI.Foldout(rectFoldout, editorValues.foldout.target,
                new GUIContent(editorValues.state.Name + " | " + editorValues.state.ID), true, EditorConfig.Styles.Foldout);

            if (!editorValues.state.IsDefaultState)
            {
                if (GUI.Button(new Rect(rect.width + 5, rect.y + 1, 30, 20), EditorConfig.Content.iconToolbarMinus, EditorConfig.Styles.preButton))
                {
                    State removingState = editorValues.state;
                    StatesGroup removingStateGroup = (StatesGroup)removingState.Parent;

                    EditorActions.Add(() =>
                    {
                        removingStateGroup.Remove(removingState, false);

                        EditorAssets.Remove(removingState);
                        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(removingStateGroup));

                        StateEditorValues.Reorder(removingStateGroup.items);
                    },
                    removingStateGroup, "[MSS] Remove state");
                }
            }

        }

        private void DrawProperties()
        {
            if (editorValues.foldout.faded == 0) return;

            EditorConfig.Colors.PushGUIColor();

            GUI.color *= editorValues.foldout.faded;

            //EditorGUI.BeginProperty(rect, label, property);
            EditorGUI.BeginDisabledGroup(editorValues.foldout.faded < 0.2f || !editorValues.state.Enabled);

            LayOutRect = new Rect(rect.x + LayOutOffset, rect.y + 20, rect.width - LayOutOffset * 2, 300);

            float timeFieldWidth = 54;
            float nameFieldWidth = rect.width - timeFieldWidth * 2 - LayOutOffset * 4;
            GUIStyle FieldStyle = EditorConfig.Styles.greyMiniLabel;

            LayOutControl(nameFieldWidth,
                () => { EditorGUI.LabelField(LayOutRect, "Name", FieldStyle); });

            LayOutControl(timeFieldWidth,
                () => { EditorGUI.LabelField(LayOutRect, "Delay", FieldStyle); });

            LayOutControl(timeFieldWidth,
                () => { EditorGUI.LabelField(LayOutRect, "Duration", FieldStyle); });

            LayOutSpace();

            LayOutControl(nameFieldWidth, () =>
            {
                EditorGUI.BeginDisabledGroup(editorValues.state.IsDefaultState);
                EditorGUI.PropertyField(LayOutRect, editorValues.serializedState.FindProperty("s_Name"), GUIContent.none);
                editorValues.state.name = string.Format("[State] {0}", editorValues.state.Name);
                EditorGUI.EndDisabledGroup();
            });

            LayOutControl(timeFieldWidth, () =>
            {
                EditorGUI.PropertyField(LayOutRect, editorValues.serializedState.FindProperty("s_Delay"), GUIContent.none);
            });

            LayOutControl(timeFieldWidth, () =>
            {
                EditorGUI.PropertyField(LayOutRect, editorValues.serializedState.FindProperty("s_Duration"), GUIContent.none);
            });

            EditorGUI.EndDisabledGroup();

            //EditorGUI.EndProperty();

            EditorConfig.Colors.PullGUIColor();
        }

        #endregion

        #region LayOut helper

        private Rect LayOutRect;
        private readonly float LayOutOffset = 4;

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

        #endregion
    }
}