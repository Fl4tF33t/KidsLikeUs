using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "ScriptableObjects/Spirit", order = 1)]
public class SpiritSO : EntitySO
{
    public enum Type{ Snow, Water, Fire, Earth}

    public Type type;

    public TaskSO[] tasks;
}
