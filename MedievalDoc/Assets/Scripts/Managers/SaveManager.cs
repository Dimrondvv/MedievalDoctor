using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
public class SaveManager : MonoBehaviour
{
    private Dictionary<int, string> gameData; //int - key || string - json
    private void Awake()
    {
        App.Instance.GameplayCore.RegisterSaveManager(this);
    }
    private void OnDestroy()
    {
        App.Instance.GameplayCore.UnregisterSaveManager();
    }

    public void SaveGameData<T>(T data, string fileName)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(Application.persistentDataPath + "/" + fileName, json);
    }

    public T LoadGameData<T>(string fileName)
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/" + fileName);
        return JsonConvert.DeserializeObject<T>(json);
    }
}
