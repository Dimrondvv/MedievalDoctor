using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
