using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimerManager : MonoBehaviour
{
    private int elapsedTime;
    public int ElapsedTime { get { return elapsedTime; } set { elapsedTime = value; } }

    float currCountdownValue;
    public IEnumerator StartCountdown(float countdownValue = 600)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
    }

    //[SerializeField] TextMeshProUGUI timerText;

    private void Awake()
    {
        App.Instance.GameplayCore.RegisterTimerManager(this);
    }
    private void Start()
    {
        StartCoroutine(StartCountdown());
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
