using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimerManager : MonoBehaviour
{
    private static TimerManager instance;
    public static TimerManager Instance { get { return instance; } }
    private int elapsedTime;
    public int ElapsedTime { get { return elapsedTime; } set { elapsedTime = value; } }

    
    //[SerializeField] TextMeshProUGUI timerText;

    private GameObject timerText;
    private GameObject timerChild;

    private void Start()
    {
        InvokeRepeating("OneSecondTimer", 0, 1);
        timerText = UIManager.Instance.UiPrefab;
        elapsedTime = 0;
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
}
