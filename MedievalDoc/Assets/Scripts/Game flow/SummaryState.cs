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
        Debug.Log("£adowanie sceny");
        SceneManager.LoadScene("SummaryScene", LoadSceneMode.Additive);
        
    }

    

    public override void OnExit(int next)
    {
        SceneManager.UnloadSceneAsync("SummaryScene");
        base.OnExit(next);
    }
}
