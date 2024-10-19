using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    private Entity entityBrain;
    private List<TaskSO> tasks;
    void Start()
    {
        entityBrain = GetComponent<Entity>();
        InitializeTasks();
    }

    private void InitializeTasks()
    {
        if (entityBrain == null)
        {
            Debug.LogError("EntityBrain is null");
            return;
        }

        if(entityBrain.entitySO.hasPrerequisite)
        {
            
        }
    }
}
