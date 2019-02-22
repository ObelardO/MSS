using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Obel.MSS;
using UnityEditor.AnimatedValues;
using UnityEngine.Events;
using System.Linq;

namespace Obel.MSS.Editor
{

    public static class MSSStateWrapperExtantions
    {
        public static MSSStateWrapper GetWrapperForState(this List<MSSStateWrapper> wrappers, MSSState state)
        {
            return wrappers.Where(w => w.state == state).FirstOrDefault();
        }
    }

    public class MSSStateWrapper
    {
        public MSSItem parent;
        public MSSState state;
        public ReorderableList tweens;
        public AnimBool stateFade;
        public AnimBool tweenFade;

        MSSStateWrapper(SerializedObject serializedObject, MSSState state, UnityAction onRepaint = null)
        {
            this.state = state;

            tweens = new ReorderableList(serializedObject, serializedObject.FindProperty("Tweens"), true, true, true, true);

            stateFade = new AnimBool(false);
            tweenFade = new AnimBool(false);

            if (onRepaint == null)
            {
                stateFade.target = false;
                tweenFade.target = false;
            }
            else
            {
                stateFade.valueChanged.AddListener(onRepaint);
                tweenFade.valueChanged.AddListener(onRepaint);
            }

            tweens = new ReorderableList(state.tweens, typeof(IMSSTween), true, true, true, true);

            tweens.drawElementCallback = (Rect rect, int index, bool selected, bool focused) =>
            {
                //SerializedProperty property = serializedObject.FindProperty("state\tweens").GetArrayElementAtIndex(index);
                //EditorGUI.PropertyField(rect, property, GUIContent.none);
                EditorGUI.LabelField(rect, state.tweens[index].name);
            };
            tweens.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Tweens");
            };
            tweens.elementHeightCallback = (int index) =>
            {
                float height = 60;
                return height;
            };
            tweens.onAddCallback += rect =>
            {
                Undo.RecordObject(serializedObject.targetObject, "[MSS] add tween");
                state.tweens.Add(new MSSTweenPosition(state));
            };
            tweens.onRemoveCallback += rect =>
            {
                Undo.RecordObject(serializedObject.targetObject, "[MSS] remove tween");
                state.tweens.RemoveAt(tweens.index);
            };

        }

        public static void Generate(SerializedObject serializedObject, ref List<MSSStateWrapper> wrappers, List<MSSState> states, UnityAction onRepaint = null)
        {
            wrappers = wrappers.Where(w => states.Contains(w.state)).ToList();

            List<MSSState> statesToAdd = new List<MSSState>();

            foreach (MSSState state in states)
            {
                bool stateExist = false;
                foreach (MSSStateWrapper wrapper in wrappers) if (wrapper.state == state) { stateExist = true; break; }
                if (!stateExist) statesToAdd.Add(state);
            }

            foreach (MSSState state in statesToAdd)
                wrappers.Add(new MSSStateWrapper(serializedObject, state, onRepaint));
        }

        public static void DrawAll(ref List<MSSStateWrapper> wrappers)
        {
            foreach (MSSStateWrapper wrapper in wrappers) wrapper.Draw();
        }

        public static void DrawAllInState(ref List<MSSStateWrapper> wrappers, MSSState state)
        {
            GetWrapper(ref wrappers, state).Draw();
        }

        public static MSSStateWrapper GetWrapper(ref List<MSSStateWrapper> wrappers, MSSState state)
        {
            foreach (MSSStateWrapper wrapper in wrappers) if (wrapper.state == state) return wrapper;
            return null;
        }

        public void Draw()
        {
            tweens.DoLayoutList();
        }
    }


    [CustomEditor(typeof(MSSItem))]
    public class MSSItemEditor : UnityEditor.Editor
    {
        #region Content

        private static readonly GUIContent statesHolderTitle = new GUIContent("States");

        #endregion

        #region Private

        private static AnimBool foldOutStates;
        private static MSSItem item;
        private static List<MSSStateWrapper> statesWrappers = new List<MSSStateWrapper>();
        private static MSSStateWrapper drawingStateWrapper;
        private static MSSState drawingState;

        private static MSSState stateToRemove;

        #endregion

        public void OnDisable()
        {
            statesWrappers.Clear();
        }

        public void OnEnable()
        {
            if (foldOutStates == null) foldOutStates = new AnimBool(false);
            foldOutStates.valueChanged.AddListener(Repaint);

            item = (MSSItem)target;

            statesWrappers.Clear();
            ReGenerateWrappers();
        }

        private void ReGenerateWrappers()
        {
            if (statesWrappers.Count == item.states.Count) return;
            MSSStateWrapper.Generate(serializedObject, ref statesWrappers, item.states, Repaint);
        }

        #region InspectorGUI

        public override void OnInspectorGUI()
        {
            ReGenerateWrappers();

            serializedObject.Update();

            EditorGUILayout.Space();

            foreach(MSSState state in item.states)
            {
                drawingStateWrapper = statesWrappers.GetWrapperForState(state);
                drawingState = state;

                MSSEditorUtils.DrawPanel(drawingStateWrapper.stateFade, state.name, OnDrawStatePanel, OnStateDrawHeader);
            }

            //EditorGUILayout.Space();

            if (GUILayout.Button("ADD NEW STATE"))
            {
                OnAddState();
            }

            RemoveMarkedStates();
            CloseInspector();
        }

        private void RemoveMarkedStates()
        {
            if (stateToRemove == null) return;

            Undo.RecordObject(item, "[MSS] remove state");

            item.RemoveState(stateToRemove);
            stateToRemove = null;
        }

        private void CloseInspector()
        {
            serializedObject.ApplyModifiedProperties();

            EditorUtility.SetDirty(target);

            //DrawDefaultInspector();
        }

        public void OnDrawState(MSSState state)
        {
            drawingStateWrapper = statesWrappers.GetWrapperForState(state);
            drawingState = state;

            MSSEditorUtils.DrawPanel(drawingStateWrapper.stateFade, state.name, OnDrawStatePanel, OnStateDrawHeader);
        }

        public void OnStateDrawHeader()
        {
            if (GUILayout.Button("X"))
            {
                OnRemoveState(drawingState);
                ReGenerateWrappers();
                return;
            }
        }

        public void OnDrawStatePanel()
        {
            EditorGUILayout.Space();
            MSSStateWrapper.DrawAllInState(ref statesWrappers, drawingState);
        }


        public void OnAddState()
        {
            Undo.RecordObject(item, "[MSS] new state");

            foldOutStates.target = true;

            item.AddState();
            ReGenerateWrappers();

            statesWrappers.ForEach(sw => sw.stateFade.target = false);
            statesWrappers.Last().stateFade.target = true;
        }

        public void OnRemoveState(MSSState state)
        {
            stateToRemove = state;
        }

        public void OnUpState(MSSState state)
        {

        }

        public void OnDownState(MSSState state)
        {

        }

        #endregion
    }
}
