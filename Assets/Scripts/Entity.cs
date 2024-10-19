using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Entity : MonoBehaviour
{
    public event Action<string> OnStartDialogue;
    public EntitySO entitySO;

    public InputActionReference interactInputActionReference;

    public enum Status { None, InProgress, Completed}
    [SerializeField]
    private Status entityStatus = Status.None;
    public Status EntityStatus 
    { 
        get { return entityStatus; } 
        protected set { entityStatus = value; }
    }
    private bool firstTimeInteraction = true;

    private void Awake()
    {
        InitializeEntity();
    }

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

}
