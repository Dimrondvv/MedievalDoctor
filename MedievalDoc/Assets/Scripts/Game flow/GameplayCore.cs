using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayCore
{
    public UnityEvent<GameManager> OnGameManagerRegistered = new UnityEvent<GameManager>();
    public UnityEvent<LoadManager> OnLoadManagerRegistered = new UnityEvent<LoadManager>();
    public UnityEvent<SaveManager> OnSaveManagerRegistered = new UnityEvent<SaveManager>();
    public UnityEvent<PatientManager> OnPatientManagerRegistered = new UnityEvent<PatientManager>();
    public UnityEvent<PlayerManager> OnPlayerManagerRegistered = new UnityEvent<PlayerManager>();
    public UnityEvent<UIManager> OnUIManagerRegistered = new UnityEvent<UIManager>();
    public UnityEvent<TimerManager> OnTimerManagerRegistered = new UnityEvent<TimerManager>();
    public UnityEvent<UpgradeManager> OnUpgradeManagerRegistered = new UnityEvent<UpgradeManager>();
    public UnityEvent OnPlayPressed = new UnityEvent();

    public GameManager GameManager { get; private set; }
    public LoadManager LoadManager { get; private set; }
    public SaveManager SaveManager { get; private set; }
    public PatientManager PatientManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }
    public UIManager UIManager { get; private set; }
    public TimerManager TimerManager { get; private set; }
    public UpgradeManager UpgradeManager { get; private set; }

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
    public void RegisterSaveManager(SaveManager saveManager)
    {
        SaveManager = saveManager;
        Debug.Log("SaveManagerManager Registered");
        OnSaveManagerRegistered.Invoke(saveManager);
    }
    public void RegisterPatientManager(PatientManager patientManager)
    {
        PatientManager = patientManager;
        Debug.Log("PatientManager Registered");
        OnPatientManagerRegistered.Invoke(patientManager);
    }
    public void RegisterPlayerManager(PlayerManager playerManager)
    {
        PlayerManager = playerManager;
        Debug.Log("PlayerManager Registered");
        OnPlayerManagerRegistered.Invoke(playerManager);
    }
    public void RegisterUIManager(UIManager uiManager)
    {
        UIManager = uiManager;
        Debug.Log("UIManager Registered");
        OnUIManagerRegistered.Invoke(uiManager);
    }
    public void RegisterTimerManager(TimerManager timerManager)
    {
        TimerManager = timerManager;
        Debug.Log("TimerManager Registered");
        OnTimerManagerRegistered.Invoke(timerManager);
    }
    public void RegisterUpgradeManager(UpgradeManager upgradeManager)
    {
        UpgradeManager = upgradeManager;
        Debug.Log("UpgradeManager Registered");
        OnUpgradeManagerRegistered.Invoke(upgradeManager);
    }

    public void UnregisterGameManager()
    {
        GameManager = null;
    }
    public void UnregisterLoadManager()
    {
        LoadManager = null;
    }
    public void UnregisterSaveManager()
    {
        SaveManager = null;
    }
    public void UnregisterPatientManager()
    {
        PatientManager = null;
    }
    public void UnregisterPlayerManager()
    {
        PatientManager = null;
    }
    public void UnregisterUIManager()
    {
        UIManager = null;
    }
    public void UnregisterTimerManager()
    {
        TimerManager = null;
    }
    public void UnregisterUpgradeManager()
    {
        UpgradeManager = null;
    }
    public void PressPlay()
    {
        OnPlayPressed.Invoke();
    }
}
