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

        if (entitySO != null && taskSO == null)
            return CheckEntity();

        if (entitySO == null && taskSO != null)
            return CheckTask();

        return CheckEntity() && CheckTask();
    }

    private bool CheckEntity()
    {
        foreach (EntitySO e in entitySO)
        {
            if (GameManager.Instance.saveLoad.HasEntity(e.entityName + e.uniqueID))
            {
                if (GameManager.Instance.saveLoad.GetEntityData(e.entityName + e.uniqueID).Status != Utils.Status.Completed)
                    return false;
            }
        }
        return true;
    }
    private bool CheckTask()
    {
        return true;
    }
}
