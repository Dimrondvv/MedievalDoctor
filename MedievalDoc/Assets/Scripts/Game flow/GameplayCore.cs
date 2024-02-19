using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayCore
{
    public UnityEvent<GameManager> OnGameManagerRegistered = new UnityEvent<GameManager>();
    public UnityEvent OnPlayPressed = new UnityEvent();

    public GameManager GameManager { get; private set; }

    public void Initialize()
    {

    }
    public void UnInitialize()
    {

    }

    public void RegisterGameManager(GameManager gameManager)
    {
        GameManager = gameManager;
        Debug.Log("Registered");
        OnGameManagerRegistered.Invoke(gameManager);
    }
    public void UnregisterGameManager()
    {
        GameManager = null;
    }

    public void PressPlay()
    {
        OnPlayPressed.Invoke();
    }
}
