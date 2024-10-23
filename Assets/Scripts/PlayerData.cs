using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public PlayerData() { entities = new List<EntityData>(); }
    public List<EntityData> entities;

    [Serializable]
    public class EntityData
    {
        public event Action OnEntityDataChanged;
        [SerializeField]
        private string uniqueID;
        [SerializeField]
        private Entity.Status status;
        [SerializeField]
        private bool firstTimeInteraction;

        public EntityData(string uniqueID) 
        { 
            this.uniqueID = uniqueID;
            status = Entity.Status.None;
            firstTimeInteraction = true;
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
        public Entity.Status Status 
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
    }
}