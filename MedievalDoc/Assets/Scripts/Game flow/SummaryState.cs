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
        App.Instance.GameplayCore.DaySummaryManager.onMenuPressed.AddListener(HandleMenuPressed);
    }

    private void HandleEndDayPressed()
    {
        App.Instance.GameplayCore.DaySummaryManager.startDay();
        App.Instance.GameplayCore.DaySummaryManager.onEndDayPressed.RemoveListener(HandleEndDayPressed);
        Parent.MakeTransition((int)EAppState.Game);
    }

    private void HandleMenuPressed()
    {
        int c = SceneManager.sceneCount;
        for (int i = 0; i < c; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            Debug.Log(scene.name);
            SceneManager.UnloadSceneAsync(scene);
        }
        App.Instance.GameplayCore.OnLoadManagerRegistered.RemoveAllListeners();
        SceneManager.LoadSceneAsync("GlobalScene");
    }

    public override void OnExit(int next)
    {
        SceneManager.UnloadSceneAsync("SummaryScene");
        base.OnExit(next);
    }
}
