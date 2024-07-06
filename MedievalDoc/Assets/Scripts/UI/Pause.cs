using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool isPaused;
    private UIManager ui;


    public void PauseFunction()
    {
        ui = App.Instance.GameplayCore.UIManager;

        isPaused = true;
        ui.IsPauseEnabled = true;
        //pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeFunction()
    {
        isPaused = false;
        ui.IsNotebookEnabled = false;
        ui.IsPauseEnabled = false;
        ui.PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Options()
    {
        Debug.Log("Options");
    }

    public void MainMenu()
    {
        Debug.Log("MainMenu");

        //int c = SceneManager.sceneCount;
        //for (int i = 0; i < c; i++)
        //{
        //    Scene scene = SceneManager.GetSceneAt(i);
        //    Debug.Log(scene.name);
        //    SceneManager.UnloadSceneAsync(scene);
        //}
        //App.Instance.GameplayCore.OnLoadManagerRegistered.RemoveAllListeners();
        //SceneManager.LoadSceneAsync("GlobalScene");
    }


    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
