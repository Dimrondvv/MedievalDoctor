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
        Debug.Log($"SAVED FILE TO {Application.persistentDataPath + "/" + fileName}");
    }

    public T LoadGameData<T>(string fileName)
    {
        try
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            string json = File.ReadAllText(Application.persistentDataPath + "/" + fileName);
            Debug.Log($"LOADED FILE FROM {Application.persistentDataPath + "/" + fileName}");
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
        catch
        {
            return default(T);
        }
    }
}
