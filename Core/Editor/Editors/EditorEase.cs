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

        public static void Draw(Rect rect, Tween tween)
        {
            if (!GUI.Button(rect, tween.EaseName, EditorStyles.foldoutHeader/* toolbarPopup*/)) return;

            _selectedTween = tween;
            EasesMenu.ShowAsContext();
        }

        #endregion

        #region Init

        [InitializeOnLoadMethod]
        private static void ApplicationStart()
        {
            EditorApplication.delayCall += () =>
            {
                Ease.BindAll(OnBind);
                EasesMenu.AddItem(new GUIContent("Default"), false, () => OnEaseMenu(Ease.DefaultFunc));
            };
        }

        #endregion

        #region Inspector callbacks

        private static void OnBind(Func<float, float, float> easeFunc, string path)
        {
            EasesMenu.AddItem(new GUIContent(path), false, () => OnEaseMenu(easeFunc));

            Debug.Log($"[MSS] [Editor] [Eases] {path} bonded");
        }

        private static void OnEaseMenu(Func<float, float, float> easeFunc)
        {
            _selectedTween.EaseFunc = easeFunc;
            //TODO UNDO (on undo event assign all ease method by restored name)
        }

        #endregion
    }
}

