using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Obel.MSS;

namespace Obel.MSS.Editor
{

    [CustomEditor(typeof(MSSTest))]
    public class MSSTestEditor : UnityEditor.Editor
    {
        #region Content

        private static readonly GUIContent goBtnContent = new GUIContent("GO");

        #endregion

        #region InspectorGUI

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspector();

            if (GUILayout.Button(goBtnContent))
            {
                Debug.Log("HI");
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
