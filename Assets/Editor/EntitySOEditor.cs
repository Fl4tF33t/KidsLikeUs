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
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("entityName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("entityDescription"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("introDialogue"));

        EditorGUILayout.Space(15);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("prerequisite"));
        if (entitySO.prerequisite)
        {
            EditorGUI.indentLevel += 2;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("prerequisites"), true);
            EditorGUI.indentLevel -= 2;
        }

        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Validate Entity Data"))
        {
            // Call the PrintEntityName method when the button is pressed
            entitySO.ValidateEntityData();
        }
    }
}
#endif