using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool isPaused;

    public void PauseFunction()
    {
        isPaused = true;
        UIManager.Instance.IsPauseEnabled = true;
        //pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeFunction()
    {
        isPaused = false;
        UIManager.Instance.IsNotebookEnabled = false;
        UIManager.Instance.IsPauseEnabled = false;
        UIManager.Instance.PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Options()
    {
        Debug.Log("Options");
    }

    public void MainMenu()
    {
        Debug.Log("MainMenu");
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }


    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
