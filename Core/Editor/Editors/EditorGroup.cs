using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Obel.MSS.Editor
{
    internal static class EditorGroup
    {
        #region Properties

        private static ReorderableList _statesList;

        #endregion

        #region Inspector

        public static void Draw(Group group)
        {
            //if (group == null) return;

            GUILayout.Space(3);
            GUILayout.BeginHorizontal();
            GUILayout.Space(-6);
            GUILayout.BeginVertical();

            _statesList?.DoLayoutList();

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            DrawAddButton(group);
        }

        private static void DrawAddButton(Group group)
        {
            var rectAddButton = EditorGUILayout.GetControlRect();

            rectAddButton.y -= 4;
            rectAddButton.x = rectAddButton.width - 12;
            rectAddButton.width = 30;

            if (GUI.Button(rectAddButton, EditorConfig.Content.IconToolbarPlus, EditorConfig.Styles.PreButton)) OnAddStateButton(group);
        }

        #endregion

        #region Inspector callbacks

        private static void OnAddStateButton(Group group)
        {
            EditorActions.Add(() =>
            {
                var state = group.CreateState();

                EditorState.Reorder(group);
                EditorState.Get(state).Open();
            }, 
            InspectorStates.States, "Add State");
        }
    
        #endregion

        #region Public methods

        public static void Enable(Group group)
        {
            //if (group == null) return;

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

        #endregion
    }
}