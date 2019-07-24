using UnityEngine;
using UnityEditor;

namespace Obel.MSS.Editor
{
    [CustomPropertyDrawer(typeof(State))]
    internal class DrawerState : PropertyDrawer
    {
        #region Properties

        private static readonly GUIContent contentLabel = new GUIContent("Name"),
                                           delayLabel = new GUIContent("Delay"),
                                           durationLabel = new GUIContent("Duration"),
                                           testLabel = new GUIContent("Test");

        public static EditorStateValues editorValues;

        private SerializedProperty property;
        private GUIContent label;
        private Rect rect;

        public static readonly float headerHeight = 20;

        #endregion

        #region Inspector

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            if (editorValues == null) return;

            this.rect = rect;
            this.label = label;
            this.property = property;

            editorValues.serializedState.Update();

            DrawHeader();
            DrawProperties();

            editorValues.serializedState.ApplyModifiedProperties();
        }

        private void DrawHeader()
        {
            Rect rectBackground = new Rect(rect.x, rect.y, rect.width, rect.height - 6);
            EditorGUI.DrawRect(rectBackground, Color.white * 0.4f);

            Rect rectFoldOutBack = new Rect(rect.x, rect.y, rect.width, headerHeight);
            GUI.Box(rectFoldOutBack, GUIContent.none, GUI.skin.box);

            Rect rectStateTabColor = new Rect(rect.x, rect.y, 2, headerHeight);
            Color tabColor = Color.gray;
            if (editorValues.state.IsOpenedState) tabColor = EditorConfig.Colors.greenColor;
            if (editorValues.state.IsClosedState) tabColor = EditorConfig.Colors.redColor;
            EditorGUI.DrawRect(rectStateTabColor, tabColor);

            Rect rectToggle = new Rect(rect.x + 5, rect.y, 20, headerHeight);

            if (editorValues.state.IsDefaultState)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.Toggle(rectToggle, GUIContent.none, true);
                EditorGUI.EndDisabledGroup();
            }
            else
                EditorGUI.PropertyField(rectToggle, editorValues.serializedState.FindProperty("s_Enabled"), GUIContent.none);

            Rect rectFoldout = new Rect(rect.x + 34, rect.y + 2, rect.width - 54, headerHeight);
            editorValues.foldout.target = EditorGUI.Foldout(rectFoldout, editorValues.foldout.target,
                new GUIContent(editorValues.state.Name/* + " | " + editorValues.state.ID*/), true, EditorConfig.Styles.Foldout);

            if (!editorValues.state.IsDefaultState && 
                GUI.Button(new Rect(rect.width + 8, rect.y + 1, 30, headerHeight), EditorConfig.Content.iconToolbarMinus, EditorConfig.Styles.preButton))
                    OnRemoveButton();
        }

        private void DrawProperties()
        {
            if (editorValues.foldout.faded == 0) return;

            //EditorGUI.BeginProperty(rect, label, property);
            EditorGUI.BeginDisabledGroup(editorValues.foldout.faded < 0.2f || !editorValues.state.Enabled);

            float timeFieldWidth = 54;
            float nameFieldWidth = rect.width - timeFieldWidth * 2 - EditorLayout.offset * 4;

            GUIStyle FieldStyle = EditorConfig.Styles.greyMiniLabel;

            EditorLayout.PushColor();

            GUI.color *= Mathf.Clamp01(editorValues.foldout.faded - 0.5f) / 0.5f ;

            EditorLayout.SetPosition(rect.x, rect.y + headerHeight);

            EditorLayout.Control(nameFieldWidth, (Rect r) => EditorGUI.LabelField(r, "Name", FieldStyle));

            EditorLayout.SetWidth(timeFieldWidth);

            EditorLayout.Control((Rect r) => EditorGUI.LabelField(r, "Delay", FieldStyle));
            EditorLayout.Control((Rect r) => EditorGUI.LabelField(r, "Duration", FieldStyle));

            EditorLayout.Space(2);

            EditorLayout.Control(nameFieldWidth, (Rect r) =>
            {
                EditorGUI.BeginDisabledGroup(editorValues.state.IsDefaultState);
                EditorGUI.PropertyField(r, editorValues.serializedState.FindProperty("s_Name"), GUIContent.none);
                if (editorValues.state != null) editorValues.state.name = string.Format("[State] {0}", editorValues.state.Name);
                EditorGUI.EndDisabledGroup();
            });

            EditorLayout.Control((Rect r) => EditorGUI.PropertyField(r, editorValues.serializedState.FindProperty("s_Delay"), GUIContent.none));
            EditorLayout.Control((Rect r) => EditorGUI.PropertyField(r, editorValues.serializedState.FindProperty("s_Duration"), GUIContent.none));

            EditorLayout.Space(6);

            EditorLayout.SetWidth(rect.width - EditorLayout.offset * 2);

            editorValues.tweensListHeight = 0;
            EditorLayout.Control((Rect r) => editorValues.tweensReorderableList.DoList(r));

            EditorLayout.PullColor();

            EditorGUI.EndDisabledGroup();

            //EditorGUI.EndProperty();
        }

        #endregion

        #region Inspector callbacks

        private void OnRemoveButton()
        {
            State removingState = editorValues.state;
            StatesGroup removingStateGroup = (StatesGroup)removingState.Parent;

            // TODO! Remove tweens

            EditorActions.Add(() =>
            {
                removingStateGroup.Remove(removingState, false);

                EditorAssets.Remove(removingState);
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(removingStateGroup));

                EditorStateValues.Reorder(removingStateGroup.items);
            },
            removingStateGroup, "[MSS] Remove state");
        }

        #endregion
    }
}