using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    [CustomEditor(typeof(States))]
    public class InspectorStates : UnityEditor.Editor
    {
        #region Properties


        private static readonly GUIContent profileLabel = new GUIContent("States"),
                                           newButton = new GUIContent("New");

        private static States states;

        #endregion

        #region Unity methods

        private void OnEnable()
        {
            EditorActions.Clear();
            EditorState.Clear();
            EditorState.Repaint = Repaint;

            states = (States)target;

            DrawerGroup.OnEnable(states.statesGroup);
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

            GUILayout.BeginHorizontal();
                GUILayout.Space(-8);

                GUILayout.BeginVertical(GUI.skin.box);
                    GUILayout.Space(2);

                    GUILayout.BeginHorizontal();
                        GUILayout.Space(12);

                        EditorGUI.BeginChangeCheck();

                        states.statesGroup = EditorGUILayout.ObjectField(profileLabel, states.statesGroup, typeof(StatesGroup), false) as StatesGroup;

                        if (states.statesGroup == null && GUILayout.Button(newButton, GUILayout.Width(50), GUILayout.Height(14))) states.statesGroup = DrawerGroup.CreateStatesProfile();

                        if (EditorGUI.EndChangeCheck())
                        {
                            EditorState.Clear();
                            DrawerGroup.OnEnable(states.statesGroup);
                        }   
                    GUILayout.EndHorizontal();

                    DrawerGroup.Draw(states.statesGroup);

                GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            EditorActions.Process();

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
 