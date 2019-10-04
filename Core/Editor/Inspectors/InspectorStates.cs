using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;

namespace Obel.MSS.Editor
{
    [CustomEditor(typeof(States))]
    internal class InspectorStates : UnityEditor.Editor
    {
        #region Properties

        private static readonly GUIContent profileLabel = new GUIContent("States"),
                                           newButton = new GUIContent("New");

        public static States states { private set; get; }

        #endregion

        #region Unity methods

        private void OnEnable()
        {
            EditorActions.Clear();
            EditorState.Clear();
            EditorState.Repaint = Repaint;

            states = (States)target;

            EditorGroup.OnEnable(states.statesGroup);

            Debug.Log("MSS INSPECTOR BEGIN");
        }

        private void OnDisable()
        {
            EditorActions.Clear();
            EditorState.Clear();
            EditorState.Repaint = null;

            states = null;

            Debug.Log("MSS INSPECTOR END");
        }

        #endregion

        #region Inspector

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();

            serializedObject.Update();

            GUILayout.BeginHorizontal();
                GUILayout.Space(-8);

                GUILayout.BeginVertical(/*GUI.skin.box*/);
                    GUILayout.Space(2);

                    GUILayout.BeginHorizontal();
                        GUILayout.Space(12);

                        //EditorGUI.BeginChangeCheck();

                        //states.statesGroup = EditorGUILayout.ObjectField(profileLabel, states.statesGroup, typeof(StatesGroup), false) as StatesGroup;

                        if (states.statesGroup == null && GUILayout.Button(newButton, GUILayout.Width(50), GUILayout.Height(14))) states.statesGroup = EditorGroup.CreateStatesProfile();

                        /*if (EditorGUI.EndChangeCheck())
                        {
                            EditorState.Clear();
                            EditorGroup.OnEnable(states.statesGroup);
                        }*/ 
                        
                    GUILayout.EndHorizontal();

                    if (states.statesGroup != null) EditorGroup.Draw(states.statesGroup);

                GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            EditorActions.Process();

            Event guiEvent = Event.current;
            if (guiEvent.type == EventType.ValidateCommand && guiEvent.commandName == "UndoRedoPerformed") OnUndo(states.statesGroup);

            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region Inspector callbacks

        private static void OnUndo(StatesGroup group)
        {
            if (group == null) return;

            //Debug.Log("YYY");

            return;

            EditorState.Reorder(group);
            EditorGroup.OnEnable(group);
            EditorActions.Clear();
            EditorState.CalculateAllTweensListsHeight();
        }

        #endregion

    }
}
 