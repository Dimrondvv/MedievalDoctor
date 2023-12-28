using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    float timeRemaining = 1;

    void Update()
    {

        OneSecondTimer();


       // elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OneSecondTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            elapsedTime += 1;
            timeRemaining = 1;
            TimeCheck();
        }
    }



    void TimeCheck()
    {
        if(elapsedTime > 1)
        {
            if(elapsedTime % 5 == 0)
            {
               // Debug.Log("Min�o 5 sekund");
            }
        }
    }

}
