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

        static private Dictionary<int, EditorState> statesDictionary = new Dictionary<int, EditorState>();

        public State state;
        public AnimBool foldout;
        //public SerializedObject serializedState;
        public ReorderableList tweensReorderableList;

        public static UnityAction Repaint;

        public float TweensListHeight { private set; get; }

        public static readonly float HeaderHeight = 20;

        #endregion

        #region Public methods

        public EditorState(State state)
        {
            foldout = new AnimBool(false);
            this.state = state;

            statesDictionary.Add(state.ID, this);

            if (Repaint != null) foldout.valueChanged.AddListener(Repaint);

            //serializedState = new SerializedObject(state);

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

            if (statesDictionary.ContainsKey(state.ID))
                editor = statesDictionary[state.ID];
            else
                editor = new EditorState(state);

            return editor;
        }

        public static void Clear()
        {
            statesDictionary.Clear();
        }

        public static void CalculateAllTweensListsHeight()
        {
            foreach (KeyValuePair<int, EditorState> state in statesDictionary) statesDictionary[state.Key].CalculateTweensListHeight();
        }

        public static void Reorder(StatesGroup group)
        {
            foreach (KeyValuePair<int, EditorState> state in statesDictionary)
                for (int i = 0; i < group.Count; i++)
                    if (state.Key.Equals(group[i].ID))
                        statesDictionary[state.Key].state = group[i];
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

            //editor.serializedState.Update();

            DrawHeader(rect, editor);
            DrawProperties(rect, editor);

            //editor.serializedState.ApplyModifiedProperties();
        }

        public static void DrawBackground(Rect rect, int index, bool isActive, bool isFocused) => EditorGUI.DrawRect(rect, Color.clear);

        private static void DrawHeader(Rect rect, EditorState editor)
        {
            Rect rectBackground = new Rect(rect.x, rect.y, rect.width, rect.height - 6);
            EditorGUI.DrawRect(rectBackground, Color.white * 0.4f);

            Rect rectFoldOutBack = new Rect(rect.x, rect.y, rect.width, HeaderHeight);
            GUI.Box(rectFoldOutBack, GUIContent.none, GUI.skin.box);

            Rect rectStateTabColor = new Rect(rect.x, rect.y, 2, HeaderHeight);
            Color tabColor = Color.gray;
            if (editor.state.IsOpenedState) tabColor = EditorConfig.Colors.green;
            if (editor.state.IsClosedState) tabColor = EditorConfig.Colors.red;
            EditorGUI.DrawRect(rectStateTabColor, tabColor);

            Rect rectToggle = new Rect(rect.x + 5, rect.y, 20, HeaderHeight);

            if (editor.state.IsDefaultState)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.Toggle(rectToggle, GUIContent.none, true);
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                EditorGUI.Toggle(rectToggle, editor.state.Enabled);
            }

            Rect rectFoldout = new Rect(rect.x + 34, rect.y + 2, rect.width - 54, HeaderHeight);
            editor.foldout.target = EditorGUI.Foldout(rectFoldout, editor.foldout.target, new GUIContent(editor.state.Name), true, EditorConfig.Styles.Foldout);

            if (!editor.state.IsDefaultState &&
                GUI.Button(new Rect(rect.width - 5, rect.y + 1, 30, HeaderHeight), EditorConfig.Content.iconToolbarMinus, EditorConfig.Styles.preButton))
                OnRemoveButton(editor.state);
        }

        private static void DrawProperties(Rect rect, EditorState editor)
        {
            if (editor.foldout.faded == 0) return;

            EditorGUI.BeginDisabledGroup(editor.foldout.faded < 0.2f || !editor.state.Enabled);

            float timeFieldWidth = 54;
            float nameFieldWidth = rect.width - timeFieldWidth * 2 - EditorLayout.offset * 4;

            GUIStyle FieldStyle = EditorConfig.Styles.greyMiniLabel;

            EditorLayout.PushColor();

            GUI.color *= Mathf.Clamp01(editor.foldout.faded - 0.5f) / 0.5f;

            EditorLayout.SetPosition(rect.x, rect.y + HeaderHeight);

            EditorLayout.Control(nameFieldWidth, (Rect r) => EditorGUI.LabelField(r, "Name", FieldStyle));

            EditorLayout.SetWidth(timeFieldWidth);

            EditorLayout.Control((Rect r) => EditorGUI.LabelField(r, "Delay", FieldStyle));
            EditorLayout.Control((Rect r) => EditorGUI.LabelField(r, "Duration", FieldStyle));

            EditorLayout.Space(2);

            EditorLayout.Control(nameFieldWidth, (Rect r) =>
            {
                EditorGUI.BeginDisabledGroup(editor.state.IsDefaultState);
                EditorGUI.TextField(r, editor.state.Name);
                //editor.state.name = string.Format("[State] {0}", editor.state.Name); // TODO WTF?
                EditorGUI.EndDisabledGroup();
            });

            EditorLayout.Control((Rect r) => EditorGUI.FloatField(r, editor.state.Delay));
            EditorLayout.Control((Rect r) => EditorGUI.FloatField(r, editor.state.Duration));

            EditorLayout.Space(6);

            EditorLayout.SetWidth(rect.width - EditorLayout.offset * 2);

            EditorLayout.Control((Rect r) => editor.tweensReorderableList.DoList(r));

            EditorLayout.PullColor();

            EditorGUI.EndDisabledGroup();
        }

        public static float GetHeight(State state) => GetHeight(Get(state));

        public static float GetHeight(EditorState editor)
        {
            return HeaderHeight + 6 + Mathf.Lerp(0, 77 + (editor.state.Count == 0 ? 14 : editor.TweensListHeight - 7), editor.foldout.faded);
        }

        #endregion

        #region Inspector callbacks

        private static void OnRemoveButton(State state)
        {
            StatesGroup group = (StatesGroup)state.Parent;

            EditorActions.Add(() =>
            {
                group.Remove(state, false);

                //EditorAssets.Remove(state);
                //AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(group));

                Reorder(group);
            }/*,
            group, "[MSS] Remove state"*/);
        }

        public void OnTweenAdded<T>(T tween) where T : Tween
        {
            TweensListHeight += EditorTween.Get(tween)?.TotalHeight ?? EditorGUIUtility.singleLineHeight;
            Repaint();
        }

        public void OnTweenRemoving<T>(T tween) where T : Tween
        {
            TweensListHeight -= EditorTween.Get(tween)?.TotalHeight ?? EditorGUIUtility.singleLineHeight;
            Repaint();
        }

        #endregion
    }
}
