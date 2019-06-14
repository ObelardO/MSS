using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditorInternal;

namespace Obel.MSS.Editor
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(StatesBehaviour))]
    public class InspectorStatesBehaviour : Editor, IDataBaseEditor<StatesGroup>
    {
        #region Properties

        private StatesBehaviour statesBehaviour;
        private SerializedObject serializedStatesGroup;
        private SerializedProperty statesProperty;
        private ReorderableList statesReorderableList;
        //private Dictionary<string, ReorderableList> tweensListDictionary = new Dictionary<string, ReorderableList>();

        #endregion

        #region Unity methods

        private void OnEnable()
        {
            if (!(target is StatesBehaviour)) return;

            statesBehaviour = (StatesBehaviour)target;

            StateEditorValues.Clear();
            StateEditorValues.updatingAction = Repaint;

            if (statesBehaviour.statesGroup == null) statesBehaviour.statesGroup = Add();

            SerializedProperty statesGroupProperty = serializedObject.FindProperty("statesGroup");

            serializedStatesGroup = new SerializedObject(statesGroupProperty.objectReferenceValue);

            statesProperty = serializedStatesGroup.FindProperty("items");

            statesReorderableList = new ReorderableList(serializedObject, statesProperty)
            {
                displayAdd = false,
                displayRemove = false,
                draggable = true,

                headerHeight = 0,
                footerHeight = 0,

                showDefaultBackground = false,

                drawElementBackgroundCallback = DrawStateBackground,
                drawElementCallback = DrawState,
                elementHeightCallback = GetStateHeight,
                onReorderCallback = OnReordered
            };
        }

        #endregion

        #region Inspector

        public override void OnInspectorGUI()
        {
            serializedStatesGroup.Update();

            EditorGUI.BeginDisabledGroup(!statesBehaviour.enabled);
            GUILayout.Space(6);

            statesReorderableList.DoLayoutList();

            GUILayout.Space(3);

            if (GUILayout.Button("Add Stete"))
            {
                EditorActions.Add(() =>
                {
                    State state = EditorDataBase.SaveAsset<State>();


                    statesBehaviour.statesGroup.Add(state);


                    StateEditorValues.Clear();
                    //StateEditorValues.Reorder(statesBehaviour.statesGroup.items);
                    StateEditorValues.Get(state).foldout.target = true;



                    AssetDatabase.ImportAsset(EditorDataBase.AssetPath);
                    //AssetDatabase.Refresh();
                },
                statesBehaviour.statesGroup, "[MSS] Add State");
            }

            EditorGUI.EndDisabledGroup();

            Event guiEvent = Event.current;
            if (guiEvent.type == EventType.ValidateCommand && guiEvent.commandName == "UndoRedoPerformed")
            {
                //StateEditorValues.Clear();
                StateEditorValues.Reorder(statesBehaviour.statesGroup.items);


                AssetDatabase.ImportAsset(EditorDataBase.AssetPath);
            }

            EditorActions.Process();

            serializedStatesGroup.ApplyModifiedProperties();
        }

        #endregion

        #region Inspector callbacks

        private void OnReordered(ReorderableList list)
        {
            StateEditorValues.Reorder(statesBehaviour.statesGroup.items);
        }

        private void DrawStateBackground(Rect rect, int index, bool isActive, bool isFocused)
        {
            EditorGUI.DrawRect(rect, Color.clear);
        }

        private void DrawState(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.width += 5;

            DrawerState.editorValues = StateEditorValues.Get(statesBehaviour.statesGroup.items[index]);

            SerializedProperty stateProperty = statesProperty.GetArrayElementAtIndex(index);

            EditorGUI.PropertyField(rect, stateProperty, true);

            //if (index > 1)
            {
                if (GUI.Button(new Rect(rect.width + 5, rect.y + 1, 30, 20), EditorConfig.Content.iconToolbarMinus, EditorConfig.Styles.preButton))
                {
                    Debug.Log("REMOVE STATE HERE!");

                    EditorActions.Add(() =>
                    {
                        State removingState = statesBehaviour.statesGroup[index];
                        statesBehaviour.statesGroup.Remove(removingState, false);
                        EditorDataBase.RemoveAsset(removingState);

                        StateEditorValues.Reorder(statesBehaviour.statesGroup.items);
                    },
                    statesBehaviour.statesGroup, "[MSS] Remove state");
                }
            }
        }

        private float GetStateHeight(int index)
        {
            int tweensCount = statesBehaviour.statesGroup.items[index].items.Count;

            StateEditorValues editorValues = StateEditorValues.Get(statesBehaviour.statesGroup.items[index]);

            return 26 + Mathf.Lerp(0, 70 + (tweensCount == 0 ? 0 : tweensCount - 1) * 21 + 40,
                       editorValues.foldout.faded);
        }

        #endregion

        /*EditorGUI.LabelField(rect, DrawerState.editorValues.state.name + " | " + statesBehaviour.states[index].name);

        SerializedProperty tweensProperty = stateProperty.FindPropertyRelative("tweens");

        string tweenListKey = stateProperty.propertyPath;

        ReorderableList tweensReorderableList;

        if (tweensListDictionary.ContainsKey(tweenListKey))
        {
            tweensReorderableList = tweensListDictionary[tweenListKey];
        }
        else
        {
            tweensReorderableList = new ReorderableList(serializedObject, tweensProperty)
            {
                displayAdd = true,
                displayRemove = true,
                draggable = false,

                drawHeaderCallback = innerRect =>
                {
                    EditorGUI.LabelField(innerRect, "Tweens");
                },

                drawElementCallback = (innerRect, innerIndex, innerA, innerH) =>
                {
                    // Get element of inner list
                    SerializedProperty tweenProperty = tweensProperty.GetArrayElementAtIndex(innerIndex);

                    //SerializedProperty tweenProperty = tweenProperty.FindPropertyRelative("name");

                    EditorGUI.PropertyField(innerRect, tweenProperty);
                }
            };
            tweensListDictionary[tweenListKey] = tweensReorderableList;
        }

        var height = (tweensProperty.arraySize + 3) * EditorGUIUtility.singleLineHeight;
        tweensReorderableList.DoList(new Rect(rect.x, rect.y, rect.width, height));

        }
        */


        #region DataBase methods

        public StatesGroup Add()
        {
            StatesGroup newStatesGroup = EditorDataBase.SaveAsset<StatesGroup>();
            EditorDataBase.instance.Add(newStatesGroup);

            return newStatesGroup;
        }

        public void Remove(StatesGroup statesGroup)
        {
            EditorDataBase.instance.Remove(statesGroup, false);
            EditorDataBase.RemoveAsset(statesGroup);
        }

        #endregion
    }


    #region supporting classes

    public class StateEditorValues
    {
        static private Dictionary<int, StateEditorValues> statesDictionary = new Dictionary<int, StateEditorValues>();

        public State state;
        public AnimBool foldout;
        public ReorderableList tweensReorderableList;

        public static UnityAction updatingAction;

        public StateEditorValues(State state)
        {
            foldout = new AnimBool(false);
            this.state = state;

            statesDictionary.Add(state.id, this);

            if (updatingAction != null) foldout.valueChanged.AddListener(updatingAction);

            // TODO

            //tweensReorderableList
        }

        public static StateEditorValues Get(State state)
        {
            StateEditorValues editorValues;

            if (statesDictionary.ContainsKey(state.id))
                editorValues = statesDictionary[state.id];
            else
                editorValues = new StateEditorValues(state);

            return editorValues;
        }

        public static void Clear()
        {
            statesDictionary.Clear();
        }

        public static void Reorder(List<State> reorderedStates)
        {
            foreach (KeyValuePair<int, StateEditorValues> entry in statesDictionary)
                for (int i = 0; i < reorderedStates.Count; i++)
                    if (entry.Key.Equals(reorderedStates[i].id))
                        statesDictionary[entry.Key].state = reorderedStates[i];
        }
    }

    #endregion

}



