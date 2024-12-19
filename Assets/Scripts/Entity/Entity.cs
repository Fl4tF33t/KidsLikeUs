using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Dialogue))]
public class Entity : MonoBehaviour, ISaveable
{
    #region SaveLoad
    private SaveLoad saveLoad;
    [SerializeField]
    public PlayerData.EntityData entityData;
    #endregion
    public EntitySO entitySO;

    #region Interactions
    public event Action<string> OnStartDialogue;
    public InputActionReference interactInputActionReference;
    #endregion

    private void OnValidate()
    {
        if (entitySO == null)
        {
            Debug.LogError("Ensure there is a EntitySO assigned");
            return;
        }
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

        entityData = new PlayerData.EntityData(entitySO);
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

    protected void TriggerDialogueEvent(string arg) => OnStartDialogue?.Invoke(arg);

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
            entityData.Status = Utils.Status.InProgress;
            entityData.FirstTimeInteraction = false;
            FirstTimeInteraction();
            return;
        }
        Debug.Log("Prerequisite met");
    }

    protected virtual void UnavailableInteraction()
    {
        if(entitySO.hasPrerequisite)
            OnStartDialogue?.Invoke(entitySO.unavailableDialogue);
    }

    protected virtual void FirstTimeInteraction()
    {
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
