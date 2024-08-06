using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DaySummaryManager : MonoBehaviour
{
    [SerializeField]
    public DayAndNightController dayAndNightController;

    [SerializeField]
    private Fading fadeing;

    //[SerializeField]
    //private PatientManager patientManager;

    [SerializeField]
    private GameObject SummaryUI;

    [SerializeField]
    private GameObject callPatient;

    [SerializeField]
    private bool isSummaryState;

    [SerializeField]
    private bool isTimeStoped;

    private AudioSource bellAudio;

    public bool IsTimeStoped
    {
        get { return isTimeStoped;  }
        set { isTimeStoped = value; }
    }

    private int patientCount;

    private bool isNewDay;

    public bool IsNewDay
    {
        get { return isNewDay; }
        set { isNewDay = value; }
    }

    private UnityEvent OnTimeStoped = new UnityEvent();
    public  UnityEvent ChangingToSummaryState = new UnityEvent();
    public UnityEvent onEndDayPressed = new UnityEvent();
    public UnityEvent onMenuPressed = new UnityEvent();
    public UnityEvent onNewDay = new UnityEvent();




    private void Awake()
    {
        App.Instance.GameplayCore.RegisterDaySummaryManager(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        bellAudio = GetComponent<AudioSource>();
        DayAndNightController.OnEndOfaDay.AddListener(stopTime);
        OnTimeStoped.AddListener(ChangeToSummaryState);
        callPatient = GameObject.Find("Spawn Patient");
    }
    
    public void newDay()
    {
        OnTimeStoped.AddListener(ChangeToSummaryState);
        DayAndNightController.OnEndOfaDay.AddListener(stopTime);
        onNewDay?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        patientCount = App.Instance.GameplayCore.PatientManager.patients.Count;

        if (isTimeStoped && patientCount == 0)
        {
            OnTimeStoped.Invoke();
        }
    }

    void stopTime(float endTime) {
        //bellAudio.Play();
        callPatient.layer = 0;
        dayAndNightController.TimeMultiplier = endTime;
        isTimeStoped = true;
    }

    void ChangeToSummaryState() {
        if (isTimeStoped && patientCount == 0) {
            fadeing.FadeOut();

            if (!fadeing.Locked) {       
                ChangingToSummaryState?.Invoke();
                OnTimeStoped.RemoveListener(ChangeToSummaryState);
                DayAndNightController.OnEndOfaDay.RemoveListener(stopTime);
                fadeing.Locked = true;
            }
        }
    }

    public void PressEndDay()
    {
        onEndDayPressed.Invoke();
    }

    public void PressMenu()
    {
        onMenuPressed.Invoke();
    }


    public void startDay()
    {
        fadeing.FadeIn();
        dayAndNightController.resetDay();
        callPatient.layer = 7;
    }

    private void OnDestroy()
    {
        App.Instance.GameplayCore.UnregisterDaySummaryManager();
    }
}


