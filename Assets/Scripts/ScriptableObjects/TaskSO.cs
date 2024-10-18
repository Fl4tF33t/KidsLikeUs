using UnityEngine;
using System;

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
        None,
        Spirit,
        Other
    }
    
    public TaskHolders holder;
    public SpiritSO.Type spiritType;
    public enum OtherType
    {
        Player,
        Wolf
    }
    [SerializeField]
    private OtherType otherType;

    [Min(1)]
    public int taskID;

    public enum State{ Wait, InProgress, Completed, Failed}
    public State taskState = State.Wait;
    public bool prerequisite;
    public Prerequisites[] prerequisites;


    public virtual void StartTask()
    {
        if (prerequisite)
        {
            foreach (Prerequisites prerequisite in prerequisites)
            {
                if (!prerequisite.CheckPrerequisite())
                {
                    Debug.Log("Prerequisite not met");
                    ChangeState(State.Failed);
                    return;
                }
            }
        }
        Debug.Log("Starting task " + taskName);
        ChangeState(State.InProgress);
    }

    public virtual void ChangeState(State newState)
    {
        taskState = newState;
    }
    
    public virtual void ValidateTaskData()
    {
        string empty = "";
        switch (holder)
        {
            case TaskHolders.None:
                break;
            case TaskHolders.Spirit:
                empty = spiritType.ToString();
                break;
            case TaskHolders.Other:
                empty = otherType.ToString();
                break;
        }
        name = empty + " Task - " + taskID;

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