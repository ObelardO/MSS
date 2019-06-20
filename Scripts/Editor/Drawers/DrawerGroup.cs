using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Obel.MSS.Editor
{
    [CustomPropertyDrawer(typeof(StatesGroup))]
    internal class DrawerGroup : PropertyDrawer
    {
        #region Properties

        private static readonly GUIContent profileLabel = new GUIContent("Profile"),
                                           newButton = new GUIContent("New"),
                                           addStateButton = new GUIContent("Add State");

        private StatesGroup statesGroup;
        private SerializedObject serializedObject;
        private SerializedObject serializedStatesGroup;
        private SerializedProperty statesProperty;
        private ReorderableList statesReorderableList;

        private SerializedProperty serializedProperty;

        private bool enabled = false;

        #endregion

        #region Inspector

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            if (serializedProperty == null) serializedProperty = property;

            DrawGroupSelector();

            if (!enabled) OnEnable();

            if (!enabled || statesGroup == null) return;

            DrawGroup();
        }

        private void DrawGroupSelector()
        {
            GUILayout.BeginHorizontal(GUI.skin.box);

            StatesGroup assignedStatesGroup = serializedProperty.objectReferenceValue as StatesGroup;

            EditorGUILayout.PropertyField(serializedProperty, profileLabel, false);

            if ((StatesGroup) serializedProperty.objectReferenceValue != assignedStatesGroup) EditorStateValues.Clear();

            if (!statesGroup && GUILayout.Button(newButton, GUILayout.Width(50))) OnCreateGroupButton();

            GUILayout.EndHorizontal();
        }

        private void DrawGroup()
        {
            serializedStatesGroup.Update();

            statesReorderableList.DoLayoutList();

            if (GUILayout.Button(addStateButton, GUILayout.Width(200))) EditorActions.Add(OnAddStateButton, statesGroup, "[MSS] Add State");

            Event guiEvent = Event.current;
            if (guiEvent.type == EventType.ValidateCommand && guiEvent.commandName == "UndoRedoPerformed") OnUndo();

            serializedStatesGroup.ApplyModifiedProperties();
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
        }

        #endregion

        #region Inspector callbacks

        void OnEnable()
        {
            serializedObject = serializedProperty.serializedObject;

            statesGroup = fieldInfo.GetValue(serializedObject.targetObject) as StatesGroup;
            //serializedObject.FindProperty("statesGroup").objectReferenceValue as StatesGroup;

            if (statesGroup == null) return;

            serializedStatesGroup = new SerializedObject(statesGroup);
            //serializedStatesGroup = new SerializedObject(serializedObject.FindProperty("statesGroup").objectReferenceValue);

            statesProperty = serializedStatesGroup.FindProperty("items");

            statesReorderableList = new ReorderableList(statesGroup.items, typeof(State))
            //statesReorderableList = new ReorderableList(serializedObject, statesProperty)
            {
                displayAdd = false,
                displayRemove = false,
                draggable = true,

                headerHeight = 0,
                footerHeight = 0,

                showDefaultBackground = false,

                drawElementBackgroundCallback = DrawStateBackground,
                elementHeightCallback = OnGetStateHeight,
                drawElementCallback = DrawState,
                onReorderCallback = OnReordered
            };

            enabled = true;
        }

        private void OnUndo()
        {
            EditorAssets.Refresh(statesGroup);
            EditorStateValues.Reorder(statesGroup.items);
            EditorActions.Clear();
        }

        private void OnAddStateButton()
        {
            State state = AddState(statesGroup);
            EditorAssets.Refresh(statesGroup);

            EditorStateValues.Reorder(statesGroup.items);
            EditorStateValues.Get(state).foldout.target = true;
        }

        private void OnCreateGroupButton()
        {
            serializedProperty.objectReferenceValue = CreateStatesProfile();
        }

        private void OnReordered(ReorderableList list)
        {
            EditorStateValues.Reorder(statesGroup.items);
        }

        private float OnGetStateHeight(int index)
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

