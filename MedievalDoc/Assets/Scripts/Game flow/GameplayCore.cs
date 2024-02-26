using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayCore
{
    public UnityEvent<GameManager> OnGameManagerRegistered = new UnityEvent<GameManager>();
    public UnityEvent<LoadManager> OnLoadManagerRegistered = new UnityEvent<LoadManager>();
    public UnityEvent OnPlayPressed = new UnityEvent();

    public GameManager GameManager { get; private set; }
    public LoadManager LoadManager { get; private set; }

    public void Initialize()
    {

    }
    public void UnInitialize()
    {

    }

    public void RegisterGameManager(GameManager gameManager)
    {
        GameManager = gameManager;
        Debug.Log("GameManager Registered");
        OnGameManagerRegistered.Invoke(gameManager);
    }
    public void RegisterLoadManager(LoadManager loadManager)
    {
        LoadManager = loadManager;
        Debug.Log("LoadManager Registered");
        OnLoadManagerRegistered.Invoke(loadManager);
    }
    public void UnregisterGameManager()
    {
        GameManager = null;
    }
    public void UnregisterLoadManager()
    {
        LoadManager = null;
    }

    public void PressPlay()
    {
        OnPlayPressed.Invoke();
    }
}
