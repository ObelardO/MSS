using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Obel.MSS;

namespace Obel.MSS.Editor
{
    public class EditorGenericTween<T, TU> : IGenericTweenEditor
        where T : GenericTween<TU>
        where TU : struct
    {
        #region Properties

        public virtual string Name { get; }
        public virtual string DisplayName { get; private set; }
        public virtual float Height { get; }
        public virtual bool Multiple => false;

        public float HeaderHeight => EditorConfig.Sizes.LineHeight * (DrawValueFunc == null ? 2.3f : 3.3f);
        public float TotalHeight => HeaderHeight + Height;

        public Type Type { get; set; }
        public Action AddAction { get; set; }
        public Func<Rect, string, TU, TU> DrawValueFunc { set; get; }

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
            EditorGUI.HelpBox(rect, $"no drawer for tween: { Name }", MessageType.Warning);
        }

        public void DrawHeader(Rect rect, Tween tween) => DrawHeader(rect, tween as T);

        public void DrawHeader(Rect rect, T tween)
        {
            //TODO Make margin method
            rect.height -= EditorConfig.Sizes.Offset;
            GUI.Box(rect, string.Empty, EditorStyles.helpBox);
            rect.height += EditorConfig.Sizes.Offset;
            rect.y += EditorConfig.Sizes.Offset;
            rect.width -= EditorConfig.Sizes.Offset * 2;

            EditorLayout.SetPosition(rect.x, rect.y);

            EditorLayout.Control(18, r =>
            {
                var tweenEnabled = EditorGUI.ToggleLeft(r, GUIContent.none, tween.Enabled);
                if (tweenEnabled != tween.Enabled)
                    EditorActions.Add(() => tween.Enabled = tweenEnabled, InspectorStates.States);
            });

            EditorGUI.BeginDisabledGroup(!tween.Enabled);

            EditorLayout.Control(100, r => EditorGUI.LabelField(r, DisplayName, EditorStyles.popup));

            EditorLayout.Control(80, r =>
            {
                if (tween.EaseFunc != null) EditorEase.Draw(r, tween.EaseFunc.Method.Name);
                else EditorGUI.HelpBox(r, tween.EaseFunc?.Method.Name, MessageType.Warning);
            });

            EditorLayout.Control(60, r =>
            {
                if (GUI.Button(r, "Capture")) EditorActions.Add(() =>
                {
                    tween.Capture(InspectorStates.States.gameObject);
                    Debug.Log($"[MSS] [Editor] [Tween] Say hello to new {DisplayName} tween!");
                },
                InspectorStates.States);
            });

            EditorLayout.Space();

            var rangeMin = tween.Range.x;
            var rangeMax = tween.Range.y;
            EditorLayout.Control(rect.width, r =>
            {
                EditorGUI.BeginChangeCheck();
                EditorGUI.MinMaxSlider(r, ref rangeMin, ref rangeMax, 0, 1);

                if (EditorGUI.EndChangeCheck())
                {
                    EditorActions.Add(() => tween.Range = new Vector2(rangeMin, rangeMax), InspectorStates.States, "tween range");
                }
            });

            if (DrawValueFunc != null)
            {
                EditorLayout.Space(2);
                EditorLayout.Control(rect.width, r =>
                {
                    EditorGUI.BeginChangeCheck();
                    var value = DrawValueFunc(r, DisplayName, tween.Value);
                    if (EditorGUI.EndChangeCheck())
                        EditorActions.Add(() => tween.Value = value, InspectorStates.States);
                });
            }

            EditorGUI.EndDisabledGroup();
        }

        #endregion
    }

    public static class EditorTween
    {
        #region Properties

        private static readonly List<IGenericTweenEditor> Editors = new List<IGenericTweenEditor>();

        public static State SelectedState { get; private set; }
        public static Tween SelectedTween { get; private set; }

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

            SelectedTween = tween;

            editor.DrawHeader(rect, tween);

            if (editor.Height <= 0) return;

            //TODO Make margin method
            rect.y += editor.HeaderHeight - EditorConfig.Sizes.Offset;
            rect.height = editor.Height - EditorConfig.Sizes.Offset;
            rect.x += EditorConfig.Sizes.Offset;
            rect.width -= EditorConfig.Sizes.Offset * 2;

            EditorGUI.BeginDisabledGroup(!tween.Enabled);
            editor.Draw(rect, tween);
            EditorGUI.EndDisabledGroup();
        }

        public static void DrawHeader(Rect rect) => EditorGUI.LabelField(rect, "");

        public static void DrawEmptyList(Rect rect) => EditorGUI.LabelField(rect, "Click + to add tween");

        public static float GetHeight<T>(T tween) where T : Tween => GetHeight(tween.GetType());

        public static float GetHeight(Type type) => Get(type)?.TotalHeight ?? EditorConfig.Sizes.SingleLine;

        #endregion

        #region Inspector callbacks

        public static void Add<T, TU>(EditorGenericTween<T, TU> editor, Func<Rect, string, TU, TU> drawValueFunc = null)
            where T : GenericTween<TU>, new()
            where TU : struct
        {
            if (Editors.Contains(editor)) return;

            Editors.Add(editor);
            editor.Type = typeof(T);
            editor.SetDisplayName();
            editor.DrawValueFunc = drawValueFunc;
            editor.AddAction = () => /*EditorActions.Add(() =>*/ //TODO Need or not? 
            {
                var tween = new T();
                SelectedState.Add(tween);

                if (Ease.DefaultFunc != null) tween.EaseFunc = Ease.DefaultFunc;

                EditorState.Get(SelectedState).OnTweenAdded(tween);

            }/*, InspectorStates.states.gameObject)*/;

            Debug.Log($"[MSS] [Editor] [Tween] Registered: {editor.DisplayName}");
        }

        public static IGenericTweenEditor Get<T>(T tween) => Get(tween.GetType());

        public static IGenericTweenEditor Get(Type type) => Editors.FirstOrDefault(t => t.Type == type);

        public static void OnAddButton(State state)
        {
            SelectedState = state;

            var tweenMenu = new GenericMenu();

            foreach (var editor in Editors)
            {
                if (editor.Multiple || SelectedState.Items.All(t => t.GetType() != editor.Type))
                    tweenMenu.AddItem(new GUIContent(editor.Name), false, () => EditorActions.Add(editor.AddAction, InspectorStates.States));
            }

            tweenMenu.ShowAsContext();
        }

        public static void OnRemoveButton(State state, int index)
        {
            EditorActions.Add(() =>
            {
                EditorState.Get(state).OnTweenRemoving(state[index]);
                state.Remove(state[index], false);
            },
            InspectorStates.States.gameObject);
        }

        #endregion
    }
}
