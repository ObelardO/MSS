using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditorInternal;

namespace Obel.MSS.Editor
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(StatesBehaviour))]
    public class InspectorStatesBehaviour : Editor, IDataBaseEditor<StatesGroup>
    {
        #region Properties

        private StatesBehaviour statesBehaviour;
        private SerializedObject serializedStatesGroup;
        private SerializedProperty statesProperty;
        private ReorderableList statesReorderableList;

        #endregion

        #region Unity methods

        private void OnEnable()
        {
            if (!(target is StatesBehaviour)) return;

            statesBehaviour = (StatesBehaviour)target;

            StateEditorValues.Clear();
            StateEditorValues.updatingAction = Repaint;

            if (statesBehaviour.statesGroup == null) statesBehaviour.statesGroup = Add();

            SerializedProperty statesGroupProperty = serializedObject.FindProperty("statesGroup");

            serializedStatesGroup = new SerializedObject(statesGroupProperty.objectReferenceValue);

            statesProperty = serializedStatesGroup.FindProperty("items");

            statesReorderableList = new ReorderableList(serializedObject, statesProperty)
            {
                displayAdd = false,
                displayRemove = false,
                draggable = true,

                headerHeight = 0,
                footerHeight = 0,

                showDefaultBackground = false,

                drawElementBackgroundCallback = DrawStateBackground,
                drawElementCallback = DrawState,
                elementHeightCallback = GetStateHeight,
                onReorderCallback = OnReordered
            };
        }


        private void OnDisable()
        {
            StateEditorValues.Clear();
            StateEditorValues.updatingAction = null;
        }

        #endregion

        #region Inspector

        public override void OnInspectorGUI()
        {
            serializedStatesGroup.Update();

            EditorGUI.BeginDisabledGroup(!statesBehaviour.enabled);
            GUILayout.Space(6);

            statesReorderableList.DoLayoutList();

            GUILayout.Space(3);

            if (GUILayout.Button("Add Stete"))
            {
                EditorActions.Add(() =>
                {
                    State state = EditorDataBase.SaveAsset<State>();


                    statesBehaviour.statesGroup.Add(state);


                    //StateEditorValues.Clear();
                    StateEditorValues.Reorder(statesBehaviour.statesGroup.items);
                    StateEditorValues.Get(state).foldout.target = true;



                    AssetDatabase.ImportAsset(EditorDataBase.AssetPath);
                    //AssetDatabase.Refresh();
                },
                statesBehaviour.statesGroup, "[MSS] Add State");
            }

            EditorGUI.EndDisabledGroup();

            Event guiEvent = Event.current;
            if (guiEvent.type == EventType.ValidateCommand && guiEvent.commandName == "UndoRedoPerformed")
            {
                //StateEditorValues.Clear();
                StateEditorValues.Reorder(statesBehaviour.statesGroup.items);


                AssetDatabase.ImportAsset(EditorDataBase.AssetPath);
            }

            EditorActions.Process();

            serializedStatesGroup.ApplyModifiedProperties();
        }

        #endregion

        #region Inspector callbacks

        private void OnReordered(ReorderableList list)
        {
            StateEditorValues.Reorder(statesBehaviour.statesGroup.items);
        }

        private void DrawStateBackground(Rect rect, int index, bool isActive, bool isFocused)
        {
            EditorGUI.DrawRect(rect, Color.clear);
        }

        private void DrawState(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.width += 5;

            DrawerState.editorValues = StateEditorValues.Get(statesBehaviour.statesGroup[index]);

            EditorGUI.PropertyField(rect, statesProperty.GetArrayElementAtIndex(index), true);
        }

        private float GetStateHeight(int index)
        {
            int tweensCount = statesBehaviour.statesGroup[index].items.Count;

            StateEditorValues editorValues = StateEditorValues.Get(statesBehaviour.statesGroup[index]);

            return 26 + Mathf.Lerp(0, 70 + (tweensCount == 0 ? 0 : tweensCount - 1) * 21 + 40,
                       editorValues.foldout.faded);
        }

        #endregion

        #region DataBase methods

        public StatesGroup Add()
        {
            StatesGroup newStatesGroup = EditorDataBase.SaveAsset<StatesGroup>();
            EditorDataBase.instance.Add(newStatesGroup);

            AddState(newStatesGroup);
            AddState(newStatesGroup);

            return newStatesGroup;
        }

        public State AddState(StatesGroup statesGroup)
        {
            State newState = EditorDataBase.SaveAsset<State>();
            statesGroup.Add(newState);

            return newState;
        }

        public void Remove(StatesGroup statesGroup)
        {
            EditorDataBase.instance.Remove(statesGroup, false);
            EditorDataBase.RemoveAsset(statesGroup);
        }

        #endregion
    }


    #region supporting classes

    public class StateEditorValues
    {
        static private Dictionary<int, StateEditorValues> statesDictionary = new Dictionary<int, StateEditorValues>();

        public State state;
        public AnimBool foldout;
        public SerializedObject serializedState;
        public ReorderableList tweensReorderableList;

        public static UnityAction updatingAction;

        public StateEditorValues(State state)
        {
            foldout = new AnimBool(false);
            this.state = state;

            statesDictionary.Add(state.ID, this);

            if (updatingAction != null) foldout.valueChanged.AddListener(updatingAction);

            serializedState = new SerializedObject(state);
        }

        public static StateEditorValues Get(State state)
        {
            StateEditorValues editorValues;

            if (statesDictionary.ContainsKey(state.ID))
                editorValues = statesDictionary[state.ID];
            else
                editorValues = new StateEditorValues(state);

            return editorValues;
        }

        public static void Clear()
        {
            statesDictionary.Clear();
        }

        public static void Reorder(List<State> reorderedStates)
        {
            foreach (KeyValuePair<int, StateEditorValues> entry in statesDictionary)
                for (int i = 0; i < reorderedStates.Count; i++)
                    if (entry.Key.Equals(reorderedStates[i].ID))
                        statesDictionary[entry.Key].state = reorderedStates[i];
        }
    }

    #endregion
}