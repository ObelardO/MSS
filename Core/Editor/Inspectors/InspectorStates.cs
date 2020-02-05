using UnityEngine;
using UnityEditor;

namespace Obel.MSS.Editor
{
    [CustomEditor(typeof(States))]
    public class InspectorStates : UnityEditor.Editor
    {
        #region Properties

        public static States States { private set; get; }

        public static SerializedObject SerializedState { private set; get; }

        #endregion

        #region Unity methods

        private void OnEnable()
        {
            Undo.undoRedoPerformed += PrecessUndo;

            EditorActions.Clear();
            EditorState.Clear();
            EditorState.Repaint = Repaint;

            States = (States)target;

            SerializedState = serializedObject;

            EditorGroup.Enable(States.Group);
        }

        private void OnDisable()
        {
            Undo.undoRedoPerformed -= PrecessUndo;

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

            EditorGroup.Draw(States.Group);

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            EditorActions.Process();

            SerializedState.ApplyModifiedProperties();
        }

        #endregion

        #region Inspector callbacks

        private void PrecessUndo()
        {
            EditorGroup.Enable(States.Group);
            EditorState.Reorder(States.Group);
            Repaint();
        }

        #endregion

    }
}