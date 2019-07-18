using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine.Events;
using UnityEditorInternal;

namespace Obel.MSS.Editor
{
    internal class EditorStateValues
    {
        #region Properties

        static private Dictionary<int, EditorStateValues> statesDictionary = new Dictionary<int, EditorStateValues>();

        public State state;
        public AnimBool foldout;
        public SerializedObject serializedState;
        public ReorderableList tweensReorderableList;

        public static UnityAction updatingAction;

        #endregion

        #region Public methods

        public EditorStateValues(State state)
        {
            foldout = new AnimBool(false);
            this.state = state;

            statesDictionary.Add(state.ID, this);

            if (updatingAction != null) foldout.valueChanged.AddListener(updatingAction);

            serializedState = new SerializedObject(state);

            tweensReorderableList = new ReorderableList(state.items, typeof(Tween)/*, false, true, true, true*/)
            {
                displayAdd = true,
                displayRemove = true,
                draggable = false,
                showDefaultBackground = true,

                onAddCallback = DrawerTween.OnAddButton,
                onRemoveCallback = DrawerTween.OnRemoveButton,
                drawHeaderCallback = DrawerTween.DrawHeader,
                drawElementCallback = DrawerTween.Draw
            };
        }

        public static EditorStateValues Get(State state)
        {
            EditorStateValues editorValues;

            if (statesDictionary.ContainsKey(state.ID))
                editorValues = statesDictionary[state.ID];
            else
                editorValues = new EditorStateValues(state);

            return editorValues;
        }

        public static void Clear()
        {
            statesDictionary.Clear();
        }

        public static void Reorder(List<State> reorderedStates)
        {
            foreach (KeyValuePair<int, EditorStateValues> entry in statesDictionary)
                for (int i = 0; i < reorderedStates.Count; i++)
                    if (entry.Key.Equals(reorderedStates[i].ID))
                        statesDictionary[entry.Key].state = reorderedStates[i];
        }

        #endregion
    }
}
