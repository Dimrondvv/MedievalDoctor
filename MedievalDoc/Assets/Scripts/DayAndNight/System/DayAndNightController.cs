using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DayAndNightController : MonoBehaviour
{

    [SerializeField] PatronCharacter patronCharacter;

    [SerializeField]
    private float timeMultiplier; // How fast time pass in the game
    private float basicTimeMultiplier;
    public float TimeMultiplier
    {
        get { return timeMultiplier; }
        set { timeMultiplier = value; }
    }

    public float BasicTimeMultiplayer
    {
        get { return basicTimeMultiplier; }
        set { basicTimeMultiplier = value; }
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



    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    private DateTime currentTime;
    public DateTime CurrentTime
    {
        get { return currentTime; }
        set { currentTime = value; }
    }


    public static UnityEvent DayActivation = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        basicTimeMultiplier = timeMultiplier;
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
        //UpdateTimeOfDay(37800);
    }

    // Update is called once per frame
    void Update()
    {
        TurnOnLights();
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        
    }



    public void UpdateTimeOfDay(int optionalTimeAdd = 0, bool isTimeSkip = false) {
        if (currentTime.Hour > 21 && !isTimeSkip)
            return;

        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier + optionalTimeAdd);
        dayCounterTemp = currentTime.Day - DateTime.Now.Day + 1;
        if(daytemp != dayCounterTemp)
        {
            dayCounter = dayCounterTemp;
            if(dayCounter == 10)
            {
                UIManager.Instance.EnableNotebook();
                Time.timeScale = 0;
            }
        }
        daytemp = dayCounterTemp;

        

        if (currentTime.Day == patronCharacter.DeadLineDay && currentTime.Hour == patronCharacter.DeadLineHour)
        {
            if (patronCharacter.IsQuestActive == true)
            {
                patronCharacter.DisableQuest();
            }

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
        sunLight.transform.eulerAngles = new Vector3(sunLightRotation, sunRotationY, 0);

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
            if(!App.Instance.GameplayCore.GameManager.IsNight == false)
            {
                DayActivation.Invoke();
            }
            App.Instance.GameplayCore.GameManager.IsNight = false;
        }
    }
}
