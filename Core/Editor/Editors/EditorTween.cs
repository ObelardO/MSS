using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal class EditorGenericTween<T, U> : IGenericTweenEditor
        where T : GenericTween<U>
        where U : struct
    {
        #region Properties

        public virtual string Name { get; }
        public virtual string DisplayName { get; private set; }
        public virtual float Height { get; }
        public virtual bool Multiple => false;

        public float HeaderHeight => EditorConfig.Sizes.singleLine * 4;
        public float TotalHeight => HeaderHeight + Height;

        public Type Type { get; set; }
        public Action AddAction { get; set; }
        public Func<Rect, string, U, U> DrawValueFunc { set; get; }

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

        public void DrawHeader(Rect rect, Tween tween) => DrawHeader(rect, tween as T);

        public void DrawHeader(Rect rect, T tween)
        {
            rect.height -= 2;
            GUI.Box(rect, string.Empty, EditorStyles.helpBox);
            rect.height += 2;
            rect.y += 2;
            rect.width -= 8;

            EditorLayout.SetPosition(rect.x, rect.y);

            EditorLayout.Control(18, r =>
                {
                    bool tweenEnabled = EditorGUI.ToggleLeft(r, GUIContent.none, tween.Enabled);
                    if (tweenEnabled != tween.Enabled) EditorActions.Add(() => tween.Enabled = tweenEnabled, InspectorStates.states);
                }
            );

            EditorGUI.BeginDisabledGroup(!tween.Enabled);

            EditorLayout.Control(100, r => EditorGUI.LabelField(r, DisplayName, EditorStyles.popup));

            EditorLayout.Control(80, r =>
            {
                if (tween.Ease != null) EditorEase.Draw(r, tween.Ease.Method.Name);
                else EditorGUI.HelpBox(r, tween.EaseName, MessageType.Warning);
            });

            EditorLayout.Control(60, r =>
            {
                if (GUI.Button(r, "Capture")) EditorActions.Add(() => tween.Capture(InspectorStates.states.gameObject), InspectorStates.states);
            });

            EditorLayout.Space();

            EditorLayout.Control(rect.width, r =>
            {
                float rangeMin = tween.Range.x;
                float rangeMax = tween.Range.y;

                EditorGUI.BeginChangeCheck();
                EditorGUI.MinMaxSlider(r, ref rangeMin, ref rangeMax, 0, 1);

                if (EditorGUI.EndChangeCheck())
                {
                    EditorActions.Add(() => tween.Range = new Vector2(rangeMin, rangeMax), InspectorStates.states.gameObject, "tween range");
                }
            });

            EditorLayout.Space();
            EditorLayout.Control(rect.width, r =>
            {
                //r.height = 15;
                EditorGUI.BeginChangeCheck();
                U value = DrawValueFunc(r, DisplayName, tween.Value);
                if (EditorGUI.EndChangeCheck()) EditorActions.Add(() => tween.Value = value, InspectorStates.states.gameObject);
            });

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

            if (editor == null)
            {
                EditorGUI.HelpBox(rect, "unknown tween module: \"" + tween + "\"", MessageType.Warning);
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
            IGenericTweenEditor editor = Get(@Type);
            return editor == null ? EditorConfig.Sizes.singleLine : editor.TotalHeight;
        }

        #endregion

        #region Inspector callbacks

        public static void Add<T, U>(EditorGenericTween<T, U> editor, Func<Rect, string, U, U> drawValueFunc)
            where T : GenericTween<U>
            where U : struct
        {
            if (!editors.Contains(editor))
            {
                editors.Add(editor);
                editor.Type = typeof(T);
                editor.SetDisplayName();
                editor.DrawValueFunc = drawValueFunc;
                editor.AddAction = () =>
                {
                    EditorActions.Add(() =>
                    {
                        //TODO Remove activator
                        T tween = (T)Activator.CreateInstance(typeof(T));// EditorAssets.Save<T>(selectedState, string.Concat("[Tween] ", editor.Name));
                        selectedState.Add(tween);

                        if (EditorEase.HasEases) tween.Ease = Ease.Get(EditorEase.FirstEaseName);

                        EditorState.Get(selectedState).OnTweenAdded(tween);

                    }, InspectorStates.states.gameObject);
                };
            }
        }

        public static IGenericTweenEditor Get<T>(T tween) => Get(tween.GetType());

        public static IGenericTweenEditor Get(Type type) => editors.Where(t => t.Type.Equals(type)).FirstOrDefault();

        public static void OnAddButton(State state)
        {
            selectedState = state;

            GenericMenu tweensMenu = new GenericMenu();

            foreach (IGenericTweenEditor editor in editors)
            {
                if (editor.Multiple || selectedState.items.Where(t => t.GetType() == editor.Type).Count() == 0)
                    tweensMenu.AddItem(new GUIContent(editor.Name), false, () => editor.AddAction());
            }

            tweensMenu.ShowAsContext();
        }

        public static void OnRemoveButton(State state, int index)
        {   
            EditorActions.Add(() =>
            {
                EditorState.Get(state).OnTweenRemoving(state[index]);
                state.Remove(state[index], false);
            },
            InspectorStates.states.gameObject);
        }

        #endregion
    }
}
