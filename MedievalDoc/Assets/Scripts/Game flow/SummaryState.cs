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
        App.Instance.GameplayCore.GameManager.OnLevelComplete.AddListener(HandleMenuPressed);
        //App.Instance.GameplayCore.DaySummaryManager.onEndDayPressed.AddListener(HandleEndDayPressed);
        // App.Instance.GameplayCore.DaySummaryManager.onMenuPressed.AddListener(HandleMenuPressed);
    }

    private void HandleEndDayPressed()
    {
        Parent.MakeTransition((int)EAppState.Intro);
        App.Instance.GameplayCore.DaySummaryManager.startDay();
        App.Instance.GameplayCore.DaySummaryManager.onEndDayPressed.RemoveListener(HandleEndDayPressed);
        
    }

    private void HandleMenuPressed()
    {
        Parent.MakeTransition((int)EAppState.Intro);
        App.Instance.GameplayCore.GameManager.OnLevelComplete.RemoveListener(HandleMenuPressed);

    }

    public override void OnExit(int next)
    {
        SceneManager.UnloadSceneAsync("SummaryScene");
        base.OnExit(next);
    }
}
