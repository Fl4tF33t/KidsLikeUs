#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EntitySO), true)]
public class EntitySOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Add a button below the default inspector
        EntitySO entitySO = (EntitySO)target;

        // Start checking for changes to the serialized object
        serializedObject.Update();

        // Draw taskName, entitySO, taskID, and prerequisite fields
        EditorGUILayout.LabelField("Entity", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("entityName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("uniqueID"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("entityDescription"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("introDialogue"));
        EditorGUILayout.Space(15);
        EditorGUI.indentLevel--;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("hasPrerequisite"));

        if (entitySO.hasPrerequisite)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("unavailableDialogue"), true);
            EditorGUI.indentLevel += 2;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("prerequisites"), true);
            EditorGUI.indentLevel -= 3;
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("hasTask"));
        if (entitySO.hasTask)
        {
            EditorGUI.indentLevel += 2;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tasks"), true);
            EditorGUI.indentLevel -= 2;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif