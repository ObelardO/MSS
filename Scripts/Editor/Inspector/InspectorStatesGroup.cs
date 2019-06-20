using UnityEditor;

namespace Obel.MSS.Editor
{
    [CustomEditor(typeof(StatesGroup))]
    public class InspectorStatesGroup : UnityEditor.Editor
    {
        #region Properties

        private StatesGroup statesGroup;
        private SerializedProperty statesGroupProperty;

        #endregion

        #region Unity methods

        private void OnEnable()
        {
            if (!(target is StatesGroup)) return;

            statesGroup = (StatesGroup)target;       
        }

        private void OnDisable()
        {

        }

        #endregion

        #region Inspector

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }

        #endregion
    }
}