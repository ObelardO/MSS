using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Obel.MSS.Editor
{
    internal static class EditorGroup
    {
        #region Properties

        private static SerializedObject _serializedStatesGroup;
        private static ReorderableList _statesList;

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

            _statesList.DoLayoutList();

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            DrawAddButton(group);

            //serializedStatesGroup.ApplyModifiedProperties();
        }

        private static void DrawAddButton(StatesGroup group)
        {
            var rectAddButton = EditorGUILayout.GetControlRect();

            rectAddButton.y -= 4;
            rectAddButton.x = rectAddButton.width - 19;
            rectAddButton.width = 30;

            if (GUI.Button(rectAddButton, EditorConfig.Content.IconToolbarPlus, EditorConfig.Styles.PreButton))
                EditorActions.Add(() => OnAddStateButton(group), InspectorStates.States, "Add State");
        }

        #endregion

        #region Inspector callbacks

        public static void OnEnable(StatesGroup group)
        {
            if (group == null) return;

            Debug.Log("New states list");

            //serializedStatesGroup = InspectorStates.SerializedState.FindProperty("statesGroup")); 

            _statesList = new ReorderableList(group.Items as IList, typeof(State))
            {
                displayAdd = false,
                displayRemove = false,
                draggable = true,

                headerHeight = 0,
                footerHeight = 0,

                showDefaultBackground = false,

                drawElementBackgroundCallback = EditorState.DrawBackground,
                elementHeightCallback = index => EditorState.GetHeight(EditorState.Get(group[index])),
                drawElementCallback = (rect, index, isActive, isFocused) => EditorState.Draw(rect, group[index]),
                onReorderCallback = list => EditorState.Reorder(group)
            };
        }

        private static void OnAddStateButton(StatesGroup group)
        {
            var state = group.AddNew();

            EditorState.Reorder(group);
            EditorState.Get(state).Open();
        }

        #endregion

        #region Public methods

        public static StatesGroup Create()
        {
            var newStatesGroup = new StatesGroup();

            newStatesGroup.AddNew();
            newStatesGroup.AddNew();

            OnEnable(newStatesGroup);

            return newStatesGroup;
        }

        #endregion
    }
}