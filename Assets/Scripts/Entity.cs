using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Entity : MonoBehaviour, ISaveable
{
    #region SaveLoad
    private SaveLoad saveLoad;// = GameManager.Instance.saveLoad;
    private string uniqueID;
    public string UniqueID 
    { 
        get { return uniqueID; } 
    }
    #endregion
    public event Action<string> OnStartDialogue;
    public EntitySO entitySO;

    public InputActionReference interactInputActionReference;

    public enum Status { None, InProgress, Completed}
    [SerializeField]
    private Status entityStatus = Status.None;
    public Status EntityStatus 
    { 
        get { return entityStatus; } 
        protected set 
        { 
            if (entityStatus == value) return;

            entityStatus = value; 
            SaveData();
        }
    }
    private bool firstTimeInteraction = true;

    private void OnValidate()
    {
        this.name = entitySO.entityName;
    }
    private void Awake()
    {
        InitializeEntity();
        StartCoroutine(AfterInteractionEvents());
    }
    private IEnumerator AfterInteractionEvents()
    {
        yield return new WaitForSeconds(3f);
        Interact();
    }

    //right now the entity subscribes and unsubscribes during enableing
    //can change this to occur whenever, for example when the player enters a region or starts a section
    private void OnEnable()
    {
        interactInputActionReference.action.performed += Interact;
    }
    private void OnDisable()
    {
        interactInputActionReference.action.performed -= Interact;
    }

    protected virtual void InitializeEntity()
    {
        if (entitySO == null)
        {
            Debug.LogError("EntitySO is null");
            return;
        }

        saveLoad = GameManager.Instance.saveLoad;
        uniqueID = entitySO.entityName + entitySO.uniqueID;
        saveLoad.AddSaveable(this);
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
        if (firstTimeInteraction)
        {
            EntityStatus = Status.InProgress;
            firstTimeInteraction = false;
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
        if (saveLoad.HasEntity(uniqueID))
            saveLoad.GetEntityData(uniqueID).status = EntityStatus;
    }

    public virtual void LoadData()
    {
        if (saveLoad.HasEntity(uniqueID))
            EntityStatus = saveLoad.GetEntityData(uniqueID).status;
    }
}
