using System;

[Serializable]
public class Prerequisites
{
    public EntitySO[] entitySO;
    public TaskSO taskSO;

    public bool CheckPrerequisite()
    {
        if (entitySO == null && taskSO == null)
           return true;

        // else if (entity != null && taskSO != null)
        // {
        //     if (entity.EntityStatus == Entity.Status.Completed && taskSO.taskState == TaskSO.State.Completed)
        //         return true;
            
        //     else return false;
        // }
        
        if (entitySO != null)
        {
            foreach (EntitySO e in entitySO)
            {
                if (GameManager.Instance.saveLoad.HasEntity(e.entityName + e.uniqueID))
                {
                    if(GameManager.Instance.saveLoad.GetEntityData(e.entityName + e.uniqueID).Status != Entity.Status.Completed)
                        return false;
                }
            }
            return true;
        }

        return false;
    }
}
