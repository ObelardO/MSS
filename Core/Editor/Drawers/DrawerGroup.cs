using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Obel.MSS.Editor
{
    [CustomPropertyDrawer(typeof(StatesGroup))]
    internal class DrawerGroup : PropertyDrawer
    {
        #region Properties

        private static readonly GUIContent profileLabel = new GUIContent("States"),
                                           newButton = new GUIContent("New"),
                                           addStateButton = new GUIContent("Add State");

        private Rect rect;
        
        private StatesGroup statesGroup;
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

            this.rect = rect;

            GUILayout.BeginVertical(GUI.skin.box);

            DrawGroupSelector();

            if (!enabled) OnEnable();

            if (!enabled || statesGroup == null)
            {
                GUILayout.EndVertical();
                return;
            }

            DrawGroup();
        }

        private void DrawGroupSelector()
        {
            GUILayout.BeginHorizontal();

            StatesGroup assignedStatesGroup = serializedProperty.objectReferenceValue as StatesGroup;

            EditorGUILayout.PropertyField(serializedProperty, profileLabel, false);

            if ((StatesGroup) serializedProperty.objectReferenceValue != assignedStatesGroup) EditorState.Clear();

            if (!statesGroup && GUILayout.Button(newButton, GUILayout.Width(50), GUILayout.Height(14))) OnCreateGroupButton();

            GUILayout.EndHorizontal();
        }

        private void DrawGroup()
        {
            GUILayout.Space(3);

            serializedStatesGroup.Update();

            statesReorderableList.DoLayoutList();

            DrawAddButton();

            GUILayout.EndVertical();

            Event guiEvent = Event.current;
            if (guiEvent.type == EventType.ValidateCommand && guiEvent.commandName == "UndoRedoPerformed") OnUndo();

            serializedStatesGroup.ApplyModifiedProperties();
        }

        private void DrawAddButton()
        {
            Rect rectAddButton = EditorGUILayout.GetControlRect();

            rectAddButton.y -= 4;
            rectAddButton.x = rectAddButton.width - 11;
            rectAddButton.width = 30;

            if (GUI.Button(rectAddButton, EditorConfig.Content.iconToolbarPlus, EditorConfig.Styles.preButton))
                EditorActions.Add(OnAddStateButton, statesGroup, "[MSS] Add State");
        }

        private void DrawStateBackground(Rect rect, int index, bool isActive, bool isFocused)
        {
            EditorGUI.DrawRect(rect, Color.clear);
        }

        private void DrawState(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.width += 5;

            EditorState.Select(statesGroup[index]);

            EditorGUI.PropertyField(rect, statesProperty.GetArrayElementAtIndex(index), true);
        }

        #endregion

        #region Inspector callbacks

        private void OnEnable()
        {
            SerializedObject serializedObject = serializedProperty.serializedObject;

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
                elementHeightCallback = GetStateHeight,
                drawElementCallback = DrawState,
                onReorderCallback = OnReordered
            };

            enabled = true;
        }

        private void OnUndo()
        {
            EditorAssets.Refresh(statesGroup);
            EditorState.Reorder(statesGroup.items);
            EditorActions.Clear();
        }

        private void OnAddStateButton()
        {
            State state = EditorAssets.AddItem(statesGroup);
            EditorAssets.Refresh(statesGroup);

            EditorState.Reorder(statesGroup.items);
            EditorState.Get(state).foldout.target = true;
        }

        private void OnCreateGroupButton()
        {
            serializedProperty.objectReferenceValue = CreateStatesProfile();
        }

        private void OnReordered(ReorderableList list)
        {
            EditorState.Reorder(statesGroup.items);
        }

        private float GetStateHeight(int index)
        {
            // TODO cache height !!!

            EditorState.Select(statesGroup[index]);

            float tweensHeight = 0;

            for (int i = 0; i < statesGroup[index].Count; i++) tweensHeight += DrawerTween.GetHeight(i);

            return DrawerState.headerHeight + 6 +
                Mathf.Lerp(0, 77 + (statesGroup[index].Count == 0 ? 14 : tweensHeight - 7),
                EditorState.Get(statesGroup[index]).foldout.faded);
        }

        #endregion

        #region Public methods

        [MenuItem("Assets/Create/MSS/States Profile")]
        public static StatesGroup CreateStatesProfile()
        {
            StatesGroup newStatesGroup = EditorAssets.Create<StatesGroup>("NewStatesGroup");

            EditorAssets.AddItem(newStatesGroup, "[State] newState");
            EditorAssets.AddItem(newStatesGroup, "[State] newState");

            return newStatesGroup;
        }

        #endregion
    }
}

