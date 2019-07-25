using UnityEngine;
using UnityEditor;

namespace Obel.MSS.Editor
{
    internal static class DrawerState
    {
        #region Properties

        private static readonly GUIContent contentLabel = new GUIContent("Name"),
                                           delayLabel = new GUIContent("Delay"),
                                           durationLabel = new GUIContent("Duration"),
                                           testLabel = new GUIContent("Test");

        public static readonly float headerHeight = 20;

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
            EditorGUI.DrawRect(rectBackground, Color.white * 0.4f);

            Rect rectFoldOutBack = new Rect(rect.x, rect.y, rect.width, headerHeight);
            GUI.Box(rectFoldOutBack, GUIContent.none, GUI.skin.box);

            Rect rectStateTabColor = new Rect(rect.x, rect.y, 2, headerHeight);
            Color tabColor = Color.gray;
            if (editor.state.IsOpenedState) tabColor = EditorConfig.Colors.greenColor;
            if (editor.state.IsClosedState) tabColor = EditorConfig.Colors.redColor;
            EditorGUI.DrawRect(rectStateTabColor, tabColor);

            Rect rectToggle = new Rect(rect.x + 5, rect.y, 20, headerHeight);

            if (editor.state.IsDefaultState)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.Toggle(rectToggle, GUIContent.none, true);
                EditorGUI.EndDisabledGroup();
            }
            else
                EditorGUI.PropertyField(rectToggle, editor.serializedState.FindProperty("s_Enabled"), GUIContent.none);

            Rect rectFoldout = new Rect(rect.x + 34, rect.y + 2, rect.width - 54, headerHeight);
            editor.foldout.target = EditorGUI.Foldout(rectFoldout, editor.foldout.target, new GUIContent(editor.state.Name), true, EditorConfig.Styles.Foldout);

            if (!editor.state.IsDefaultState &&
                GUI.Button(new Rect(rect.width - 5, rect.y + 1, 30, headerHeight), EditorConfig.Content.iconToolbarMinus, EditorConfig.Styles.preButton))
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

            EditorLayout.SetPosition(rect.x, rect.y + headerHeight);

            EditorLayout.Control(nameFieldWidth, (Rect r) => EditorGUI.LabelField(r, "Name", FieldStyle));

            EditorLayout.SetWidth(timeFieldWidth);

            EditorLayout.Control((Rect r) => EditorGUI.LabelField(r, "Delay", FieldStyle));
            EditorLayout.Control((Rect r) => EditorGUI.LabelField(r, "Duration", FieldStyle));

            EditorLayout.Space(2);

            EditorLayout.Control(nameFieldWidth, (Rect r) =>
            {
                EditorGUI.BeginDisabledGroup(editor.state.IsDefaultState);
                EditorGUI.PropertyField(r, editor.serializedState.FindProperty("s_Name"), GUIContent.none);
                editor.state.name = string.Format("[State] {0}", editor.state.Name); // TODO WTF?
                EditorGUI.EndDisabledGroup();
            });

            EditorLayout.Control((Rect r) => EditorGUI.PropertyField(r, editor.serializedState.FindProperty("s_Delay"), GUIContent.none));
            EditorLayout.Control((Rect r) => EditorGUI.PropertyField(r, editor.serializedState.FindProperty("s_Duration"), GUIContent.none));

            EditorLayout.Space(6);

            EditorLayout.SetWidth(rect.width - EditorLayout.offset * 2);

            EditorLayout.Control((Rect r) => editor.tweensReorderableList.DoList(r));

            EditorLayout.PullColor();

            EditorGUI.EndDisabledGroup();
        }

        public static float GetHeight(EditorState editor)
        {
            float tweensHeight = 0;

            for (int i = 0; i < editor.state.Count; i++) tweensHeight += DrawerTween.GetHeight(editor.state[i]);

            return headerHeight + 6 + Mathf.Lerp(0, 77 + (editor.state.Count == 0 ? 14 : editor.TweensListHeight - 7), editor.foldout.faded);
        }

        #endregion

        #region Inspector callbacks

        private static void OnRemoveButton(State state)
        {
            StatesGroup group = (StatesGroup)state.Parent;

            EditorActions.Add(() =>
            {
                group.Remove(state, false);

                EditorAssets.Remove(state);
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(group));

                EditorState.Reorder(group);
            },
            group, "[MSS] Remove state");
        }

        #endregion
    }
}