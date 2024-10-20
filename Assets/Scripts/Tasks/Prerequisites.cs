using System;
using System.Diagnostics;

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
        
        else if (entity != null && taskSO == null)
        {
            foreach (Entity e in entity)
            {
                if (e.EntityStatus != Entity.Status.Completed)
                {
                    return false;
                }
            }
            return true;
        }

        // else if (taskSO != null && entity == null)
        //     return taskSO.taskState == TaskSO.State.Completed;

        // else 
        return false;
    }
}
