using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySummaryManager : MonoBehaviour
{
    [SerializeField]
    private DayAndNightController dayAndNightController;

    [SerializeField]
    private PatientManager patientManager;


    private DateTime time;
    
    // Start is called before the first frame update
    void Start()
    {
        dayAndNightController.OnEndOfaDay.AddListener(stopTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void stopTime(float endTime) {
        dayAndNightController.TimeMultiplier = endTime;
        Debug.Log("========= STOP THE TIME =========");
        
        
    }
}
