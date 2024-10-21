using System;
using System.Collections.Generic;


[Serializable]
public class PlayerData
{
    public PlayerData() { entities = new List<EntityData>(); }
    public List<EntityData> entities;

    [Serializable]
    public class EntityData
    {
        public string uniqueID;
        public Entity.Status status;
    }
}