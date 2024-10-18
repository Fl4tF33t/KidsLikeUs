using System;

[Serializable]
public class Prerequisites
{
    public EntitySO entitySO;
    public TaskSO taskSO;

    public bool CheckPrerequisite()
    {
        if (entitySO == null && taskSO == null)
           return true;
        
        //else if (entitySO != null){}
        else if (taskSO != null)
            return taskSO.taskState == TaskSO.State.Completed;

        else return false;
    }
}
