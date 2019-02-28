using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Obel.MSS;
using UnityEditor.AnimatedValues;
using UnityEngine.Events;
using System.Linq;
using System;

namespace Obel.MSS.Editor
{

    
    public static class MSSStateEditorWrapperExtantions
    {
        public static MSSStateEditorWrapper GetWrapperForState(this List<MSSStateEditorWrapper> wrappers, MSSState state)
        {
            return wrappers.Where(w => w.state == state).FirstOrDefault();
        }

        public static void OpenLast(this List<MSSStateEditorWrapper> wrappers)
        {
            wrappers.ForEach(sw => sw.stateFade.target = false);
            wrappers.Last().stateFade.target = true;
        }
    }

    public class MSSStateEditorWrapper
    {
        public static MSSItem item;

        public MSSState state;
        public ReorderableList tweensList;
        public AnimBool stateFade;
        public AnimBool tweenFade;

        MSSStateEditorWrapper(MSSState state, UnityAction onRepaint)
        {
            this.state = state;

            stateFade = new AnimBool(false);
            stateFade.valueChanged.AddListener(onRepaint);

            tweenFade = new AnimBool(false);
            tweenFade.valueChanged.AddListener(onRepaint);

            InitTweensList();
        }

        public void InitTweensList()
        {
            tweensList = new ReorderableList(state.tweens, typeof(MSSTween), true, true, true, true);

            tweensList.drawElementCallback = (Rect rect, int index, bool selected, bool focused) => DrawTween(state.tweens[index], rect, index);
            tweensList.drawHeaderCallback = rect => UIDrawTweenListHeader(rect);
            tweensList.elementHeightCallback = (int index) => UIGetTweenHeight();
            tweensList.onAddCallback += rect => Addtween();
            tweensList.onRemoveCallback += rect => RemoveTween(state.tweens[tweensList.index]);
        }

        private void DrawTween(MSSTween tween, Rect position, int index = 0)
        {
            EditorGUI.LabelField(position, string.Format("{0} [{1}]", tween.name, index));
        }

        private void Addtween()
        {
            Undo.RecordObject(state.gameObject, "[MSS] add tween");

            state.AddTween();
        }

        private void RemoveTween(MSSTween tween)
        {
            Undo.RecordObject(state.gameObject, "[MSS] remove tween");
            state.tweens.Remove(tween);
        }

        private int UIGetTweenHeight()
        {
            return 60;
        }

        private void UIDrawTweenListHeader(Rect position)
        {
            EditorGUI.LabelField(position, "Tweens");
        }

        public static void Rebuild(MSSItem item, ref List<MSSStateEditorWrapper> wrappers, UnityAction onRepaint)
        {
            if (item != MSSStateEditorWrapper.item) { wrappers.Clear(); MSSStateEditorWrapper.item = item; }
            wrappers = wrappers.Where(w => item.states.Contains(w.state)).ToList();

            if (wrappers.Count != item.count)
                foreach (MSSState state in item.states)
                    if (!WrappersContainedState(wrappers, state)) wrappers.Add(new MSSStateEditorWrapper(state, onRepaint));
        }

        private static bool WrappersContainedState(List<MSSStateEditorWrapper> wrappers, MSSState state)
        {
            foreach (MSSStateEditorWrapper wrapper in wrappers) if (wrapper.state == state) return true;

            return false;
        }

        public void Draw()
        {
            //if (state.tweens == null) state.InitTweensList();
            if (tweensList == null) InitTweensList();

            try
            {
                tweensList.DoLayoutList();
            }
            catch (NullReferenceException ex)
            {
                InitTweensList();
                    
                Debug.Log((state.tweens == null) + " " + (tweensList == null));

                for (int i = 0; i < state.tweens.Count; i++)
                {
                    Debug.Log(state.name + " t" + i + " " + (state.tweens[i] == null));
                }
            }





        }

