using UnityEngine.InputSystem;
public class Spirit : Entity
{
    protected override void InitializeEntity()
    {
        base.InitializeEntity();
        if (entitySO is SpiritSO spiritSO)
        {

        } 
    }

    public override void Interact(InputAction.CallbackContext context)
    {
        base.Interact(context);
    }
}
