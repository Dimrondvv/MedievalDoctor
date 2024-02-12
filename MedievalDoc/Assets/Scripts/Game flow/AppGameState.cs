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
        base.Initialize();
        SceneManager.LoadScene("PlayTest", LoadSceneMode.Additive);
    }
}
