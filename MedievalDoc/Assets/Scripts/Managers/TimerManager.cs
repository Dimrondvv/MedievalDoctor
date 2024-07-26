using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimerManager : MonoBehaviour
{
    private int elapsedTime;
    public int ElapsedTime { get { return elapsedTime; } set { elapsedTime = value; } }

    [SerializeField]
    const int HowManySeconds = 10;

    static public float currCountdownValue;
    public IEnumerator StartCountdown(float countdownValue = HowManySeconds)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
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
        elapsedTime = HowManySeconds;
    }
    private void OnDestroy()
    {
        App.Instance.GameplayCore.UnregisterTimerManager();
    }


    void OneSecondTimer()
    {
        elapsedTime-=1;
    }
}
