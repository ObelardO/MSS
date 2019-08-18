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


        //private static Dictionary<string, Func<float, float, float, float, float>> eases = new Dictionary<>


        // private static readonly List<IGenericTweenEditor> eases = new List<IGenericTweenEditor>();

        private static Dictionary<string, string> eases = new Dictionary<string, string>();

        [InitializeOnLoadMethod]
        private static void ApplicationStart()
        {
            Ease.onEaseAdded = OnEaseAdded;
        }

        private static void OnEaseAdded(string name, string path)
        {
            eases.Add(name, path);

            Debug.LogFormat("Ease \"{0}\" added. path: {1}", name, path);
        }

        public static void Draw(Rect rect, string name)
        {
            GUI.Button(rect, name, EditorStyles.popup);
        }

        #endregion
    }
}

