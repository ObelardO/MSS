using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;

namespace Obel.MSS.Editor
{
    public abstract class EditorGenericTween<T, C, V> : IGenericTweenEditor

        where T : GenericTween<C, V>
        where C : Component
        where V : struct
    {
        #region Properties

        public virtual string Name { get; }
        public virtual string DisplayName { get; private set; }
        public virtual float Height { get; }
        public virtual bool IsMultiple => false;

        public float HeaderHeight => EditorConfig.Sizes.LineHeight * (DrawValueFunc == null ? 2 : 3);
        public float TotalHeight => HeaderHeight + Height;

        public Type Type { get; set; }
        public Action<State> AddAction { get; set; }
        public Func<Rect, GUIContent, V, V> DrawValueFunc { set; get; }

        public bool HasComponent(Tween tween) => HasComponent(tween as T);
        public bool HasComponent(T tween) => tween.Component;

        private GUIContent _content = GUIContent.none;

        #endregion

        #region Public methods

        public void SetDisplayName(GUIContent content)
        {
            DisplayName = Name.Contains("/") ? Name.Split('/').Last() : Name;
            _content = content ?? new GUIContent(DisplayName);
        }

        #endregion

        #region Inspector

        public virtual void Draw(Rect rect, Tween tween) => Draw(rect, tween as T);

        public virtual void Draw(Rect rect, T tween) => EditorGUI.HelpBox(rect, $"no drawer for tween: { Name }", MessageType.Warning);

        public void DrawHeader(Rect rect, Tween tween) => DrawHeader(rect, tween as T);

        public void DrawHeader(Rect rect, T tween)
        {
            EditorLayout.SetSize(new Vector2(rect.width, rect.height));
            EditorLayout.SetPosition(rect.x, rect.y);

            EditorGUI.BeginDisabledGroup(!tween.Component);

            EditorLayout.Control(18, r =>
            {
                var tweenEnabled = EditorGUI.ToggleLeft(r, GUIContent.none, tween.Enabled, EditorStyles.label);
                if (tweenEnabled != tween.Enabled)
                    EditorActions.Add(() => tween.Enabled = tweenEnabled, InspectorStates.States);
            });

            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(!tween.Enabled);

                EditorLayout.Control(rect.width - EditorConfig.Sizes.Offset * 5 - 150, r =>
                {
                    EditorGUI.LabelField(r, DisplayName, EditorStyles.label);
                });

                EditorLayout.Control(100, r =>
                {
                    if (tween.EaseFunc != null) EditorEase.Draw(r, tween);
                    else EditorGUI.HelpBox(r, tween.EaseName, MessageType.Warning);
                });

                EditorLayout.Control(18, r =>
                {
                    if (GUI.Button(r, EditorConfig.Content.IconRecord, EditorConfig.Styles.IconButton))
                        EditorActions.Add(() => Capture(tween), InspectorStates.States);
                });

                EditorLayout.Control(18, r =>
                {
                    if (GUI.Button(r, EditorConfig.Content.IconReturn, EditorConfig.Styles.IconButton))
                        EditorActions.Add(tween.Apply, tween.Component);
                });

                EditorLayout.Space(0);

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

            EditorGUI.EndDisabledGroup();

            if (!tween.Component)
            {
                var rectWarning = new Rect(rect.x, rect.y -EditorConfig.Sizes.Offset, rect.width + EditorConfig.Sizes.Offset * 2, TotalHeight - EditorConfig.Sizes.Offset);
                EditorGUI.DrawRect(rectWarning, EditorConfig.Colors.EditorBackGrey);
                EditorGUI.HelpBox(rectWarning, $"Tween {DisplayName} require {typeof(C)} component!", MessageType.Warning);

                var rectButton = new Rect(rect.width, rect.y + rect.height - 30, 50, 20);

                return;
            }

            if (DrawValueFunc == null) return;

            EditorGUI.BeginDisabledGroup(!tween.Enabled);

                EditorLayout.Space(0);
                EditorLayout.SetWidth(rect.width);
                EditorLayout.PropertyField(ref tween.Value, DrawValueFunc, InspectorStates.Record, _content);

            EditorGUI.EndDisabledGroup();
        }

        #endregion

        #region Inspector callbacks

        public virtual void Capture(T tween)
        {
            tween.Capture();
        }

        #endregion
    }

