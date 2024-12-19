using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-80)]
public class SaveLoad : MonoBehaviour
{
    private List<ISaveable> saveables = new List<ISaveable>();

    public void AddSaveable(ISaveable saveable)
    {
        if (!saveables.Contains(saveable))
        {
            saveables.Add(saveable);
            InitializeEntity(saveable);
        }
    }

    private void InitializeEntity(ISaveable saveable)
    {
        if (saveable is Entity entity)
        {
            if (!HasEntity(entity.entityData.UniqueID))
                GameManager.Instance.jsonSaving.playerData.entities.Add(entity.entityData);
            
            else
                entity.LoadData();
        }
        if (saveable is Task task)
        {
            foreach(PlayerData.TaskData taskData in task.taskData)
            {
                if(!HasTask(taskData.UniqueID))
                    GameManager.Instance.jsonSaving.playerData.tasks.Add(taskData);
            }
            task.LoadData();
        }
    }

    public bool HasEntity(string uniqueID)
    {
        return GameManager.Instance.jsonSaving.playerData.entities.Any(e => e.UniqueID == uniqueID);
    }

    public bool HasTask(string uniqueID)
    {
        return GameManager.Instance.jsonSaving.playerData.tasks.Any(e => e.UniqueID == uniqueID);
    }
    public PlayerData.EntityData GetEntityData(string uniqueID)
    {
        if (HasEntity(uniqueID))
            return GameManager.Instance.jsonSaving.playerData.entities.Find(e => e.UniqueID == uniqueID);

        Debug.LogError("EntityData not found: " + uniqueID);
        return null;
    }
    public PlayerData.TaskData GetTaskData(string uniqueID)
    {
        if (HasTask(uniqueID))
            return GameManager.Instance.jsonSaving.playerData.tasks.Find(e => e.UniqueID == uniqueID);

        Debug.LogError("EntityData not found: " + uniqueID);
        return null;
    }

    private void OnApplicationQuit()
    {
        GameManager.Instance.jsonSaving.SaveData();
    }
}
