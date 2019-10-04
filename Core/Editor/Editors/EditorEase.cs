using System;
using UnityEditor;
using UnityEngine;

using Obel.MSS;

namespace Obel.MSS.Editor
{
    internal static class EditorEase
    {
        #region Properties

        private static GenericMenu easesMenu = new GenericMenu();
        private static Tween selectedTween;

        #endregion

        #region Inspector

        public static void Draw(Rect rect, string name)
        {
            if (!GUI.Button(rect, name, EditorStyles.popup)) return;

            selectedTween = EditorTween.SelectedTween;
            easesMenu.ShowAsContext();
        }

        #endregion

        [InitializeOnLoadMethod]
        private static void ApplicationStart()
        {
            EditorApplication.delayCall += () => Ease.BindAll(Bind);
        }

        #region Inspector callbacks

        private static void Bind(Func<float, float, float> ease, string path)
        {
            easesMenu.AddItem(new GUIContent(path), false, () => selectedTween.Ease = ease);
            Debug.Log($"[MSS] [Editor] [Eases] {path} binded");
        }

        #endregion
    }
}

