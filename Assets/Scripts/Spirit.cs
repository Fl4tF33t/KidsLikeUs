using UnityEngine;
using UnityEngine.InputSystem;
public class Spirit : Entity
{
    private SpiritSO spiritSO;
    protected override void InitializeEntity()
    {
        base.InitializeEntity();
        if (entitySO is SpiritSO spiritSO)
        {
            this.spiritSO = spiritSO;
        } 
        
    }

    public override void Interact(InputAction.CallbackContext context)
    {
        base.Interact(context);
    }

    public override void SaveData()
    {
        GameManager.Instance.jsonSaving.playerData.spirits.Find(spirit => spirit.spiritType == spiritSO.type).status = EntityStatus;
        base.SaveData();
    }

    public override void LoadData()
    {
        EntityStatus = GameManager.Instance.jsonSaving.playerData.spirits.Find(spirit => spirit.spiritType == spiritSO.type).status;
    }
}
