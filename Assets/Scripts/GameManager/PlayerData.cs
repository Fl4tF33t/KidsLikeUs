using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public PlayerData() 
    { 
        entities = new List<EntityData>(); 
        tasks = new List<TaskData>();
    }

    //can make another class for example the settings of the player, or the player's inventory etc..
    public List<EntityData> entities;
    public List<TaskData> tasks;

    [Serializable]
    public class EntityData
    {
        public event Action OnEntityDataChanged;
        [SerializeField]
        private string uniqueID;
        [SerializeField]
        private Utils.Status status;
        [SerializeField]
        private bool firstTimeInteraction;
        [SerializeField]
        private bool hasTask;

        public EntityData(EntitySO entitySO)
        {
            uniqueID = entitySO.entityName +entitySO.uniqueID;
            status = Utils.Status.Available;
            firstTimeInteraction = true;
            hasTask = entitySO.hasTask;
        }

        public string UniqueID
        {
            get => uniqueID;
            set
            {
                if (uniqueID != value)
                {
                    uniqueID = value;
                    OnEntityDataChanged?.Invoke();
                }
            }
        }
        public Utils.Status Status
        {
            get => status;
            set
            {
                if (status != value)
                {
                    status = value;
                    OnEntityDataChanged?.Invoke();
                }
            }
        }
        public bool FirstTimeInteraction
        {
            get => firstTimeInteraction;
            set
            {
                if (firstTimeInteraction != value)
                {
                    firstTimeInteraction = value;
                    OnEntityDataChanged?.Invoke();
                }
            }
        }
        public bool HasTask
        {
            get => hasTask;
            set
            {
                if (hasTask != value)
                {
                    hasTask = value;
                    OnEntityDataChanged?.Invoke();
                }
            }
        }
    }

    [Serializable]
    public class TaskData
    {
        public event Action OnTaskDataChanged;
        [SerializeField]
        private string uniqueID;
        [SerializeField]
        private Utils.Status status;


        public TaskData(TaskSO taskSO)
        {
            uniqueID = taskSO.name;
            status = Utils.Status.Available;
        }

        public string UniqueID
        {
            get => uniqueID;
            set
            {
                if (uniqueID != value)
                {
                    uniqueID = value;
                    OnTaskDataChanged?.Invoke();
                }
            }
        }
        public Utils.Status Status
        {
            get => status;
            set
            {
                if (status != value)
                {
                    status = value;
                    OnTaskDataChanged?.Invoke();
                }
            }
        }
    }

}