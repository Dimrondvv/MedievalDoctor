using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugFunctionsMisc : MonoBehaviour
{

    public void DestroyWindow()
    {
        Destroy(gameObject);
    }
    public void EnterDevScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "RoomsTest")
            SceneManager.UnloadSceneAsync(currentScene);

        SceneManager.LoadSceneAsync("DevScene", LoadSceneMode.Additive);
    }
}
