using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    public float elapsedTime;
    float timeRemaining = 1;
    [SerializeField] public int countdown;
    public bool isAlive = true;
    Patient Patient;

    void CheckAlive()
    {
        if(elapsedTime == countdown)
        {
            isAlive = false;
            Patient.Death();
        }
    }

    private void Awake()
    {
        Patient = GetComponent<Patient>();
    }

    // Update is called once per frame
    void Update()
    {
        OneSecondTimer();
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
            CheckAlive();
        }
    }
}
