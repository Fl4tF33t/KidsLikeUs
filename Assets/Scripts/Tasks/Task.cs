using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Task : MonoBehaviour, ISaveable
{
    private SaveLoad saveLoad;
    private Entity entityBrain;
    public List<PlayerData.TaskData> taskData;
    private void Awake()
    {
        entityBrain = GetComponent<Entity>();
        saveLoad = GameManager.Instance.saveLoad;
        InitializeTasks();
    }

    private void InitializeTasks()
    {
        if (entityBrain == null)
        {
            Debug.LogError("EntityBrain is null");
            return;
        }

        if (!entityBrain.entitySO.hasTask)
        {
            Debug.LogError("Entity does not have a task");
            Destroy(this);
            return;
        }

        foreach (TaskSO task in entityBrain.entitySO.tasks)
        {
            taskData.Add(new PlayerData.TaskData(task));
        }

        saveLoad.AddSaveable(this);
    }

    private void OnEnable()
    {
        foreach (PlayerData.TaskData task in taskData)
        {
            task.OnTaskDataChanged += SaveData;
        }
    }

    private void OnDisable()
    {
        foreach (PlayerData.TaskData task in taskData)
        {
            task.OnTaskDataChanged -= SaveData;
        }
    }

    public TaskSO SelectNextTask()
    {
        // Find the task that is in progress
        PlayerData.TaskData inProgressTask = taskData.FirstOrDefault(task => task.Status == Utils.Status.InProgress);

        if (inProgressTask != null)
        {
            return entityBrain.entitySO.tasks.FirstOrDefault(task => task.taskName == inProgressTask.UniqueID);
        }

        int availableTaskIndex = taskData.FindIndex(task => task.Status == Utils.Status.Available);

        return availableTaskIndex != -1 ? entityBrain.entitySO.tasks[availableTaskIndex] : null;
    }

    public void SaveData()
    {
        foreach (PlayerData.TaskData task in taskData)
        {
            if (saveLoad.HasTask(task.UniqueID))
                saveLoad.GetTaskData(task.UniqueID).Status = task.Status;
        }
    }

    public void LoadData()
    {
        for (int i = 0; i < taskData.Count; i++)
        {
            if (saveLoad.HasTask(taskData[i].UniqueID))
                taskData[i] = saveLoad.GetTaskData(taskData[i].UniqueID);
        }
    }
}
