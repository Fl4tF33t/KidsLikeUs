using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "ScriptableObjects/Guide", order = 0)]
public class GuideSO : EntitySO
{
    public enum Type{ Wolf, Player}

    public Type type;
}
