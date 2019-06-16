using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Obel.MSS.Editor
{
    [CustomPropertyDrawer(typeof(StatesGroup))]
    internal class DrawerGroup : PropertyDrawer
    {
        #region Properties

        //public static StatesBehaviour statesBehaviour;

        private StatesGroup statesGroup;
        private SerializedObject serializedObject;
        private SerializedObject serializedStatesGroup;
        private SerializedProperty statesProperty;
        private ReorderableList statesReorderableList;

        private SerializedProperty serializedProperty;

        private float totalPropertyHeight;

        #endregion


        private bool enabled = false;

        void OnEnable()
        {
            serializedObject = serializedProperty.serializedObject;

            statesGroup = fieldInfo.GetValue(serializedObject.targetObject) as StatesGroup;

                //serializedObject.FindProperty("statesGroup").objectReferenceValue as StatesGroup;
                //;

            if (statesGroup == null) return;

            serializedStatesGroup = new SerializedObject(statesGroup/* serializedObject.FindProperty("statesGroup").objectReferenceValue*/);

            statesProperty = serializedStatesGroup.FindProperty("items");

            //statesReorderableList = new ReorderableList(serializedObject, statesProperty)
            statesReorderableList = new ReorderableList(statesGroup.items, typeof(State))
            {
                displayAdd = false,
                displayRemove = false,
                draggable = true,

                headerHeight = 0,
                footerHeight = 0,

                showDefaultBackground = false,

                drawElementBackgroundCallback = DrawStateBackground,
                elementHeightCallback = GetStateHeight,
                drawElementCallback = DrawState,
                onReorderCallback = OnReordered
            };

            enabled = true;
        }

        #region Inspector

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            if (serializedProperty == null) serializedProperty = property;

            if (!enabled) OnEnable();

            if (!enabled) return;

            DrawGroup(property, label);



            /* TODO LAYOUT VARIAND
            EditorLayout.PushColor();

            EditorLayout.SetPosition(rect.x - 4, rect.y);
            EditorLayout.SetWidth(rect.width);
            EditorLayout.Control((r) => EditorGUI.PropertyField(r, property, label, false));




            totalPropertyHeight = EditorLayout.fixedHeight * 2;

            if (!enabled || statesGroup == null)
            {
                return;
            }

            serializedStatesGroup.Update();

            EditorLayout.Space();

            EditorLayout.SetSize(new Vector2(rect.width, totalPropertyHeight));

            EditorLayout.Control((r) => statesReorderableList.DoList(r));

            EditorLayout.Space();

            EditorLayout.SetPosition(rect.width / 2 - 572 / 2, totalPropertyHeight + rect.y);

            EditorLayout.Control(572, (r) =>
            {
                if (GUI.Button(r, "Add State")) EditorActions.Add(() => OnAddButton(), statesGroup, "[MSS] Add State");
            });
            */


        }

        /*
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return totalPropertyHeight;
        }
        */

        private void DrawGroup(SerializedProperty property, GUIContent label)
        {
            serializedStatesGroup.Update();

            EditorGUILayout.PropertyField(property, label, false);

            statesReorderableList.DoLayoutList();

            if (GUILayout.Button("Add State")) EditorActions.Add(() => OnAddButton(), statesGroup, "[MSS] Add State");

            EditorLayout.PullColor();

            EditorActions.Process();

            Event guiEvent = Event.current;
            if (guiEvent.type == EventType.ValidateCommand && guiEvent.commandName == "UndoRedoPerformed") OnUndo();

            serializedStatesGroup.ApplyModifiedProperties();
        }

        #endregion

        #region Inspector callbacks

        private void OnUndo()
        {
            EditorAssets.Refresh(statesGroup);
            EditorStateValues.Reorder(statesGroup.items);
        }

        private void OnAddButton()
        {
            State state = AddState(statesGroup);
            EditorAssets.Refresh(statesGroup);

            EditorStateValues.Reorder(statesGroup.items);
            EditorStateValues.Get(state).foldout.target = true;
        }

        private void OnReordered(ReorderableList list)
        {
            EditorStateValues.Reorder(statesGroup.items);
        }

        private void DrawStateBackground(Rect rect, int index, bool isActive, bool isFocused)
        {
            EditorGUI.DrawRect(rect, Color.clear);
        }

        private void DrawState(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.width += 5;

            DrawerState.editorValues = EditorStateValues.Get(statesGroup[index]);


            EditorGUI.PropertyField(rect, statesProperty.GetArrayElementAtIndex(index), true);

            //EditorStateValues editorValues = EditorStateValues.Get(statesGroup[index]);

            totalPropertyHeight += DrawerState.headerHeight + 6 + Mathf.Lerp(0, 120,
                                       DrawerState.editorValues.foldout.faded);

            

            //EditorGUI.PropertyField(rect, serializedStatesGroup.FindProperty("items").GetArrayElementAtIndex(index), true);
        }

        private float GetStateHeight(int index)
        {
            int tweensCount = statesGroup[index].Count;

            EditorStateValues editorValues = EditorStateValues.Get(statesGroup[index]);

            float stateHeight = DrawerState.headerHeight + 6 + Mathf.Lerp(0, 70 + (tweensCount == 0 ? 0 : tweensCount - 1) * 21 + 40,
                                    editorValues.foldout.faded);


            return stateHeight;
        }

        #endregion

        [MenuItem("Assets/Create/MSS/States Profile")]
        public static StatesGroup CreateStatesProfile()
        {
            StatesGroup newStatesGroup = EditorAssets.Create<StatesGroup>("NewStatesGroup");
            AddState(newStatesGroup);
            AddState(newStatesGroup);

            return newStatesGroup;
        }

        public static State AddState(StatesGroup statesGroup)
        {
            State newState = EditorAssets.Save<State>(statesGroup, "[State] NewState");
            statesGroup.Add(newState);
            return newState;
        }
    }
}

