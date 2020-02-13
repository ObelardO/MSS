using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEditor.AnimatedValues;
using UnityEditorInternal;
using Obel.MSS.Data;

namespace Obel.MSS.Editor
{
    internal class EditorState
    {
        #region Properties

        private static readonly GUIContent ContentLabel = new GUIContent("Name"),
                                           DelayLabel = new GUIContent("Delay"),
                                           DurationLabel = new GUIContent("Duration"),
                                           AddFirstTweenLabel = new GUIContent("Click + to add tween");

        private static readonly Dictionary<int, EditorState> Editors = new Dictionary<int, EditorState>();

        private Data.State _state;

        private readonly AnimBool _foldout;
        private ReorderableList _tweensList;
        private SerializedObject _serializedState;

        public static UnityAction Repaint;

        public float ListHeight { private set; get; }

        #endregion

        #region Public methods

        public EditorState(Data.State state)
        {
            _foldout = new AnimBool(false);
            _state = state;

            Editors.Add(state.Id, this);

            if (Repaint != null) _foldout.valueChanged.AddListener(Repaint);

            Enable();
        }

        public void Enable()
        {
            _tweensList = new ReorderableList(_state.Items as IList, typeof(Tween))
            {
                displayAdd = true,
                displayRemove = true,
                draggable = true,
                showDefaultBackground = true,

                headerHeight = 3,
                footerHeight = 50,

                onAddCallback = list => EditorTween.OnAddButton(_state),
                onRemoveCallback = list => EditorTween.OnRemoveButton(_state, list.index),
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, string.Empty),
                drawElementCallback = (rect, index, isActive, isFocused) => EditorTween.Draw(rect, _state[index]),
                elementHeightCallback = index => EditorTween.GetHeight(_state[index].GetType()),
                drawNoneElementCallback = rect => EditorGUI.LabelField(rect, AddFirstTweenLabel)
            };

            CalculateListHeight();
        }

        public void Open() => _foldout.target = true;

        public void Close() => _foldout.target = false;

        public static EditorState Get(Data.State state) => Editors.ContainsKey(state.Id) ? Editors[state.Id] : new EditorState(state);

        public static void Clear() => Editors.Clear();

        public static void CalculateAllListsHeight()
        {
            foreach (var editor in Editors) Editors[editor.Key].CalculateListHeight();
        }

        public static void Reorder(Group group)
        {
            foreach (var editor in Editors)
            {
                for (var i = 0; i < group.Count; i++)
                {
                    if (!editor.Key.Equals(group[i].Id) || Editors[editor.Key]._state.Equals(group[i])) continue;
                    Editors[editor.Key]._state = group[i];
                    Editors[editor.Key].Enable();
                }
            }
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

        public static void Draw(Rect rect, Data.State state) => Draw(rect, Get(state));

        public static void Draw(Rect rect, EditorState editor)
        {
            rect.width += 5;
            DrawHeader(rect, editor);
            DrawProperties(rect, editor);
        }

        public static void DrawBackground(Rect rect, int index, bool isActive, bool isFocused) => EditorGUI.DrawRect(rect, Color.clear);

        public static float GetHeight(EditorState editor)
        {
            return EditorConfig.Sizes.SingleLine + 8 +
                  (EditorConfig.Sizes.LineHeight * 2 + 36 +
                  (editor._state.Count == 0 ? 14 : editor.ListHeight - 7)) *
                   editor._foldout.faded;
        }

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

            var rectToggle = new Rect(rect.x + 5, rect.y + 2, 20, EditorConfig.Sizes.SingleLine);
            if (editor._state.IsDefaultState)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.Toggle(rectToggle, GUIContent.none, true);
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                editor._state.Enabled = EditorLayout.PropertyField(rectToggle, editor._state.Enabled, EditorGUI.Toggle, InspectorStates.Record);
            }

            var rectFoldout = new Rect(rect.x + 34, rect.y + 2, rect.width - 116, EditorConfig.Sizes.SingleLine);
            editor._foldout.target = EditorGUI.Foldout(rectFoldout, editor._foldout.target, new GUIContent(editor._state.Name), true, EditorConfig.Styles.Foldout);

            var rectButton = new Rect(rect.width, rect.y + 3, 24, EditorConfig.Sizes.SingleLine);
            EditorGUI.BeginDisabledGroup(editor._state.IsDefaultState);
            if ( GUI.Button(rectButton, EditorConfig.Content.IconToolbarMinus, EditorConfig.Styles.PreButton)) editor.OnRemoveButton();
            EditorGUI.EndDisabledGroup();

            

            rectButton.x -= 24;
            if (GUI.Button(rectButton, EditorConfig.Content.IconReturn, EditorConfig.Styles.IconButton)) editor._state.Apply();

            rectButton.x -= 24;
            if (GUI.Button(rectButton, EditorConfig.Content.IconRecord, EditorConfig.Styles.IconButton)) EditorActions.Add(editor._state.Capture, InspectorStates.States, "Record state values");
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
            EditorLayout.SetWidth(nameFieldWidth);
            EditorGUI.BeginDisabledGroup(editor._state.IsDefaultState);
            editor._state.Name = EditorLayout.PropertyField(editor._state.Name, InspectorStates.Record);
            EditorGUI.EndDisabledGroup();

            EditorLayout.SetWidth(timeFieldWidth);
            EditorLayout.PropertyField(ref editor._state.Delay, EditorGUI.FloatField, InspectorStates.Record);
            EditorLayout.PropertyField(ref editor._state.Duration, EditorGUI.FloatField, InspectorStates.Record);

            EditorLayout.Space(6);
            EditorLayout.SetWidth(rect.width - EditorConfig.Sizes.Offset * 2);
            EditorLayout.Control(r => editor._tweensList.DoList(r));

            EditorLayout.PullColor();

            EditorGUI.EndDisabledGroup();
        }

        #endregion

        #region Inspector callbacks

        private void OnRemoveButton()
        {
            EditorActions.Add(() =>
            {
                Editors.Remove(_state.Id);
                _state.Group.RemoveState(_state);
                Reorder(_state.Group);
            },
            InspectorStates.States, "Remove state");
        }

        public void OnTweenAdded<T>(T tween) where T : Tween => 
            ListHeight += EditorTween.GetHeight(tween);
            
        public void OnTweenRemoving<T>(T tween) where T : Tween => 
            ListHeight -= EditorTween.GetHeight(tween);

        #endregion
    }
}
