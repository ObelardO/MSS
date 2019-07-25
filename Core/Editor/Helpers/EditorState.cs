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

        static private Dictionary<int, EditorState> statesDictionary = new Dictionary<int, EditorState>();

        public State state;
        public AnimBool foldout;
        public SerializedObject serializedState;
        public ReorderableList tweensReorderableList;

        public static UnityAction Repaint;

        public static EditorState Selected { private set; get; }

        public float TweensListHeight { private set; get; }

        #endregion

        #region Public methods

        public EditorState(State state)
        {
            foldout = new AnimBool(false);
            this.state = state;

            statesDictionary.Add(state.ID, this);

            if (Repaint != null) foldout.valueChanged.AddListener(Repaint);

            serializedState = new SerializedObject(state);

            tweensReorderableList = new ReorderableList(state.items, typeof(Tween))
            {
                displayAdd = true,
                displayRemove = true,
                draggable = true,
                showDefaultBackground = true,

                headerHeight = 3,
                footerHeight = 50,

                onAddCallback = (ReorderableList list) => EditorTween.OnAddButton(Selected.state),
                onRemoveCallback = (ReorderableList list) => EditorTween.OnRemoveButton(Selected.state, list.index),
                drawHeaderCallback = DrawerTween.DrawHeader,
                drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => DrawerTween.Draw(rect, state[index]),
                elementHeightCallback = (int index) => DrawerTween.GetHeight(state[index].GetType()),
                drawNoneElementCallback = DrawerTween.DrawEmptyList 
            };

            Select(state);

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

        public static void Select(State state)
        {
            Selected = Get(state);
        }

        public static void Reorder(List<State> reorderedStates)
        {
            foreach (KeyValuePair<int, EditorState> state in statesDictionary)
                for (int i = 0; i < reorderedStates.Count; i++)
                    if (state.Key.Equals(reorderedStates[i].ID))
                        statesDictionary[state.Key].state = reorderedStates[i];
        }

        #endregion

        private void CalculateTweensListHeight()
        {
            TweensListHeight = 0;
            for (int i = 0; i < state.Count; i++) TweensListHeight += DrawerTween.GetHeight(state[i]);
        }

        #region Inspector callbacks

        public void OnTweenAdded<T>(T tween) where T : Tween
        {
            TweensListHeight += EditorTween.Get(tween).Height;
            Repaint();
        }

        public void OnTweenRemoving<T>(T tween) where T : Tween
        {
            TweensListHeight -= EditorTween.Get(tween).Height;
            Repaint();
        }

        #endregion

    }
}
