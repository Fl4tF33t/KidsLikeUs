using UnityEngine;
using UnityEngine.UIElements;

public class Entity : MonoBehaviour
{
    public EntitySO entitySO;
    [HideInInspector]
    public TaskSO currentTask;
    public enum TaskStatus { None, InProgress, Completed, Failed }
    [HideInInspector]
    public TaskStatus taskStatus = TaskStatus.None;

    private void Awake()
    {
        InitializeEntity();
    }

    private void InitializeEntity()
    {
        if (entitySO == null)
        {
            Debug.LogError("EntitySO is null");
            return;
        }

        if (entitySO is SpiritSO spiritSO)
        {
            if (spiritSO.tasks != null && spiritSO.tasks.Length > 0 && currentTask == null)
            {
                currentTask = spiritSO.tasks[0];
            }
        }
    }

    void Start()
    {
        entitySO.FirstTimeInteraction();
        currentTask.StartTask();
    }
}
