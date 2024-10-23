using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Entity : MonoBehaviour, ISaveable
{
    #region SaveLoad
    private SaveLoad saveLoad;
    [SerializeField]
    public PlayerData.EntityData entityData;
    #endregion
    public EntitySO entitySO;

    public enum Status { None, InProgress, Completed }
    #region Interactions
    public event Action<string> OnStartDialogue;
    public InputActionReference interactInputActionReference;
    #endregion

    private void OnValidate()
    {
        this.name = entitySO.entityName;
    }
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

        entityData = new PlayerData.EntityData(entitySO.entityName + entitySO.uniqueID);
        saveLoad = GameManager.Instance.saveLoad;
        saveLoad.AddSaveable(this);
    }

    //right now the entity subscribes and unsubscribes during enableing
    //can change this to occur whenever, for example when the player enters a region or starts a section
    private void OnEnable()
    {
        entityData.OnEntityDataChanged += SaveData;
        interactInputActionReference.action.performed += Interact;
    }
    private void OnDisable()
    {
        entityData.OnEntityDataChanged -= SaveData;
        interactInputActionReference.action.performed -= Interact;
    }
    public virtual void Interact(InputAction.CallbackContext context = default)
    {
        if (entitySO.hasPrerequisite)
        {
            if (!entitySO.prerequisites.CheckPrerequisite())
            {
                Debug.Log("Prerequisite not met");
                UnavailableInteraction();
                return;
            }
        }
        if (entityData.FirstTimeInteraction)
        {
            entityData.Status = Status.InProgress;
            entityData.FirstTimeInteraction = false;
            FirstTimeInteraction();
        }
        Debug.Log("Prerequisite met");
    }

    protected virtual void UnavailableInteraction()
    {
        //what does this npc do if the prereq are not met, maybe inform them what they need to do
        //or just say that this part is not available yet,  ask help from mthe guide
    }

    protected virtual void FirstTimeInteraction()
    {
        //Do something for the first time, intro and tell about itself
        OnStartDialogue?.Invoke(entitySO.introDialogue);
    }


    public virtual void SaveData()
    {
        if (saveLoad.HasEntity(entityData.UniqueID))
        {
            saveLoad.GetEntityData(entityData.UniqueID).Status = entityData.Status;
            saveLoad.GetEntityData(entityData.UniqueID).FirstTimeInteraction = entityData.FirstTimeInteraction;
        }
    }

    public virtual void LoadData()
    {
        if (saveLoad.HasEntity(entityData.UniqueID))
            entityData = saveLoad.GetEntityData(entityData.UniqueID);
    }
}
