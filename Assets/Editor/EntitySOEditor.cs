#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EntitySO), true)]
public class EntitySOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        // Add a button below the default inspector
        EntitySO entitySO = (EntitySO)target;
        if (GUILayout.Button("Validate Entity Data"))
        {
            // Call the PrintEntityName method when the button is pressed
            entitySO.ValidateEntityData();
        }
    }
}
#endif