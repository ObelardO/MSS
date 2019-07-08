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
            EditorStateValues.Clear();
            EditorStateValues.updatingAction = Repaint;

            statesGroupProperty = serializedObject.FindProperty("statesGroup");
        }

        private void OnDisable()
        {
            EditorActions.Clear();
            EditorStateValues.Clear();
            EditorStateValues.updatingAction = null;
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