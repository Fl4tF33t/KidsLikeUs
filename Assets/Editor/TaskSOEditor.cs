#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TaskSO), true)]
public class TaskSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get a reference to the TaskSO object
        TaskSO taskSO = (TaskSO)target;

        // Start checking for changes to the serialized object
        serializedObject.Update();

        // Draw taskName, entitySO, taskID, and prerequisite fields
        EditorGUILayout.LabelField("Task", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taskName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taskDescription"));
        EditorGUILayout.Space(15);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("holder"));
        EditorGUI.indentLevel++;
        switch (taskSO.holder)
        {
            case TaskSO.TaskHolders.Spirit:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("spiritType"), true);
                break;
            case TaskSO.TaskHolders.Other:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("otherType"), true);
                break;
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taskID"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("hasPrerequisite"), true);
        if (taskSO.hasPrerequisite)
        {
            EditorGUI.indentLevel += 2;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("prerequisites"), true);
            EditorGUI.indentLevel -= 2;
        }

        EditorGUILayout.Space(25);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif