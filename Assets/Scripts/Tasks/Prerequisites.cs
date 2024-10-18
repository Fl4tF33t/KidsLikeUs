using System;

[Serializable]
public class Prerequisites
{
    public Entity entity;
    public TaskSO taskSO;

    public bool CheckPrerequisite()
    {
        if (entity == null && taskSO == null)
           return true;

        else if (entity != null && taskSO != null)
        {
            if (entity.GetEntityStatus() == Entity.EntityStatus.Completed && taskSO.taskState == TaskSO.State.Completed)
                return true;
            
            else return false;
        }
        
        else if (entity != null && taskSO == null)
            return entity.GetEntityStatus() == Entity.EntityStatus.Completed;

        else if (taskSO != null && entity == null)
            return taskSO.taskState == TaskSO.State.Completed;

        else return false;
    }
}
