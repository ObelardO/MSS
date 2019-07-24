using System.Collections.Generic;
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
        public float tweensListHeight;

        public static UnityAction updatingAction;

        public static EditorState Selected { private set; get; }

        #endregion

        #region Public methods

        public static void Select(State state)
        {
            Selected = Get(state);
        }

        public EditorState(State state)
        {
            foldout = new AnimBool(false);
            this.state = state;

            statesDictionary.Add(state.ID, this);

            if (updatingAction != null) foldout.valueChanged.AddListener(updatingAction);

            serializedState = new SerializedObject(state);

            tweensReorderableList = new ReorderableList(state.items, typeof(Tween))
            {
                displayAdd = true,
                displayRemove = true,
                draggable = true,
                showDefaultBackground = true,

                headerHeight = 3,
                footerHeight = 50,

                onAddCallback = EditorTween.OnAddButton,
                onRemoveCallback = EditorTween.OnRemoveButton,
                drawHeaderCallback = DrawerTween.DrawHeader,
                drawElementCallback = DrawerTween.Draw,
                drawNoneElementCallback = DrawerTween.DrawEmptyList,

                elementHeightCallback = DrawerTween.GetHeight

                
            };
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

        public static void Reorder(List<State> reorderedStates)
        {
            foreach (KeyValuePair<int, EditorState> entry in statesDictionary)
                for (int i = 0; i < reorderedStates.Count; i++)
                    if (entry.Key.Equals(reorderedStates[i].ID))
                        statesDictionary[entry.Key].state = reorderedStates[i];
        }

        #endregion
    }
}
