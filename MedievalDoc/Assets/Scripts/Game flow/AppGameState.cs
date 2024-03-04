using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppGameState : BaseState
{
    

    public AppGameState(int appState) : base(appState)
    {
    }

    public override void Initialize()
    {
        if (App.Instance.GameplayCore.GameManager != null)
        {
            App.Instance.GameplayCore.GameManager.OnEndGame.AddListener(GoToMainMenu);
        }
        else {
            App.Instance.GameplayCore.OnGameManagerRegistered.AddListener(RegisterGoToMainMenu);
        }
        base.Initialize();

    }

    private void RegisterGoToMainMenu(GameManager manager)
    {
        App.Instance.GameplayCore.GameManager.OnEndGame.AddListener(GoToMainMenu);
    }

    private void GoToMainMenu()
    {
        Parent.MakeTransition((int)EAppState.MainHub);
    }

    public override void OnExit(int next)
    {
        App.Instance.GameplayCore.OnGameManagerRegistered.RemoveListener(RegisterGoToMainMenu);
        App.Instance.GameplayCore.GameManager.OnEndGame.RemoveListener(GoToMainMenu);
        SceneManager.UnloadSceneAsync("GameScene");
        base.OnExit(next);
    }
}
