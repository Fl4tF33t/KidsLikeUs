using System.IO;
using UnityEngine;

[DefaultExecutionOrder(-90)]
public class JSONSaving : MonoBehaviour
{
    public PlayerData playerData;

    private string persistentPath;

    private void Awake()
    {
        //create the path
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "PlayerData.json";

        if (File.Exists(persistentPath))
        {
            LoadData();
        }
        else
        {
            playerData = new PlayerData();
            SaveData();
        }
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(persistentPath, json);
    }

    private void LoadData()
    {
        string json = File.ReadAllText(persistentPath);
        playerData = JsonUtility.FromJson<PlayerData>(json);
    }

    public void ResetData()
    {
        playerData = new PlayerData();
        SaveData();
    }
}