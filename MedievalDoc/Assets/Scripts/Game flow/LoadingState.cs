using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingState : BaseState
{
    public LoadingState(int appState) : base(appState)
    {
    }

    public override void Initialize()
    {
        //SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Additive);
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        //loadLevel.allowSceneActivation = false;
        while (!loadLevel.isDone)
        {
            Debug.Log("Loading");
        }
        //loadLevel.allowSceneActivation = true;
        //SceneManager.UnloadSceneAsync("LoadingScreen");
        Parent.MakeTransition((int)EAppState.Game);
        base.Initialize();
    }
    public override void OnExit(int next)
    {
        SceneManager.UnloadSceneAsync("LoadingScreen");
        base.OnExit(next);
    }
}
