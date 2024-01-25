using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayAndNightController : MonoBehaviour
{
    [SerializeField]
    private float timeMultiplier; // How fast time pass in the game

    [SerializeField]
    private float startHour; // On what hour day should start

    [SerializeField]
    private Light sunLight;

    [SerializeField]
    private Light moonLight;

    [SerializeField]
    private float sunriseHour;

    [SerializeField]
    private float sunsetHour;

    [SerializeField]
    private Color dayAmbinetLight;

    [SerializeField]
    private Color nightAmbientLight;

    [SerializeField]
    private AnimationCurve lightChangeCurve;

    [SerializeField]
    private float maxSunLightIntensity;

    [SerializeField]
    private float maxMoonLightIntensity;

    [SerializeField]
    private int dayCounter;

    [SerializeField]
    private Image clock;

    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    private DateTime currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
    }

    private void UpdateTimeOfDay() {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        dayCounter = currentTime.Day - DateTime.Now.Day + 1;
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

        clock.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.back);
        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
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
}