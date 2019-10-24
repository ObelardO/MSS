using System;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    [CustomEditor(typeof(States))]
    internal class InspectorStates : UnityEditor.Editor
    {
        #region Properties

        /*
        private static readonly GUIContent ProfileLabel = new GUIContent("States"),
                                           NewButton = new GUIContent("New");
        */

        public static States States { private set; get; }

        public static SerializedObject SerializedState { private set; get; }

        #endregion

        #region Unity methods

        private void OnEnable()
        {
            EditorActions.Clear();
            EditorState.Clear();
            EditorState.Repaint = Repaint;

            States = (States)target;

            SerializedState = serializedObject;

            EditorGroup.Enable(States.Group);
        }

        private void OnDisable()
        {
            EditorActions.Clear();
            EditorState.Clear();
            EditorState.Repaint = null;
            States = null;
        }

        #endregion

        #region Public methods

        public static void Record() => EditorActions.Record(States);

        #endregion

        #region Inspector

        public override void OnInspectorGUI()
        {
            SerializedState.Update();

            GUILayout.BeginHorizontal();
            GUILayout.Space(-8);

            GUILayout.BeginVertical();
            GUILayout.Space(2);

            /*
            GUILayout.BeginHorizontal();
            GUILayout.Space(12);

            //if (States.Group == null && GUILayout.Button(NewButton, GUILayout.Width(50), GUILayout.Height(14))) States.Group = new Group();// EditorGroup.Create();

            GUILayout.EndHorizontal();
            */

            EditorGroup.Draw(States.Group);

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            EditorActions.Process();

            var guiEvent = Event.current;
            if (guiEvent.type == EventType.ValidateCommand && guiEvent.commandName == "UndoRedoPerformed") EditorApplication.delayCall += () => OnUndo(States.Group);

            SerializedState.ApplyModifiedProperties();
        }

        #endregion

        #region Inspector callbacks

        private static void OnUndo(Group group)
        {
            if (group == null) return;

            EditorGroup.Enable(group);
            EditorState.Reorder(group);
        }

        #endregion

    }
}

