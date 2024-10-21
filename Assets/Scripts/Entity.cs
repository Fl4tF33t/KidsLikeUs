using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Entity : MonoBehaviour, ISaveable
{
    SaveLoad saveLoad;
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
        this.name = entitySO.entityName + " - " + this.GetInstanceID();
    }
    private void Awake()
    {
        InitializeEntity();
        StartCoroutine(AfterInteractionEvents());
    }
    private IEnumerator AfterInteractionEvents()
    {
        yield return new WaitForSeconds(7f);
        EntityStatus = Status.InProgress;
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
        saveLoad.AddSaveable(this);
        //GameManager.Instance.saveLoad.AddSaveable(this);
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
            entityStatus = Status.InProgress;
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
        OnStartDialogue?.Invoke(entitySO.introDialogue);
    }

    public virtual void SaveData()
    {
        if (saveLoad.HasEntity(this.name))
            GameManager.Instance.jsonSaving.playerData.entities.Find(entity => entity.uniqueID == this.name).status = entityStatus;
    }

    public virtual void LoadData()
    {
        if (saveLoad.HasEntity(this.name))
            EntityStatus = saveLoad.GetEntityData(this.name).status;
    }
}
