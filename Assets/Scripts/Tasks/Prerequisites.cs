using System;

[Serializable]
public class Prerequisites
{
    public Entity[] entity;
    public TaskSO taskSO;

    public bool CheckPrerequisite()
    {
        if (entity == null && taskSO == null)
           return true;

        // else if (entity != null && taskSO != null)
        // {
        //     if (entity.EntityStatus == Entity.Status.Completed && taskSO.taskState == TaskSO.State.Completed)
        //         return true;
            
        //     else return false;
        // }
        
        // else if (entity != null && taskSO == null)
        //     return entity.EntityStatus == Entity.Status.Completed;

        // else if (taskSO != null && entity == null)
        //     return taskSO.taskState == TaskSO.State.Completed;

        // else 
        return false;
    }
}
