using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(JSONSaving))]
[RequireComponent(typeof(SaveLoad))]
public class GameManager : Singleton<GameManager>
{
    public JSONSaving jsonSaving;
    public SaveLoad saveLoad;

    protected override void Awake()
    {
        base.Awake();
        if(jsonSaving == null)
            jsonSaving = GetComponent<JSONSaving>();
        
        if(saveLoad == null)
            saveLoad = GetComponent<SaveLoad>();
    }
    
}
