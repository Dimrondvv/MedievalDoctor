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
        App.Instance.GameplayCore.DaySummaryManager.onEndDayPressed.AddListener(HandleEndDayPressed);
    }

    private void HandleEndDayPressed()
    {
        App.Instance.GameplayCore.DaySummaryManager.startDay();
        App.Instance.GameplayCore.DaySummaryManager.onEndDayPressed.RemoveListener(HandleEndDayPressed);
        Parent.MakeTransition((int)EAppState.Game);
    }

    public override void OnExit(int next)
    {
        SceneManager.UnloadSceneAsync("SummaryScene");
        base.OnExit(next);
    }
}
