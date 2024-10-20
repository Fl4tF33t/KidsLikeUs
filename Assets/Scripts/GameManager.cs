using UnityEngine;
using System.Collections.Generic;

[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(JSONSaving))]
public class GameManager : Singleton<GameManager>
{
    public JSONSaving jsonSaving;
    private List<ISaveable> saveables = new List<ISaveable>();

    public List<ISaveable> Saveables
    {
        get { return saveables; }
        set 
        { 
            saveables = value; 
            SaveData();
        }
    }
    protected override void Awake()
    {
        base.Awake();
        jsonSaving = GetComponent<JSONSaving>();
    }

    private void Start()
    {
        if (saveables.Count > 0)
        {
            foreach (ISaveable saveable in saveables)    
            {
                saveable.LoadData();
            }
        }
    }
    public void SaveData() {jsonSaving.SaveData();}
}
