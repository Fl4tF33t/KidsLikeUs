using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class Spirit : Entity
{
    private Task task;
    // private SpiritSO spiritSO;
    // protected override void InitializeEntity()
    // {
    //     base.InitializeEntity();
    //     if (entitySO is SpiritSO spiritSO)
    //     {
    //         this.spiritSO = spiritSO;
    //     } 
        
    // }

    private void Start()
    {
        if(entityData.HasTask)
        {
            task = GetComponent<Task>();
        }
        
        InvokeRepeating(nameof(Interact), 3, 5);
    }
    

    public override void Interact(InputAction.CallbackContext context = default)
    {
        base.Interact(context);
        if(entityData.HasTask)
        {
            TaskSO nextTask = task.SelectNextTask();
            TriggerDialogueEvent(nextTask.taskDescription);
        }
    }

    // public override void SaveData()
    // {
    //     GameManager.Instance.jsonSaving.playerData.spirits.Find(spirit => spirit.spiritType == spiritSO.type).status = EntityStatus;
    //     base.SaveData();
    // }

    // public override void LoadData()
    // {
    //     EntityStatus = GameManager.Instance.jsonSaving.playerData.spirits.Find(spirit => spirit.spiritType == spiritSO.type).status;
    // }
}
