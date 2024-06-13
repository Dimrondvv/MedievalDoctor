using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DayAndNightController : MonoBehaviour
{

    [SerializeField] 
    private bool isSunRotating = true;

    [SerializeField]
    private int SummaryHour = 20;



    [SerializeField]
    private float timeMultiplier; // How fast time pass in the game

    PatientManager patientManager;
    private float timeMultiplayerCopy;


    public float TimeMultiplier {
        get { return timeMultiplier;  }
        set { timeMultiplier = value;  }
    }

    [SerializeField]
    private float startHour; // On what hour day should start

    [SerializeField]
    private float maxSunLightIntensity;

    [SerializeField]
    private float maxMoonLightIntensity;

    [SerializeField]
    private float sunRotationY;
    [SerializeField]
    private float sunriseHour;

    [SerializeField]
    private float sunsetHour;

    [SerializeField]
    private int dayCounter=1;
    public int DayCounter
    { 
        get { return dayCounter; } 
        set { dayCounter = value; }
    }
    private int daytemp;
    private int dayCounterTemp;

    [SerializeField]
    private Light sunLight;

    [SerializeField]
    private Light moonLight;

    [SerializeField]
    private Color dayAmbinetLight;

    [SerializeField]
    private Color nightAmbientLight;

    [SerializeField]
    private AnimationCurve lightChangeCurve;

    [SerializeField]
    private Image clock;

    [SerializeField]
    private GameObject LightsToTurnOn;

    public static UnityEvent<float> OnEndOfaDay = new UnityEvent<float>();
    public static UnityEvent OnNewDay = new UnityEvent();

    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    private DateTime currentTime;
    public DateTime CurrentTime
    {
        get { return currentTime; }
        set { currentTime = value; }
    }



    public bool UseJustOnce;

    // Start is called before the first frame update
    void Start()
    {
        timeMultiplayerCopy = timeMultiplier;
        patientManager = App.Instance.GameplayCore.PatientManager;
        PatientManager.OnPatientSpawn.AddListener(StartTimer);
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
        timeMultiplier = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        TurnOnLights();
    }

    private void StartTimer(Patient patient)
    {
        timeMultiplier = timeMultiplayerCopy;
    }

    private void UpdateTimeOfDay() {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        dayCounterTemp = currentTime.Day - DateTime.Now.Day + 1;
        if(daytemp != dayCounterTemp)
        {
            dayCounter += 1;
            App.Instance.GameplayCore.DaySummaryManager.onNewDay.Invoke();
            if (dayCounter == 10)
            {
                App.Instance.GameplayCore.UIManager.ChangeNotebookState();
                Time.timeScale = 0;
            }
        }
        daytemp = dayCounterTemp;

        

        
        if(currentTime.Hour == SummaryHour && !UseJustOnce) {
            UseJustOnce = true;
            OnEndOfaDay?.Invoke(0);
        } 

    }

    private void RotateSun() {
       

        float sunLightRotation;

        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime) {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        } else {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        clock.transform.rotation = Quaternion.AngleAxis(sunLightRotation - 68, Vector3.back);
        //Debug.Log(sunLightRotation);
        //sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);

        if (isSunRotating) {
            sunLight.transform.eulerAngles = new Vector3(sunLightRotation, sunRotationY, 0);
        }

    }

    private void UpdateLightSettings() {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down); // value between -1 and 1, when sun pointing directly down then is 1
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbinetLight, lightChangeCurve.Evaluate(dotProduct));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime) {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0) { // If value is lesser than 0 we need to add 24 
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }

    private void TurnOnLights()
    {
        if (currentTime.Hour >= sunsetHour || currentTime.Hour <= sunriseHour)
        {
            LightsToTurnOn.SetActive(true);
            App.Instance.GameplayCore.GameManager.IsNight = true;
        } else {
            LightsToTurnOn.SetActive(false);
            App.Instance.GameplayCore.GameManager.IsNight = false;
        }
    }

    public void setTimeInHours(int h)
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(h);
    }

    public void resetDay()
    { 
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
        timeMultiplier = 0;
        dayCounter += 1;
        UseJustOnce = false;
        App.Instance.GameplayCore.DaySummaryManager.IsTimeStoped = false;
        App.Instance.GameplayCore.DaySummaryManager.newDay();
    }
}
