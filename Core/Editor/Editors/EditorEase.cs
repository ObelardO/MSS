using System;
using UnityEngine;
using UnityEditor;
using Obel.MSS.Data;
using Obel.MSS.Base;

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
            if (!GUI.Button(rect, tween.EaseName, EditorStyles.foldoutHeader)) return;

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
                Debug.Log("[MSS] [Editor] [Eases] Start binding...");
                Ease.ForEach((ease, path) => EasesMenu.AddItem(new GUIContent(path), false, () => _selectedTween.EaseFunc = ease));
                Debug.Log("[MSS] [Editor] [Eases] Binding done.");

                EasesMenu.AddSeparator(string.Empty);
                EasesMenu.AddItem(new GUIContent("Default"), false, () => _selectedTween.EaseFunc = Ease.DefaultFunc);
                EasesMenu.AddItem(new GUIContent("Linear"), false, () => _selectedTween.EaseFunc = Ease.Linear);
            };
        }

        #endregion
    }
}

