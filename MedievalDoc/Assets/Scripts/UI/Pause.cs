using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isPaused;
    private PlayerInputActions playerInputActions;

    private void Update()
    {
        playerInputActions.Player.Pause.performed;
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void PauseMenu(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        if (!isPaused)
        {
            PauseFunction();
        }
        else
        {
            ResumeFunction();
        }
    }

    private void PauseFunction()
    {
        isPaused = true;
        //pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ResumeFunction()
    {
        isPaused = false;
        //pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
