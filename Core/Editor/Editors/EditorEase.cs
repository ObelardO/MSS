using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    public static class EditorEase
    {
        #region Properties

        public static bool HasEases { private set; get; }
        public static string FirstEaseName { private set; get; }

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
            EditorApplication.delayCall += () => Ease.BindAll((ease, path) => Bind(ease.Method.Name, path));
        }

        #region Inspector callbacks

        private static void Bind(string name, string path)
        {
            easesMenu.AddItem(new GUIContent(path), false, () => selectedTween.Ease = Ease.Get(name));

            if (!HasEases)
            {
                HasEases = true;
                FirstEaseName = name;
            }

            Debug.LogFormat("Ease \"{0}\" binded", name);
        }

        #endregion
    }
}

