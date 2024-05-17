using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DaySummaryManager : MonoBehaviour
{
    [SerializeField]
    private DayAndNightController dayAndNightController;

    //[SerializeField]
    //private PatientManager patientManager;

    [SerializeField]
    private GameObject SummaryUI;

    [SerializeField]
    private bool isSummaryState;

    [SerializeField]
    private bool isTimeStoped;

    private UnityEvent OnTimeStoped = new UnityEvent(); // This is ev


    // Start is called before the first frame update
    void Start()
    {
        DayAndNightController.OnEndOfaDay.AddListener(stopTime);
        OnTimeStoped.AddListener(ChangeToSummaryState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void stopTime(float endTime) {
        dayAndNightController.TimeMultiplier = endTime;
        isTimeStoped = true;
        OnTimeStoped.Invoke();
        //Debug.Log("========= STOP THE TIME =========");
    }

    void ChangeToSummaryState() {
        int patientCount = App.Instance.GameplayCore.PatientManager.patients.Count;
        if (isTimeStoped && patientCount == 0) {
            isSummaryState = true;
            Instantiate(SummaryUI);
        }
    }
}