    public static class EditorTween
    {
        #region Properties

        private static readonly List<IGenericTweenEditor> Editors = new List<IGenericTweenEditor>();

        #endregion

        #region Init

        [InitializeOnLoadMethod]
        private static void OnApplicationStart() 
        {
            UnityEngine.Debug.Log("== All Assemblies ==");
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.FullName.StartsWith("MSS")) continue;
                 
                foreach (var type in assembly.GetTypes()) 
                {
                    if (type.IsClass && !type.IsAbstract /*&& type.IsPublic */&& typeof(IGenericTweenEditor).IsAssignableFrom(type))
                    {
                        Debug.Log(type);
                    }
                    
                }


            }
        }

        #endregion

        #region Inspector

        public static void Draw(Rect rect, Tween tween)
        {
            IGenericTweenEditor editor = Get(tween.GetType());

            if (editor == null)
            {
                EditorGUI.HelpBox(rect, $"unknown tween module: \"{tween}\"", MessageType.Warning);
                return;
            }

            rect.height -= EditorConfig.Sizes.Offset;
            GUI.Box(rect, string.Empty, EditorStyles.helpBox);

            rect.y += EditorConfig.Sizes.Offset;
            rect.width -= EditorConfig.Sizes.Offset * 2;
            rect.height = EditorConfig.Sizes.LineHeight;

            editor.DrawHeader(rect, tween);

            if (editor.Height <= 0) return;

            rect.y += editor.HeaderHeight - EditorConfig.Sizes.Offset * 3;
            rect.x += EditorConfig.Sizes.Offset;
            rect.height = editor.Height;

            if (!editor.HasComponent(tween)) return;

            EditorGUI.BeginDisabledGroup(!tween.Enabled);
            editor.Draw(rect, tween);
            EditorGUI.EndDisabledGroup();
        }

        public static float GetHeight<T>(T tween) where T : Tween => GetHeight(tween.GetType());

        public static float GetHeight(Type type) => Get(type)?.TotalHeight ?? EditorConfig.Sizes.SingleLine;

        #endregion

        #region Inspector callbacks

        public static void Add<T, C, V>(EditorGenericTween<T, C, V> editor, Func<Rect, GUIContent, V, V> drawValueFunc = null, GUIContent content = null)

            where T : GenericTween<C, V>, new()
            where V : struct
            where C : Component
        {
            if (Editors.Contains(editor)) return;

            Editors.Add(editor);
            editor.Type = typeof(T);
            editor.SetDisplayName(content);
            editor.DrawValueFunc = drawValueFunc;
            editor.AddAction = state => state.CreateTween<T>();

            Debug.Log($"[MSS] [Editor] [Tween] Registered: {editor.DisplayName}");
        }

        public static IGenericTweenEditor Get<T>(T tween) where T : Tween => Get(tween.GetType());

        public static IGenericTweenEditor Get(Type type) => Editors.FirstOrDefault(t => t.Type == type);

        public static void OnAddButton(State state)
        {
            var tweenMenu = new GenericMenu();

            foreach (var editor in Editors)
            {
                if (!editor.IsMultiple && state.Items.Any(t => t.GetType() == editor.Type))
                {
                    tweenMenu.AddDisabledItem(new GUIContent(editor.Name));
                    continue;
                }

                tweenMenu.AddItem(new GUIContent(editor.Name), false, () => 
                    EditorActions.Add(() =>
                    {
                        editor.AddAction(state);

                        //TODO move it to EditorState class
                        EditorState.Get(state).OnTweenAdded(state.Last);

                    }, 
                    InspectorStates.States, $"Add tween"));
            }

            tweenMenu.ShowAsContext();
        }

        public static void OnRemoveButton(State state, int index)
        {
            EditorActions.Add(() =>
            {
                //TODO move it to EditorState class
                EditorState.Get(state).OnTweenRemoving(state[index]);

                state.Remove(state[index]);
            }, 
            InspectorStates.States, "Remove tween");
        }

        #endregion
    }
}
