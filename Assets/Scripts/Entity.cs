using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntitySO entitySO;
    public enum EntityStatus { None, InProgress, Completed, Failed }
    public EntityStatus entityStatus = EntityStatus.None;

    private bool firstTimeInteraction;

    private void Awake()
    {
        InitializeEntity();
    }

    protected virtual void InitializeEntity()
    {
        if (entitySO == null)
        {
            Debug.LogError("EntitySO is null");
            return;
        }
    }

    public virtual void Interact()
    {
        if (entitySO.prerequisite)
        {
            foreach (Prerequisites prerequisite in entitySO.prerequisites)
            {
                if (!prerequisite.CheckPrerequisite())
                {
                    Debug.Log("Prerequisite not met");
                    return;
                }
            }
        }
        if (firstTimeInteraction)
        {
            entityStatus = EntityStatus.InProgress;
            firstTimeInteraction = false;
            entitySO.FirstTimeInteraction();
        }
    }

}
