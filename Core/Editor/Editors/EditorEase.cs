using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    public static class EditorEase
    {
        #region Properties


        public static bool HasEases => eases.Count > 0;

        public static string FirstEaseName => HasEases ? eases.Keys.First() : null;

        private static Dictionary<string, string> eases = new Dictionary<string, string>();

        private static GenericMenu easesMenu = new GenericMenu();

        private static Tween selectedTween;

        #endregion

        #region Inspector

        public static void Draw(Rect rect, string name)
        {
            if (GUI.Button(rect, name, EditorStyles.popup))
            {
                selectedTween = EditorTween.selectedTween;
                easesMenu.ShowAsContext();
            }
        }

        #endregion

        [InitializeOnLoadMethod]
        private static void ApplicationStart()
        {
            Ease.onEaseAdded = OnEaseAdded;
        }

        private static void OnEaseAdded(string name, string path)
        {
            eases.Add(name, path);

            easesMenu.AddItem(new GUIContent(path), false, () => SelectEase(name));

            Debug.LogFormat("Ease \"{0}\" added. path: {1}", name, path);
        }



        private static void SelectEase(string name)
        {
            // TODO UNDO

            selectedTween.Ease = Ease.Get(name);
        }



    }
}

