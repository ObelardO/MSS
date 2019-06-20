using UnityEditor;

namespace Obel.MSS.Editor
{
    [CustomEditor(typeof(StatesBehaviour))]
    public class InspectorStatesBehaviour : UnityEditor.Editor
    {
        #region Properties

        private SerializedProperty statesGroupProperty;

        #endregion

        #region Unity methods

        private void OnEnable()
        {
            EditorStateValues.Clear();
            EditorStateValues.updatingAction = Repaint;

            statesGroupProperty = serializedObject.FindProperty("statesGroup");
        }

        private void OnDisable()
        {
            EditorStateValues.Clear();
            EditorStateValues.updatingAction = null;
        }

        #endregion

        #region Inspector

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorActions.Clear();

            EditorGUILayout.PropertyField(statesGroupProperty);

            EditorActions.Process();

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}