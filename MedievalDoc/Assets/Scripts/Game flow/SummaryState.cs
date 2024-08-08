using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SummaryState : BaseState
{
   public SummaryState(int appState) : base(appState)
    {
    }

    public override void Initialize()
    {
        base.Initialize();
        LoadSummaryState();
    }

    private void LoadSummaryState()
    {
        SceneManager.LoadScene("SummaryScene", LoadSceneMode.Additive);
        App.Instance.GameplayCore.GameManager.OnLevelComplete.AddListener(HandleLevelComplition);

    }

    private void HandleLevelComplition()
    {
        Parent.MakeTransition((int)EAppState.Intro);
        App.Instance.GameplayCore.GameManager.OnLevelComplete.RemoveListener(HandleLevelComplition);

    }

    public override void OnExit(int next)
    {
        SceneManager.UnloadSceneAsync("SummaryScene");
        base.OnExit(next);
    }
}
