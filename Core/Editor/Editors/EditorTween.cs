using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal class EditorGenericTween<T> : IGenericTweenEditor where T : Tween
    {
        #region Properties

        public virtual string Name { get; }
        public virtual string DisplayName { get; private set; }

        public virtual Type Type { get; set; }

        public Action AddAction { get; set; }

        private float s_Height = 0;
        public virtual float Height
        {
            get => s_Height;
            set => s_Height = value;
        }
        public float HeaderHeight => 24;

        public float TotalHeight => HeaderHeight + Height;

        #endregion

        #region Public methods

        public void SetDisplayName()
        {
            if (!Name.Contains("/"))
            {
                DisplayName = Name;
                return;
            }

            DisplayName = Name.Split('/').Last();
        }

        #endregion

        #region Inspector

        public virtual void Draw(Rect rect, Tween tween) => Draw(rect, tween as T);

        public virtual void Draw(Rect rect, T tween)
        {
            EditorGUI.HelpBox(rect, "no drawer for tween: \"" + Name + "\"", MessageType.Warning);
        }

        public void DrawHeader(Rect rect, Tween tween)
        {
            rect.height -= 2;
            GUI.Box(rect, string.Empty, EditorStyles.helpBox);
            rect.height += 2;
            rect.y += 2;

            EditorLayout.SetPosition(rect.x, rect.y);

            EditorLayout.Control(18, (Rect r) =>
            {
                bool tweenEnabled = EditorGUI.ToggleLeft(r, GUIContent.none, tween.Enabled);
                if (tweenEnabled != tween.Enabled) EditorActions.Add(() => tween.Enabled = tweenEnabled/*, tween*/);
            }
            );

            EditorGUI.BeginDisabledGroup(!tween.Enabled);

            EditorLayout.Control(100, (Rect r) => EditorGUI.LabelField(r, DisplayName, EditorStyles.popup));

            EditorLayout.Control(80, (Rect r) =>
            {
                if (tween.Ease != null) EditorEase.Draw(r, tween.Ease.Method.Name);
                else
                {
                    EditorGUI.HelpBox(r, tween.EaseName, MessageType.Warning);
                }
            }
            );

            EditorGUI.EndDisabledGroup();
        }

        #endregion
    }

    internal static class EditorTween
    {
        #region Properties

        private static readonly List<IGenericTweenEditor> editors = new List<IGenericTweenEditor>();

        public static State selectedState { get; private set; }
        public static Tween selectedTween { get; private set; }

        #endregion

        #region Inspector

        public static void DrawBackground(Rect rect, int index, bool isActive, bool isFocused) => EditorGUI.DrawRect(rect, Color.clear);

        public static void Draw(Rect rect, Tween tween)
        {
            IGenericTweenEditor editor = Get(tween.GetType());

            if (editors.Count == 0) Debug.Log("NO EDITORS");

            if (editor == null)
            {
                EditorGUI.HelpBox(rect, "unknown tween module: \"" + typeof(Tween).Name + "\"", MessageType.Warning);
                return;
            }

            selectedTween = tween;

            editor.DrawHeader(rect, tween);

            rect.y += editor.HeaderHeight;
            rect.height -= editor.HeaderHeight;

            EditorGUI.BeginDisabledGroup(!tween.Enabled);
            editor.Draw(rect, tween);
            EditorGUI.EndDisabledGroup();
        }

        public static void DrawHeader(Rect rect) => EditorGUI.LabelField(rect, "Tweens");

        public static void DrawEmptyList(Rect rect) => EditorGUI.LabelField(rect, "Click + to add tween");

        public static float GetHeight<T>(T tween) where T : Tween => GetHeight(tween.GetType());

        public static float GetHeight(Type @Type)
        {
            IGenericTweenEditor editor = EditorTween.Get(@Type);

            return editor == null ? EditorGUIUtility.singleLineHeight : editor.TotalHeight;
        }

        #endregion

        #region Inspector callbacks

        public static void Add<T>(EditorGenericTween<T> editor) where T : Tween, new()
        {
            if (!editors.Contains(editor))
            {
                editors.Add(editor);
                editor.Type = typeof(T);
                editor.SetDisplayName();
                editor.AddAction = () =>
                {
                    EditorActions.Add(() =>
                    {
                        T tween = new T();//EditorAssets.Save<T>(selectedState, string.Concat("[Tween] ", editor.Name));
                        selectedState.Add(tween);

                        if (EditorEase.HasEases) tween.Ease = Ease.Get(EditorEase.FirstEaseName);

                        selectedState.items.ForEach(t => Debug.Log(t.GetType()));

                        EditorState.Get(selectedState).OnTweenAdded(tween);

                    }/*, selectedState*/);
                };

                Debug.Log("ADDED EDITOR FOR TWEEN : " + editor.Name + " TYPE : " + editor.Type);
            }
        }

        public static IGenericTweenEditor Get<T>(T tween) => Get(tween.GetType());

        public static IGenericTweenEditor Get(Type type)
        {
            return editors.Where(t => t.Type.Equals(type)).FirstOrDefault();
        }

        public static void OnAddButton(State state)
        {
            selectedState = state;

            GenericMenu tweensMenu = new GenericMenu();



            foreach (IGenericTweenEditor editor in editors)
            {
                

                if (selectedState.items.Where(t => t.GetType() == editor.Type).Count() == 0)
                    tweensMenu.AddItem(new GUIContent(editor.Name), false, () => editor.AddAction());
            }



            tweensMenu.ShowAsContext();
        }

        public static void OnRemoveButton(State state, int index)
        {   
            EditorActions.Add(() =>
            {
                Tween tween = state[index];

                EditorState.Get(state).OnTweenRemoving(tween);

                state.Remove(tween, false);
                //EditorAssets.Remove(tween);
                //AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(state));

            }/*,
            state*/);
        }

        #endregion
    }
}
