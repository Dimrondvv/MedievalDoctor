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
 
        if (time.Hour >= summaryHour && patientManager.patients.Count == 1) {
            dayAndNightController.TimeMultiplier = 0;
            Debug.Log("========= STOP THE TIME =========");
        }

       
    }
}
