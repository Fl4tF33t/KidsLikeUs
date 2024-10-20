using System;
using System.Collections.Generic;
using JetBrains.Annotations;


[Serializable]
public class PlayerData
{
    public List<Spirit> spirits; 
    public PlayerData()
    {
        spirits = new List<Spirit>();
        foreach(SpiritSO.Type spiritType in Enum.GetValues(typeof(SpiritSO.Type)))
        {
            Spirit spirit = new Spirit();
            spirit.spiritType = spiritType;
            spirit.status = Entity.Status.None;
            spirits.Add(spirit);
        }
    }
    [Serializable]
    public class Spirit
    {
        public SpiritSO.Type spiritType;
        public Entity.Status status;
    }
}