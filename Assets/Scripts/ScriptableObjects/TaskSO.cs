using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "", menuName = "ScriptableObjects/Task", order = 2)]
public class TaskSO : ScriptableObject
{    
    public string taskName;
    [TextArea(3, 10)]
    public string taskDescription;
    public enum TaskHolders
    {
        Spirit,
        Other
    }
    
    public TaskHolders holder;
    public SpiritSO.Type spiritType;
    public enum OtherType
    {
        None,
        Player,
        Wolf
    }
    [SerializeField]
    private OtherType otherType;

    [Min(1)]
    public int taskID;
    public bool hasPrerequisite;
    public Prerequisites[] prerequisites;

    
    private void OnValidate()
    {
        string empty = "";
        switch (holder)
        {
            case TaskHolders.Spirit:
                empty = spiritType.ToString() + " Task - " + taskID;
                break;
            case TaskHolders.Other:
                empty = otherType.ToString() + " Task - " + taskID;
                break;
        }

        if (name == empty)
            return;

        name = empty;

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