using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimerManager : MonoBehaviour
{
    private int elapsedTime;
    public int ElapsedTime { get { return elapsedTime; } set { elapsedTime = value; } }


    //[SerializeField] TextMeshProUGUI timerText;

    private void Awake()
    {
        App.Instance.GameplayCore.RegisterTimerManager(this);
    }
    private void Start()
    {
        InvokeRepeating("OneSecondTimer", 0, 1);
        elapsedTime = 0;
    }
    private void OnDestroy()
    {
        App.Instance.GameplayCore.UnregisterTimerManager();
    }


    void OneSecondTimer()
    {
        elapsedTime+=1;
    }
}
