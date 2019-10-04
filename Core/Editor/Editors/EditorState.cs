using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine.Events;
using UnityEditorInternal;

namespace Obel.MSS.Editor
{
    internal class EditorState
    {
        #region Properties

        private static readonly GUIContent contentLabel = new GUIContent("Name"),
                                           delayLabel = new GUIContent("Delay"),
                                           durationLabel = new GUIContent("Duration");

        private static Dictionary<int, EditorState> editors = new Dictionary<int, EditorState>();

        public State state;
        public AnimBool foldout;
        public SerializedObject serializedState;
        public ReorderableList tweensReorderableList;

        public static UnityAction Repaint;

        public float TweensListHeight { private set; get; }

        #endregion

        #region Public methods

        public EditorState(State state)
        {
            foldout = new AnimBool(false);
            this.state = state;

            editors.Add(state.ID, this);

            if (Repaint != null) foldout.valueChanged.AddListener(Repaint);

            serializedState = new SerializedObject(InspectorStates.states);//.FindProperty("statesGroup").FindPropertyRelative("itemes").serializedObject;
           


            tweensReorderableList = new ReorderableList(state.items, typeof(Tween))
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
                drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => EditorTween.Draw(rect, state[index]),
                elementHeightCallback = index => EditorTween.GetHeight(state[index].GetType()),
                drawNoneElementCallback = EditorTween.DrawEmptyList
            };

            CalculateTweensListHeight();
        }

        public static EditorState Get(State state)
        {
            EditorState editor = null;

            if (editors.ContainsKey(state.ID))
                editor = editors[state.ID];
            else
                editor = new EditorState(state);

            return editor;
        }

        public static void Clear() => editors.Clear();

        public static void CalculateAllTweensListsHeight()
        {
            foreach (KeyValuePair<int, EditorState> editor in editors) editors[editor.Key].CalculateTweensListHeight();
        }

        public static void Reorder(StatesGroup group)
        {
            foreach (KeyValuePair<int, EditorState> editor in editors)
                for (int i = 0; i < group.Count; i++)
                    if (editor.Key.Equals(group[i].ID))
                        editors[editor.Key].state = group[i];
        }

        #endregion

        #region Private methods

        private void CalculateTweensListHeight()
        {
            TweensListHeight = 0;
            for (int i = 0; i < state.Count; i++) TweensListHeight += EditorTween.GetHeight(state[i]);
        }

        #endregion

        #region Inspector

        public static void Draw(Rect rect, State state) => Draw(rect, EditorState.Get(state));

        public static void Draw(Rect rect, EditorState editor)
        {
            rect.width += 5;

            editor.serializedState.Update();

            DrawHeader(rect, editor);
            DrawProperties(rect, editor);

            editor.serializedState.ApplyModifiedProperties();
        }

        public static void DrawBackground(Rect rect, int index, bool isActive, bool isFocused) => EditorGUI.DrawRect(rect, Color.clear);

        private static void DrawHeader(Rect rect, EditorState editor)
        {
            Rect rectBackground = new Rect(rect.x, rect.y, rect.width, rect.height - 6);
            EditorGUI.DrawRect(rectBackground, EditorConfig.Colors.lightGrey);

            Rect rectFoldOutBack = new Rect(rect.x, rect.y, rect.width, EditorConfig.Sizes.LineHeight);
            GUI.Box(rectFoldOutBack, GUIContent.none, GUI.skin.box);

            Rect rectStateTabColor = new Rect(rect.x, rect.y, 2, EditorConfig.Sizes.LineHeight);
            Color tabColor = Color.gray;
            if (editor.state.IsOpenedState) tabColor = EditorConfig.Colors.green;
            if (editor.state.IsClosedState) tabColor = EditorConfig.Colors.red;
            EditorGUI.DrawRect(rectStateTabColor, tabColor);

            Rect rectToggle = new Rect(rect.x + 5, rect.y, 20, EditorConfig.Sizes.singleLine);

            if (editor.state.IsDefaultState)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.Toggle(rectToggle, GUIContent.none, true);
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                editor.state.enabled = EditorGUI.Toggle(rectToggle, editor.state.enabled);
            }

