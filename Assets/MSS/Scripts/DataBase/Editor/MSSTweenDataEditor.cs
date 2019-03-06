using UnityEngine;
using UnityEditor;
using Obel.MSS;

namespace Obel.MSS.Editor
{
    public class MSSTweenDataEditor
    {
        #region GUI

        public static void OnGUI(MSSStateData stateData, MSSTweenData tweenData)
        {
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(tweenData.tweenName);
                if (GUILayout.Button("x")) MSSStateDataEditor.RemoveTweenData(tweenData, stateData);
            EditorGUILayout.EndHorizontal();

            if (tweenData == null) return;

            if (tweenData is MSSTweenDataPosition) MSSTweenDataPositionEditor.OnGUI(stateData, tweenData as MSSTweenDataPosition);
            if (tweenData is MSSTweenDataRotation) MSSTweenDataRotationEditor.OnGUI(stateData, tweenData as MSSTweenDataRotation);
        }

        #endregion

    }
}
