using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEditor;

namespace Obel.MSS.Editor
{
    public class MSSDataBaseEditorWindow : EditorWindow
    {
        private static GUIContent windowTitleContent = new GUIContent("MSS DataBase");

        [MenuItem("Window/MSS/DataBase")]
        static void Init()
        {
            MSSDataBaseEditorWindow w = GetWindow<MSSDataBaseEditorWindow>();
            w.titleContent = windowTitleContent;
            Undo.undoRedoPerformed = UndoCallback;
        }

        private static void UndoCallback()
        {
            Debug.Log("UNDO!!");
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(MSSBaseEditor.instance));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        #region GUI

        void OnGUI()
        {
            GUILayout.Label(windowTitleContent, EditorStyles.boldLabel);
            MSSBaseEditor.OnGUI();
        }

        #endregion
    }
}