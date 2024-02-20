using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingState : BaseState
{

    public LoadingState(int appState) : base(appState)
    {
    }

    private void StartLoading(LoadManager loadManager)
    {
        loadManager.LoadGame();
        loadManager.OnGameSceneLoaded.AddListener(UnloadLoadingState);
    }



    private void LoadLoadingState()
    {
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Additive);
        if (App.Instance.GameplayCore.LoadManager != null)
            StartLoading(App.Instance.GameplayCore.LoadManager);
        else
            App.Instance.GameplayCore.OnLoadManagerRegistered.AddListener(StartLoading);
    }
    private void UnloadLoadingState()
    {
        Parent.MakeTransition((int)EAppState.Game);
    }
    public override void Initialize()
    {
        LoadLoadingState();
        base.Initialize();
    }
    public override void OnExit(int next)
    {
        SceneManager.UnloadSceneAsync("LoadingScreen");
        base.OnExit(next);
    }
}
