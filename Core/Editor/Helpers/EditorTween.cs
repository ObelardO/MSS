using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal class EditorGenericTween<T> : IGenericTweenEditor where T : Tween
    {
        #region Properties

        public virtual string Name { get; }

        public virtual Type @Type { get; set; }

        public Action AddAction { get; set; }

        private float s_Height = EditorGUIUtility.singleLineHeight;
        public virtual float Height {
            get => s_Height;
            set => s_Height = value;
        }

        #endregion

        #region Inspector

        public virtual void OnGUI(Rect rect, Tween tween) { OnGUI(rect, tween as T); }

        public virtual void OnGUI(Rect rect, T tween)
        {
            EditorGUI.HelpBox(rect, "no drawer for tween: \"" + Name + "\"", MessageType.Warning);
        }

        #endregion
    }

    internal static class EditorTween
    {
        #region Properties

        private static readonly List<IGenericTweenEditor> editors = new List<IGenericTweenEditor>();

        private static State selectedState;

        #endregion

        #region Inspector callbacks

        public static void Add<T>(EditorGenericTween<T> editor) where T : Tween
        {
            if (!editors.Contains(editor))
            {
                editors.Add(editor);
                editor.Type = typeof(T);
                editor.AddAction = () =>
                {
                    EditorActions.Add(() =>
                    {
                        T tween = EditorAssets.Save<T>(selectedState, string.Concat("[Tween] ", editor.Name));
                        selectedState.Add(tween);

                    }, selectedState);
                };

            }
        }

        public static IGenericTweenEditor Get(Type @Type)
        {
            return editors.Where(t => t.Type.Equals(Type)).FirstOrDefault();
        }

        public static void OnAddButton(ReorderableList list)
        {
            selectedState = EditorState.Selected.state;

            GenericMenu tweensMenu = new GenericMenu();

            foreach (IGenericTweenEditor editor in editors)
            {
                if (selectedState.items.Where(t => t.GetType() == editor.Type).Count() == 0)
                    tweensMenu.AddItem(new GUIContent(editor.Name), false, () => editor.AddAction());
            }

            tweensMenu.ShowAsContext();
        }

        public static void OnRemoveButton(ReorderableList list)
        {
            State state = EditorState.Selected.state;

            EditorActions.Add(() =>
            {
                Tween removingTween = state.items[list.index];

                state.Remove(removingTween, false);
                EditorAssets.Remove(removingTween);
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(state));
            },
            state);
        }

        #endregion
    }
}
