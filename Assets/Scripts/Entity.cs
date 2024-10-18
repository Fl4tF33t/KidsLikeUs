using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntitySO entitySO;
    public enum EntityStatus { None, InProgress, Completed}
    [SerializeField]
    private EntityStatus entityStatus = EntityStatus.None;
    private bool firstTimeInteraction = true;

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
                    UnavailableInteraction();
                    return;
                }
            }
        }
        if (firstTimeInteraction)
        {
            entityStatus = EntityStatus.InProgress;
            firstTimeInteraction = false;
            FirstTimeInteraction();
        }
    }

    protected virtual void UnavailableInteraction()
    {
        //what does this npc do if the prereq are not met, maybe inform them what they need to do
        //or just say that this part is not available yet,  ask help from mthe guide
    }

    protected virtual void FirstTimeInteraction()
    {
        //Do something for the first time, intro and tell about itself
    }

    public EntityStatus GetEntityStatus()
    {
        return entityStatus;
    }
}
