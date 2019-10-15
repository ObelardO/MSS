using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine.Events;
using UnityEditorInternal;
using Obel.MSS;

namespace Obel.MSS.Editor
{
    internal class EditorState
    {
        #region Properties

        private static readonly GUIContent ContentLabel = new GUIContent("Name"),
                                           DelayLabel = new GUIContent("Delay"),
                                           DurationLabel = new GUIContent("Duration");

        private static readonly Dictionary<int, EditorState> Editors = new Dictionary<int, EditorState>();

        private State _state;

        private readonly AnimBool _foldout;
        private readonly ReorderableList _list;
        private SerializedObject _serializedState;

        public static UnityAction Repaint;

        public float ListHeight { private set; get; }

        #endregion

        #region Public methods

        public EditorState(State state)
        {
            _foldout = new AnimBool(false);
            this._state = state;

            Editors.Add(state.Id, this);

            if (Repaint != null) _foldout.valueChanged.AddListener(Repaint);

            //serializedState = new SerializedObject(InspectorStates.states);//.FindProperty("statesGroup").FindPropertyRelative("itemes").serializedObject;

            _list = new ReorderableList(state.Items as IList, typeof(Tween))
            {
                displayAdd = true,
                displayRemove = true,
                draggable = true,
                showDefaultBackground = true,

                headerHeight = 3,
                footerHeight = 50,

                onAddCallback = list => EditorTween.OnAddButton(state),
                onRemoveCallback = list => EditorTween.OnRemoveButton(state, list.index),
                drawHeaderCallback = EditorTween.DrawHeader,
                drawElementCallback = (rect, index, isActive, isFocused) => EditorTween.Draw(rect, state[index]),
                elementHeightCallback = index => EditorTween.GetHeight(state[index].GetType()),
                drawNoneElementCallback = EditorTween.DrawEmptyList
            };

            CalculateListHeight();
        }

        public void Open() => _foldout.target = true;

        public static void Open(EditorState editor) => editor.Open();

        public void Close() => _foldout.target = false;

        public static void Close(EditorState editor) => editor.Close();

        public static EditorState Get(State state) => Editors.ContainsKey(state.Id) ? Editors[state.Id] : new EditorState(state);

        public static void Clear() => Editors.Clear();

        public static void CalculateAllListsHeight()
        {
            foreach (var editor in Editors) Editors[editor.Key].CalculateListHeight();
        }

        public static void Reorder(StatesGroup group)
        {
            foreach (var editor in Editors)
            {
                for (var i = 0; i < group.Count; i++)
                {
                    if (editor.Key.Equals(group[i].Id)) Editors[editor.Key]._state = group[i];
                }
            }

            CalculateAllListsHeight();
        }

        #endregion

        #region Private methods

        private void CalculateListHeight()
        {
            ListHeight = 0;
            for (var i = 0; i < _state.Count; i++) ListHeight += EditorTween.GetHeight(_state[i]);
        }

        #endregion

        #region Inspector

        public static void Draw(Rect rect, State state) => Draw(rect, Get(state));

        public static void Draw(Rect rect, EditorState editor)
        {
            rect.width += 5;

            //editor.serializedState.Update();

            DrawHeader(rect, editor);
            DrawProperties(rect, editor);

            //editor.serializedState.ApplyModifiedProperties();
        }

        public static void DrawBackground(Rect rect, int index, bool isActive, bool isFocused) => EditorGUI.DrawRect(rect, Color.clear);

        private static void DrawHeader(Rect rect, EditorState editor)
        {
            var rectBackground = new Rect(rect.x, rect.y, rect.width, rect.height - 6);
            EditorGUI.DrawRect(rectBackground, EditorConfig.Colors.LightGrey);

            var rectFoldOutBack = new Rect(rect.x, rect.y, rect.width, EditorConfig.Sizes.LineHeight);
            GUI.Box(rectFoldOutBack, GUIContent.none, GUI.skin.box);

            var rectStateTabColor = new Rect(rect.x, rect.y, 2, EditorConfig.Sizes.LineHeight);
            var tabColor = Color.gray;
            if (editor._state.IsOpenedState) tabColor = EditorConfig.Colors.Green;
            if (editor._state.IsClosedState) tabColor = EditorConfig.Colors.Red;
            EditorGUI.DrawRect(rectStateTabColor, tabColor);

            var rectToggle = new Rect(rect.x + 5, rect.y, 20, EditorConfig.Sizes.SingleLine);

            if (editor._state.IsDefaultState)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.Toggle(rectToggle, GUIContent.none, true);
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                editor._state.Enabled = EditorGUI.Toggle(rectToggle, editor._state.Enabled);
            }

