using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimerManager : MonoBehaviour
{
    private static TimerManager instance;
    public static TimerManager Instance { get { return instance; } }
    private int elapsedTime;
    public int ElapsedTime { get { return elapsedTime; } }

    
    //[SerializeField] TextMeshProUGUI timerText;

    bool gamePaused = false;

    private GameObject timerText;
    private GameObject timerChild;

    private void Start()
    {
        InvokeRepeating("OneSecondTimer", 2, 1);
        timerText = UIManager.Instance.UiPrefab;
        timerChild = timerText.transform.GetChild(1).gameObject;
    }

    private void Awake()
    {
        instance = this;
    }

    void OneSecondTimer()
    {
        elapsedTime+=1;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
    }

    void PauseGame()
    {
        Debug.Log("Pause game");
        Time.timeScale = 0f;
        gamePaused = true;
    }

    void ResumeGame()
    {
        Debug.Log("Resume Game");
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void Update(){
        PauseCheck();
    }

    void PauseCheck()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}
