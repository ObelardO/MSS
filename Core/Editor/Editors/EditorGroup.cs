using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Obel.MSS.Editor
{
    internal static class EditorGroup
    {
        #region Properties

        //private static SerializedObject serializedStatesGroup;
        private static ReorderableList statesReorderableList;

        #endregion

        #region Inspector

        public static void Draw(StatesGroup group)
        {
            if (group == null) return;

            //serializedStatesGroup.Update();

            GUILayout.Space(3);
            GUILayout.BeginHorizontal();
            GUILayout.Space(-6);
            GUILayout.BeginVertical();

            statesReorderableList.DoLayoutList();

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            DrawAddButton(group);

            Event guiEvent = Event.current;
            if (guiEvent.type == EventType.ValidateCommand && guiEvent.commandName == "UndoRedoPerformed") OnUndo(group);

            //serializedStatesGroup.ApplyModifiedProperties();
        }

        private static void DrawAddButton(StatesGroup group)
        {
            Rect rectAddButton = EditorGUILayout.GetControlRect();

            rectAddButton.y -= 4;
            rectAddButton.x = rectAddButton.width - 19;
            rectAddButton.width = 30;

            if (GUI.Button(rectAddButton, EditorConfig.Content.iconToolbarPlus, EditorConfig.Styles.preButton))
                EditorActions.Add(() => OnAddStateButton(group)/*, group, "[MSS] Add State")*/);
        }

        #endregion

        #region Inspector callbacks

        public static void OnEnable(StatesGroup group)
        {
            if (group == null) return;

            //serializedStatesGroup = new SerializedObject(group);

            statesReorderableList = new ReorderableList(group.items, typeof(State))
            {
                displayAdd = false,
                displayRemove = false,
                draggable = true,

                headerHeight = 0,
                footerHeight = 0,

                showDefaultBackground = false,

                drawElementBackgroundCallback = EditorState.DrawBackground,
                elementHeightCallback = index => EditorState.GetHeight(EditorState.Get(group[index])),
                drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => EditorState.Draw(rect, group[index]),
                onReorderCallback = list => EditorState.Reorder(group)
            };
        }

        private static void OnUndo(StatesGroup group)
        {
            //EditorAssets.Refresh(group);
            EditorState.Reorder(group);
            EditorActions.Clear();
            EditorState.CalculateAllTweensListsHeight();
        }

        private static void OnAddStateButton(StatesGroup group)
        {
            State state = EditorAssets.AddItem(group);
            //EditorAssets.Refresh(group);

            EditorState.Reorder(group);
            EditorState.Get(state).foldout.target = true;
        }

        #endregion

        #region Public methods

        [MenuItem("Assets/Create/MSS/States Profile")]
        public static StatesGroup CreateStatesProfile()
        {
            StatesGroup newStatesGroup = new StatesGroup();// EditorAssets.Create<StatesGroup>("NewStatesGroup");

            Tween a = new Tween();

            Debug.Log((newStatesGroup == null) + "   " + (a == null));

            EditorAssets.AddItem(newStatesGroup, "[State] newState");
            EditorAssets.AddItem(newStatesGroup, "[State] newState");

            return newStatesGroup;
        }

        #endregion
    }
}