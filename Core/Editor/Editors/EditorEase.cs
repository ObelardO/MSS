using System;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal static class EditorEase
    {
        #region Properties

        private static readonly GenericMenu EasesMenu = new GenericMenu();
        private static Tween _selectedTween;

        #endregion

        #region Inspector

        public static void Draw(Rect rect, string name)
        {
            if (!GUI.Button(rect, name, EditorStyles.popup)) return;

            _selectedTween = EditorTween.SelectedTween;
            EasesMenu.ShowAsContext();
        }

        #endregion

        [InitializeOnLoadMethod]
        private static void ApplicationStart()
        {
            EditorApplication.delayCall += () => Ease.BindAll(Bind);
        }

        #region Inspector callbacks

        private static void Bind(Func<float, float, float> easeFunc, string path)
        {
            EasesMenu.AddItem(new GUIContent(path), false, () => OnEaseMenu(easeFunc));

            Debug.Log($"[MSS] [Editor] [Eases] {path} bonded");
        }

        private static void OnEaseMenu(Func<float, float, float> easeFunc)
        {
            _selectedTween.EaseFunc = easeFunc;

            //TODO UNDO (on undo event assign all ease method by restored name)
            //EditorActions.Add(() => { _selectedTween.EaseFunc = easeFunc; }, InspectorStates.States, "tween ease");
        }

        #endregion
    }
}