/*
namespace Obel.MSS.Editor
{

//[CustomEditor(typeof(StatesBehaviour))]

     

public class InspectorStatesBehaviour : UnityEditor.Editor
{
#region Properties

private StatesBehaviour statesBehaviour;
private ReorderableList statesReorderableList;
private Dictionary<string, ReorderableList> tweensListDictionary = new Dictionary<string, ReorderableList>();

#endregion

#region Unity methods

private void OnEnable()
{
statesBehaviour = (StatesBehaviour)target;

StateEditorValues.updatingAction = Repaint;

SerializedProperty statesProperty = serializedObject.FindProperty("states");

statesReorderableList = new ReorderableList(serializedObject, statesProperty)
{
    displayAdd = false,
    displayRemove = false,
    draggable = true,

    headerHeight = 0,
    footerHeight = 0,

    showDefaultBackground = false,

    drawElementBackgroundCallback = DrawStateBackground,
    drawElementCallback = DrawState,
    elementHeightCallback = GetStateHeight,
    onReorderCallback = OnReordered
};
}

private void OnDisable()
{
StateEditorValues.updatingAction = null;
StateEditorValues.Clear();
}

#endregion

#region Inspector

public override void OnInspectorGUI()
{
serializedObject.Update();

EditorGUI.BeginDisabledGroup(!statesBehaviour.enabled);
GUILayout.Space(6);

statesReorderableList.DoLayoutList();

GUILayout.Space(3);

if (GUILayout.Button("Add Stete"))
{
    UserActions.Add(() =>
    {

        State state = statesBehaviour.Add("new state");
        StateEditorValues.Reorder(statesBehaviour.states);
        StateEditorValues.Get(state).foldout.target = true;
    },
    statesBehaviour, "[MSS] Add State");
}

EditorGUI.EndDisabledGroup();

Event guiEvent = Event.current;
if (guiEvent.type == EventType.ValidateCommand && guiEvent.commandName == "UndoRedoPerformed")
{
    StateEditorValues.Reorder(statesBehaviour.states);
}

UserActions.Process();

serializedObject.ApplyModifiedProperties();
}

#endregion

#region callbacks

private void OnReordered(ReorderableList list)
{
StateEditorValues.Reorder(statesBehaviour.states);
}

private void DrawStateBackground(Rect rect, int index, bool isActive, bool isFocused)
{
EditorGUI.DrawRect(rect, Color.clear);
}

private void DrawState(Rect rect, int index, bool isActive, bool isFocused)
{
rect.width += 5;

DrawerState.editorValues = StateEditorValues.Get(statesBehaviour.states[index]);

SerializedProperty stateProperty = statesReorderableList.serializedProperty.GetArrayElementAtIndex(index);

EditorGUI.PropertyField(rect, stateProperty, true);

if (index > 1)
{
    if (GUI.Button(new Rect(rect.width + 5, rect.y + 1, 30, 20), HelperEditor.Content.iconToolbarMinus, HelperEditor.Styles.preButton))
    {
        UserActions.Add(() => statesBehaviour.states.RemoveAt(index), statesBehaviour, "[MSS] Remove State");
        StateEditorValues.Reorder(statesBehaviour.states);
    }
}

rect.x += 150;

//EditorGUI.LabelField(rect, DrawerState.editorValues.state.name + " | " + statesBehaviour.states[index].name);



//SerializedProperty tweensProperty = stateProperty.FindPropertyRelative("tweens");

/*

string tweenListKey = stateProperty.propertyPath;

ReorderableList tweensReorderableList;

if (tweensListDictionary.ContainsKey(tweenListKey))
{
    tweensReorderableList = tweensListDictionary[tweenListKey];
}
else
{
    tweensReorderableList = new ReorderableList(serializedObject, tweensProperty)
    {
        displayAdd = true,
        displayRemove = true,
        draggable = false,

        drawHeaderCallback = innerRect =>
        {
            EditorGUI.LabelField(innerRect, "Tweens");
        },

        drawElementCallback = (innerRect, innerIndex, innerA, innerH) =>
        {
            // Get element of inner list
            SerializedProperty tweenProperty = tweensProperty.GetArrayElementAtIndex(innerIndex);

            //SerializedProperty tweenProperty = tweenProperty.FindPropertyRelative("name");

            EditorGUI.PropertyField(innerRect, tweenProperty);
        }
    };
    tweensListDictionary[tweenListKey] = tweensReorderableList;
}

var height = (tweensProperty.arraySize + 3) * EditorGUIUtility.singleLineHeight;
tweensReorderableList.DoList(new Rect(rect.x, rect.y, rect.width, height));

}

private float GetStateHeight(int index)
{
int tweensCount = statesBehaviour.states[index].tweens.Count;

StateEditorValues editorValues = StateEditorValues.Get(statesBehaviour.states[index]);

return 26 + Mathf.Lerp(0, 70 + (tweensCount == 0 ? 0 : tweensCount - 1) * 21 + 40,
           editorValues.foldout.faded);
}


#endregion
}

#region supporting classes

public class StateEditorValues
{
static private Dictionary<int, StateEditorValues> statesDictionary = new Dictionary<int, StateEditorValues>();

public State state;
public AnimBool foldout;
public ReorderableList tweensReorderableList;

public static UnityAction updatingAction;

public StateEditorValues(State state)
{
foldout = new AnimBool(false);
this.state = state;

statesDictionary.Add(state.id, this);

if (updatingAction != null) foldout.valueChanged.AddListener(updatingAction);

//tweensReorderableList
}

public static StateEditorValues Get(State state)
{
StateEditorValues editorValues;

if (statesDictionary.ContainsKey(state.id))
    editorValues = statesDictionary[state.id];
else
    editorValues = new StateEditorValues(state);

return editorValues;
}

public static void Clear()
{
statesDictionary.Clear();
}

public static void Reorder(List<State> reorderedStates)
{
foreach (KeyValuePair<int, StateEditorValues> entry in statesDictionary)
    for (int i = 0; i < reorderedStates.Count; i++)
        if (entry.Key.Equals(reorderedStates[i].id))
            statesDictionary[entry.Key].state = reorderedStates[i];
}
}

#endregion
}

*/
      