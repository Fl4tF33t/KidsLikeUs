#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpiritSO), true)]
public class SpiritSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Add a button below the default inspector
        SpiritSO spiritSO = (SpiritSO)target;

        // // Start checking for changes to the serialized object
        // serializedObject.Update();

        // GUILayout.Space(10);
        // EditorGUILayout.PropertyField(serializedObject.FindProperty("type"));

        // serializedObject.ApplyModifiedProperties();
    }
}
#endif