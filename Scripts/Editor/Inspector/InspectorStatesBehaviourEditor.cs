using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.Build.Content;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

namespace Obel.MSS.Editor
{


    [CustomEditor(typeof(StatesBehaviour))]
    public class StatesBehaviourEditor : UnityEditor.Editor
    {
        private Dictionary<string, StateEditorValues> statesDictionary = new Dictionary<string, StateEditorValues>();

        private class StateEditorValues
        {
            public bool editing;
            public AnimBool foldout;

            public static StateEditorValues last;

            public StateEditorValues(UnityAction valueChanged = null)
            {

                foldout = new AnimBool(false);

                if (valueChanged != null) foldout.valueChanged.AddListener(valueChanged);
            }

            //public bool 
        }

        private StateEditorValues GetStateEditorValues(State state)
        {
            StateEditorValues editorValues;

            if (statesDictionary.ContainsKey(state.name))
            {
                editorValues = statesDictionary[state.name];
            }
            else
            {
                editorValues = new StateEditorValues(Repaint);
                statesDictionary.Add(state.name, editorValues);
            }

            StateEditorValues.last = editorValues;

            return editorValues;
        }







        public static StatesBehaviourEditor instance;

        private StatesBehaviour statesBehaviour;
        private ReorderableList statesReorderableList;

        private Dictionary<string, ReorderableList> tweensListDictionary = new Dictionary<string, ReorderableList>();

        private void OnEnable()
        {
            //instance = this;

            statesBehaviour = (StatesBehaviour)target;

            statesReorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("statesGroup").FindPropertyRelative("states"))
            {
                displayAdd = false,
                displayRemove = false,
                draggable = true,
                
                headerHeight = 0,
                footerHeight = 0,
                
                showDefaultBackground = false,

                drawElementBackgroundCallback = DrawStateBackground,
                drawElementCallback = DrawState,
                elementHeightCallback = GetStateHeight
            };

            Debug.Log(statesReorderableList == null);
        }

        /*
        private void DrawStateHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "States");
        }
        */

        private void DrawStateBackground(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.height -= 2;

            EditorGUI.DrawRect(rect, Color.clear);
        }

        private void DrawState(Rect rect, int index, bool isActive, bool isFocused)
        {
            State drawingState = statesBehaviour.statesGroup.states[index];

            StateEditorValues editorValues = GetStateEditorValues(drawingState);

            SerializedProperty stateProperty = statesReorderableList.serializedProperty.GetArrayElementAtIndex(index);

            Rect rectBackground = new Rect(rect.x, rect.y, rect.width, rect.height - 4);
            EditorGUI.DrawRect(rectBackground, Color.white * 0.4f);// BeginVertical(GUI.skin.box);


            Rect rectFoldOutBack = new Rect(rect.x, rect.y, rect.width, 20);
            GUI.Box(rectFoldOutBack, GUIContent.none, GUI.skin.box);

            Rect rectFoldout = new Rect(rect.x + 15, rect.y + 2, rect.width - 40, 20);

            editorValues.foldout.target = EditorGUI.Foldout(rectFoldout, editorValues.foldout.target,
                new GUIContent(drawingState.name), true, GUI.skin.label);


            //EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, rect.height), stateProperty, true);

            if (GUI.Button(new Rect(rect.width + 5, rect.y + 1, 30, 20), HelperEditor.Content.iconToolbarMinus, HelperEditor.Styles.preButton))
            {
                UserActions.Add(() => statesBehaviour.statesGroup.states.RemoveAt(index), statesBehaviour, "[MSS] Remove State");
            }









            SerializedProperty tweensProperty = stateProperty.FindPropertyRelative("tweens");

            /*

            string tweenListKey = stateProperty.propertyPath;

            ReorderableList tweensReorderableList;

            if (tweensListDictionary.ContainsKey(tweenListKey))
            {
                tweensReorderableList = tweensListDictionary[tweenListKey];
            }
            else
            {
                tweensReorderableList = new ReorderableList(serializedObject, tweensProperty)
                {
                    displayAdd = true,
                    displayRemove = true,
                    draggable = false,

                    drawHeaderCallback = innerRect =>
                    {
                        EditorGUI.LabelField(innerRect, "Tweens");
                    },

                    drawElementCallback = (innerRect, innerIndex, innerA, innerH) =>
                    {
                        // Get element of inner list
                        SerializedProperty tweenProperty = tweensProperty.GetArrayElementAtIndex(innerIndex);

                        //SerializedProperty tweenProperty = tweenProperty.FindPropertyRelative("name");

                        EditorGUI.PropertyField(innerRect, tweenProperty);
                    }
                };
                tweensListDictionary[tweenListKey] = tweensReorderableList;
            }

            var height = (tweensProperty.arraySize + 3) * EditorGUIUtility.singleLineHeight;
            tweensReorderableList.DoList(new Rect(rect.x, rect.y, rect.width, height));

    */
        }

        private float GetStateHeight(int index)
        {
            int tweensCount = statesBehaviour.statesGroup.states[index].tweens.Count;

            StateEditorValues editorValues = GetStateEditorValues(statesBehaviour.statesGroup.states[index]);

            return 26 + Mathf.Lerp(0, 70 + (tweensCount == 0 ? 0 : tweensCount - 1) * 21 + 40,
                       editorValues.foldout.faded);
        }

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();

            GUILayout.Space(6);

            serializedObject.Update();

            statesReorderableList.DoLayoutList();

            GUILayout.Space(3);

            if (GUILayout.Button("Add Stete"))
                UserActions.Add(() => statesBehaviour.statesGroup.states.Add(new State()), statesBehaviour, "[MSS] Add State");

            UserActions.Process();

            serializedObject.ApplyModifiedProperties();

            //EditorUtility.SetDirty(target);
        }


    }
}