            var rectFoldout = new Rect(rect.x + 34, rect.y + 2, rect.width - 54, EditorConfig.Sizes.SingleLine);
            editor._foldout.target = EditorGUI.Foldout(rectFoldout, editor._foldout.target, new GUIContent(editor._state.Name), true, EditorConfig.Styles.Foldout);

            var rectRemoveButton = new Rect(rect.width - 5, rect.y + (EditorConfig.Sizes.LineHeight - 20) * 0.5f, 30, 20);
            if (!editor._state.IsDefaultState &&
                GUI.Button(rectRemoveButton, EditorConfig.Content.IconToolbarMinus, EditorConfig.Styles.PreButton))
                OnRemoveButton(editor._state);
        }

        private static void DrawProperties(Rect rect, EditorState editor)
        {
            if (editor._foldout.faded < 0.01f) return;

            EditorGUI.BeginDisabledGroup(editor._foldout.faded < 0.2f || !editor._state.Enabled);

            const float timeFieldWidth = 54;
            var nameFieldWidth = rect.width - timeFieldWidth * 2 - EditorConfig.Sizes.Offset * 4;

            var fieldStyle = EditorConfig.Styles.GreyMiniLabel;

            EditorLayout.PushColor();

            GUI.color *= Mathf.Clamp01(editor._foldout.faded - 0.5f) / 0.5f;

            EditorLayout.SetPosition(rect.x, rect.y + EditorConfig.Sizes.SingleLine);

            EditorLayout.Control(nameFieldWidth, r => EditorGUI.LabelField(r, ContentLabel, fieldStyle));

            EditorLayout.SetWidth(timeFieldWidth);

            EditorLayout.Control(r => EditorGUI.LabelField(r, DelayLabel, fieldStyle));
            EditorLayout.Control(r => EditorGUI.LabelField(r, DurationLabel, fieldStyle));

            EditorLayout.Space(2);

            EditorLayout.Control(nameFieldWidth, r =>
            {
                EditorGUI.BeginDisabledGroup(editor._state.IsDefaultState);
                editor._state.Name = EditorGUI.TextField(r, editor._state.Name);
                EditorGUI.EndDisabledGroup();
            });

            EditorLayout.Control(r => EditorGUI.FloatField(r, editor._state.Delay));
            EditorLayout.Control(r => EditorGUI.FloatField(r, editor._state.Duration));

            EditorLayout.Space(6);

            EditorLayout.SetWidth(rect.width - EditorConfig.Sizes.Offset * 2);

            EditorLayout.Control(r => editor._list.DoList(r));

            EditorLayout.PullColor();

            EditorGUI.EndDisabledGroup();
        }

        public static float GetHeight(State state) => GetHeight(Get(state));

        public static float GetHeight(EditorState editor)
        {
            return (EditorConfig.Sizes.SingleLine + 8 +
                   (EditorConfig.Sizes.LineHeight * 2 + 36 +
                   (editor._state.Count == 0 ? 14 : editor.ListHeight - 7)) *
                    editor._foldout.faded);
        }

        #endregion

        #region Inspector callbacks

        private static void OnRemoveButton(State state)
        {
            var group = (StatesGroup)state.Parent;

            EditorActions.Add(() =>
            {
                group.Remove(state, false);
                Reorder(group);
            },
            InspectorStates.States, "Remove state");
        }

        public void OnTweenAdded<T>(T tween) where T : Tween
        {
            ListHeight += EditorTween.GetHeight(tween);
            Repaint();
        }

        public void OnTweenRemoving<T>(T tween) where T : Tween
        {
            ListHeight -= EditorTween.GetHeight(tween);
            Repaint();
        }

        #endregion
    }
}