        /*
        public static void Generate(SerializedObject serializedObject, ref List<MSSStateEditorWrapper> wrappers, List<MSSState> states, UnityAction onRepaint = null)
        {   
            wrappers = wrappers.Where(w => states.Contains(w.state)).ToList();

            List<MSSState> statesToAdd = new List<MSSState>();

            foreach (MSSState state in states)
            {
                bool stateExist = false;
                foreach (MSSStateEditorWrapper wrapper in wrappers) if (wrapper.state == state) { stateExist = true; break; }
                if (!stateExist) statesToAdd.Add(state);
            }

            for (int i = 0; i < statesToAdd.Count; i++) wrappers.Add(new MSSStateEditorWrapper(i, statesToAdd[i], onRepaint));
        }

        public static void DrawAll(ref List<MSSStateEditorWrapper> wrappers)
        {
            foreach (MSSStateEditorWrapper wrapper in wrappers) wrapper.Draw();
        }

        public static void DrawAllInState(ref List<MSSStateEditorWrapper> wrappers, MSSState state)
        {
            GetWrapper(ref wrappers, state).Draw();
        }

        public static MSSStateEditorWrapper GetWrapper(ref List<MSSStateEditorWrapper> wrappers, MSSState state)
        {
            foreach (MSSStateEditorWrapper wrapper in wrappers) if (wrapper.state == state) return wrapper;
            return null;
        }

        public void Draw()
        {
            tweensList.DoLayoutList();
        }
        */
    }
    

    /*
    public class MSSStateWrapper
    {
        public MSSState state;

        public ReorderableList tweensList;
        public AnimBool stateFade;
        public AnimBool tweenFade;
    }
    */

    



    [CustomEditor(typeof(MSSItem))]
    public class MSSItemEditor : UnityEditor.Editor
    {
        #region Content

        private static readonly GUIContent statesHolderTitle = new GUIContent("States");

        #endregion

        #region Private

        private MSSItem drawingItem;
  
        private List<MSSStateEditorWrapper> statesWrappers = new List<MSSStateEditorWrapper>();
        private MSSStateEditorWrapper drawingStateWrapper;

        private MSSState drawingState;
        private SerializedObject serializedItem;

        private MSSState stateToRemove;

        #endregion

        public void OnDisable()
        {
            statesWrappers.Clear();
        }

        public void OnEnable()
        {
            drawingItem = (MSSItem)target;

            serializedItem = serializedObject;

            ReGenerateWrappers();
        }


        private void ReGenerateWrappers()
        {
            MSSStateEditorWrapper.Rebuild(drawingItem, ref statesWrappers, Repaint);

            /*
            if (statesWrappers.Count == drawingItem.count && MSSStateEditorWrapper.item == drawingItem) return;

            if (MSSStateEditorWrapper.item != drawingItem) statesWrappers.Clear();

            MSSStateEditorWrapper.Generate(serializedItem, ref statesWrappers, drawingItem.states, Repaint);
            MSSStateEditorWrapper.item = drawingItem;
            */
        }


        #region InspectorGUI

        public override void OnInspectorGUI()
        {
            ReGenerateWrappers();

            serializedObject.Update();

            EditorGUILayout.Space();

            foreach(MSSState state in drawingItem.states)
            {
                drawingStateWrapper = statesWrappers.GetWrapperForState(state);
                drawingState = state;

                MSSEditorUtils.DrawPanel(drawingStateWrapper.stateFade, state.name, OnDrawStatePanel, OnStateDrawHeader);
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("ADD NEW STATE")) OnAddState();

            RemoveMarkedStates();
            CloseInspector();
        }

        private void RemoveMarkedStates()
        {
            if (stateToRemove == null) return;

            Undo.RecordObject(drawingItem, "[MSS] remove state");

            drawingItem.RemoveState(stateToRemove);
            stateToRemove = null;
        }

        private void CloseInspector()
        {
            serializedObject.ApplyModifiedProperties();

            EditorUtility.SetDirty(target);

            DrawDefaultInspector();
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

            drawingStateWrapper.Draw();
        }


        public void OnAddState()
        {
            Undo.RecordObject(drawingItem, "[MSS] new state");

            drawingItem.AddState();
            ReGenerateWrappers();

            statesWrappers.OpenLast();
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
