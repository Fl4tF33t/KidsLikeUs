using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    private List<ISaveable> saveables = new List<ISaveable>();

    public void AddSaveable(ISaveable saveable)
    {
        if (!saveables.Contains(saveable))
            saveables.Add(saveable);
    }

    private void Start()
    {
        foreach (ISaveable saveable in saveables)
        {
            if (saveable is Entity entity)
            {
                if (!GameManager.Instance.jsonSaving.playerData.entities.Any(e => e.uniqueID == entity.name))
                {
                    GameManager.Instance.jsonSaving.playerData.entities.Add(new PlayerData.EntityData() { uniqueID = entity.name, status = entity.EntityStatus }); 
                }
            }
      
            saveable.LoadData();
        }
    }

    public bool HasEntity(string uniqueID)
    {
        return GameManager.Instance.jsonSaving.playerData.entities.Any(e => e.uniqueID == uniqueID);
    }
    public PlayerData.EntityData GetEntityData(string uniqueID)
    {
        if (HasEntity(uniqueID))
            return GameManager.Instance.jsonSaving.playerData.entities.Find(e => e.uniqueID == uniqueID);

        return null;
    }

    public void SaveData<T>(T saveable, string uniqueID = null) 
    {
        if (uniqueID != null)
        {
            
        }   
    }

    private void OnApplicationQuit()
    {
        GameManager.Instance.jsonSaving.SaveData();
    }
}
