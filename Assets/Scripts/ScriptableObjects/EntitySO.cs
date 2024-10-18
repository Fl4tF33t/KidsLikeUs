using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EntitySO : ScriptableObject
{
    public string entityName;

    [TextArea(3, 7)]
    public string description;
    [TextArea(3, 7)]
    public string introDialogue;

    public virtual void FirstTimeInteraction()
    {
        Debug.Log("My name is " + entityName);
    }

    public virtual void ValidateEntityData()
    {
        name = entityName + " SO";

        // Update the asset name in the project window
        #if UNITY_EDITOR
        string assetPath = AssetDatabase.GetAssetPath(this);
        if (!string.IsNullOrEmpty(assetPath))
        {
            AssetDatabase.RenameAsset(assetPath, name);  // Renames the asset file
            AssetDatabase.SaveAssets();  // Saves the changes
        }
        #endif
    }
}