            Rect rectFoldout = new Rect(rect.x + 34, rect.y + 2, rect.width - 54, EditorConfig.Sizes.singleLine);
            editor.foldout.target = EditorGUI.Foldout(rectFoldout, editor.foldout.target, new GUIContent(editor.state.Name), true, EditorConfig.Styles.Foldout);

            Rect rectRemoveButton = new Rect(rect.width - 5, rect.y + (EditorConfig.Sizes.LineHeight - 20) * 0.5f, 30, 20);
            if (!editor.state.IsDefaultState &&
                GUI.Button(rectRemoveButton, EditorConfig.Content.iconToolbarMinus, EditorConfig.Styles.preButton))
                OnRemoveButton(editor.state);
        }

        private static void DrawProperties(Rect rect, EditorState editor)
        {
            if (editor.foldout.faded == 0) return;

            EditorGUI.BeginDisabledGroup(editor.foldout.faded < 0.2f || !editor.state.enabled);

            float timeFieldWidth = 54;
            float nameFieldWidth = rect.width - timeFieldWidth * 2 - EditorConfig.Sizes.offset * 4;

            GUIStyle FieldStyle = EditorConfig.Styles.greyMiniLabel;

            EditorLayout.PushColor();

            GUI.color *= Mathf.Clamp01(editor.foldout.faded - 0.5f) / 0.5f;

            EditorLayout.SetPosition(rect.x, rect.y + EditorConfig.Sizes.singleLine);

            EditorLayout.Control(nameFieldWidth, r => EditorGUI.LabelField(r, "Name", FieldStyle));

            EditorLayout.SetWidth(timeFieldWidth);

            EditorLayout.Control(r => EditorGUI.LabelField(r, "Delay", FieldStyle));
            EditorLayout.Control(r => EditorGUI.LabelField(r, "Duration", FieldStyle));

            EditorLayout.Space(2);

            EditorLayout.Control(nameFieldWidth, r =>
            {
                EditorGUI.BeginDisabledGroup(editor.state.IsDefaultState);
                editor.state.Name = EditorGUI.TextField(r, editor.state.Name);
                EditorGUI.EndDisabledGroup();
            });

            EditorLayout.Control(r => EditorGUI.FloatField(r, editor.state.Delay));
            EditorLayout.Control(r => EditorGUI.FloatField(r, editor.state.Duration));

            EditorLayout.Space(6);

            EditorLayout.SetWidth(rect.width - EditorConfig.Sizes.offset * 2);

            EditorLayout.Control(r => editor.tweensReorderableList.DoList(r));

            EditorLayout.PullColor();

            EditorGUI.EndDisabledGroup();
        }

        public static float GetHeight(State state) => GetHeight(Get(state));

        public static float GetHeight(EditorState editor)
        {
            return (EditorConfig.Sizes.singleLine + 8 +
                   (EditorConfig.Sizes.LineHeight * 2 + 36 +
                   (editor.state.Count == 0 ? 14 : editor.TweensListHeight - 7)) * editor.foldout.faded);
        }

        #endregion

        #region Inspector callbacks

        private static void OnRemoveButton(State state)
        {
            StatesGroup group = (StatesGroup)state.Parent;

            EditorActions.Add(() =>
            {
                group.Remove(state, false);
                Reorder(group);
            },
            InspectorStates.states, "Remove state");
        }

        public void OnTweenAdded<T>(T tween) where T : Tween
        {
            TweensListHeight += EditorTween.GetHeight(tween);
            Repaint();
        }

        public void OnTweenRemoving<T>(T tween) where T : Tween
        {
            TweensListHeight -= EditorTween.GetHeight(tween);
            Repaint();
        }

        #endregion
    }
}
