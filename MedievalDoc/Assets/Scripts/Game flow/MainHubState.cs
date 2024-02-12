using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainHubState : BaseState
{
    public MainHubState(int appState) : base(appState)
    {
    }

    public override void Initialize()
    {
        base.Initialize();
        LoadMainHubState();
    }

    private void LoadMainHubState()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        App.Instance.GameplayCore.OnPlayPressed.AddListener(HandlePlayPressed);
    }

    private void HandlePlayPressed()
    {
        App.Instance.GameplayCore.OnPlayPressed.RemoveListener(HandlePlayPressed);
        Parent.MakeTransition((int)EAppState.Game);
    }

    public override void OnExit(int next)
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        base.OnExit(next);
    }
}
