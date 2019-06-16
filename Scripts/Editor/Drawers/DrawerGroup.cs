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

            statesGroup = serializedObject.FindProperty("statesGroup").objectReferenceValue as StatesGroup;
                //fieldInfo.GetValue(serializedObject.targetObject) as StatesGroup;

            if (statesGroup == null) return;

            serializedStatesGroup = new SerializedObject(statesGroup/* serializedObject.FindProperty("statesGroup").objectReferenceValue*/);

            statesProperty = serializedStatesGroup.FindProperty("items");

            StateEditorValues.Clear();
            

            statesReorderableList = new ReorderableList(serializedObject, statesProperty)
            //statesReorderableList = new ReorderableList(statesGroup.items, typeof(State))
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

            if (statesGroup == null)
            {
                GUI.color = Color.red;
            }
            //rect.width *= 0.5f;

            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), property, label, false);

            totalPropertyHeight = (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2);

            rect.y += totalPropertyHeight;
            rect.height = 600;
            if (enabled && statesReorderableList != null) statesReorderableList.DoList(rect);

            GUI.color = Color.white;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return totalPropertyHeight;

            /*
            if (statesGroup == null) return + EditorGUIUtility.standardVerticalSpacing;

            return statesGroup.Count * 100 + (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 2;
            */    
        }

        #endregion

        #region Inspector callbacks

        private void OnUndo()
        {
            EditorAssets.Refresh(statesGroup);
            StateEditorValues.Reorder(statesGroup.items);
        }

        private void OnAddButton()
        {
            State state = AddState(statesGroup);
            EditorAssets.Refresh(statesGroup);

            StateEditorValues.Reorder(statesGroup.items);
            StateEditorValues.Get(state).foldout.target = true;
        }

        private void OnReordered(ReorderableList list)
        {
            StateEditorValues.Reorder(statesGroup.items);
        }

        private void DrawStateBackground(Rect rect, int index, bool isActive, bool isFocused)
        {
            EditorGUI.DrawRect(rect, Color.clear);
        }

        private void DrawState(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.width += 5;

            DrawerState.editorValues = StateEditorValues.Get(statesGroup[index]);


            EditorGUI.PropertyField(rect, statesProperty.GetArrayElementAtIndex(index), true);

            //EditorGUI.PropertyField(rect, serializedStatesGroup.FindProperty("items").GetArrayElementAtIndex(index), true);
        }

        private float GetStateHeight(int index)
        {
            int tweensCount = statesGroup[index].Count;

            StateEditorValues editorValues = StateEditorValues.Get(statesGroup[index]);

            float stateHeight = DrawerState.headerHeight + 6 + Mathf.Lerp(0, 70 + (tweensCount == 0 ? 0 : tweensCount - 1) * 21 + 40,
                                    editorValues.foldout.faded);

            totalPropertyHeight += stateHeight;

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

