using UnityEditor;

namespace Obel.MSS.Editor
{
    [CustomEditor(typeof(StatesGroup))]
    public class InspectorStatesGroup : UnityEditor.Editor
    {
        #region Properties

        private static StatesGroup group;

        #endregion

        #region Unity methods

        private void OnEnable()
        {
            EditorActions.Clear();
            EditorState.Clear();
            EditorState.Repaint = Repaint;

            group = (StatesGroup)target;

            DrawerGroup.OnEnable(group);
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
            DrawerGroup.Draw(group);

            EditorActions.Process();
        }

        #endregion
    }
}