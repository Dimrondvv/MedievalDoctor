using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class AppGameState : BaseState
{
    

    public AppGameState(int appState) : base(appState)
    {
    }

    public override void Initialize()
    {
        base.Initialize();
        Debug.Log("++++ Inicjalizacja ++++");
        App.Instance.GameplayCore.DaySummaryManager.ChangingToSummaryState.AddListener(ChanageToSummaryState);
    }

    private void ChanageToSummaryState()
    {
        Debug.Log("Rodzic robic tranzycje");
        Parent.MakeTransition((int)EAppState.Summary);
    }


    public override void OnExit(int next)
    {
        SceneManager.UnloadSceneAsync("GameScene");
        base.OnExit(next);
    }


    //public void ChanageToSummaryState()
    //{
    //    Parent.MakeTransition((int)EAppState.Summary);
    //}
}
