using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public void SaveGameData<T>(T data)
    {
        
    }

    public string RequestGameData(int key)
    {
        return gameData[key];
    }
}
