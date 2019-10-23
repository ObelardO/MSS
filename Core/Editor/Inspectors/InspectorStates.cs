using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    [CustomEditor(typeof(States))]
    internal class InspectorStates : UnityEditor.Editor
    {
        #region Properties

        private static readonly GUIContent ProfileLabel = new GUIContent("States"),
                                           NewButton = new GUIContent("New");

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

            EditorGroup.OnEnable(States.Group);

            Debug.Log("MSS INSPECTOR BEGIN");
        }

        private void OnDisable()
        {
            EditorActions.Clear();
            EditorState.Clear();
            EditorState.Repaint = null;

            States = null;

            Debug.Log("MSS INSPECTOR END");
        }

        #endregion

        #region Public methods

        public static void Record()
        {
            EditorActions.Record(States);
        }

        #endregion

        #region Inspector

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();

            SerializedState.Update();

            GUILayout.BeginHorizontal();
            GUILayout.Space(-8);

            GUILayout.BeginVertical();
            GUILayout.Space(2);

            GUILayout.BeginHorizontal();
            GUILayout.Space(12);

            if (States.Group == null && GUILayout.Button(NewButton, GUILayout.Width(50), GUILayout.Height(14))) States.Group = EditorGroup.Create();

            GUILayout.EndHorizontal();

            if (States.Group != null) EditorGroup.Draw(States.Group);

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            EditorActions.Process();

            Event guiEvent = Event.current;
            if (guiEvent.type == EventType.ValidateCommand && guiEvent.commandName == "UndoRedoPerformed") EditorApplication.delayCall += () => OnUndo(States.Group);

            SerializedState.ApplyModifiedProperties();
        }

        #endregion

        #region Inspector callbacks

        private static void OnUndo(StatesGroup group)
        {
            if (group == null) return;

            Debug.Log("Undo performed");

            EditorGroup.OnEnable(group);
            EditorState.Reorder(group);

            EditorState.CalculateAllListsHeight();
        }

        #endregion

    }
}

