using UnityEditor;

namespace Obel.MSS.Editor
{
    [CustomEditor(typeof(States))]
    public class InspectorStates : UnityEditor.Editor
    {
        #region Properties

        private SerializedProperty statesGroupProperty;

        #endregion

        #region Unity methods

        private void OnEnable()
        {
            EditorActions.Clear();
            EditorState.Clear();
            EditorState.Repaint = Repaint;

            statesGroupProperty = serializedObject.FindProperty("statesGroup");
        }

        private void OnDisable()
        {
            EditorActions.Clear();
            EditorState.Clear();
            EditorState.Repaint = null;
        }

        #endregion

        #region Inspector

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(statesGroupProperty);

            EditorActions.Process();

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}