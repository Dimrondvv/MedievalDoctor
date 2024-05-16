using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySummaryManager : MonoBehaviour
{
    [SerializeField]
    private DayAndNightController dayAndNightController;

    [SerializeField]
    private int summaryHour = 18;

    private DateTime time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time = dayAndNightController.CurrentTime;
 
        if (time.Hour >= summaryHour) {
            dayAndNightController.TimeMultiplier = 0;
        }

        Debug.Log(time);
    }
}
