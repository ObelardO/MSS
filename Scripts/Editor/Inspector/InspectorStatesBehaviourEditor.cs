using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEditorInternal;
using UnityEngine;


namespace Obel.MSS.Editor
{
    [CustomEditor(typeof(StatesBehaviour))]
    public class StatesBehaviourEditor : UnityEditor.Editor
    {
        private StatesBehaviour statesBehaviour;
        private ReorderableList statesReorderableList;

        private void OnEnable()
        {
            statesBehaviour = (StatesBehaviour)target;

            statesReorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("statesGroup").FindPropertyRelative("states"), true, true, true, true);
            statesReorderableList.drawElementCallback = DrawState;
            statesReorderableList.drawHeaderCallback = DrawHeader;
            statesReorderableList.elementHeightCallback = GetElementHeight;
        }

        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "States");
        }

        private void DrawState(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty stateProperty = statesReorderableList.serializedProperty.GetArrayElementAtIndex(index);

            DrawerState.drawingState = statesBehaviour.statesGroup.states[index];
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, rect.height), stateProperty, true);
        }

        private float GetElementHeight(int index)
        {
            return 130 + statesBehaviour.statesGroup.states[index].tweens.Count * 80;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            serializedObject.Update();

            statesReorderableList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();

            EditorUtility.SetDirty(target);
        }
    }
}

