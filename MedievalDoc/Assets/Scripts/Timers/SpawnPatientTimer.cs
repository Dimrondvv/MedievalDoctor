using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPatientTimer : MonoBehaviour
{
    public float elapsedTime;
    float timeRemaining = 1;
    [SerializeField] int countdown;
    [SerializeField] GameObject Patient;
    [SerializeField] int SpawnTime; // Timer for patient spawning
    GameObject SpawnedPatient;

    void Spawning()
    {
        SpawnedPatient = Instantiate(Patient, new Vector3(10,1,7), Quaternion.identity);
        SpawnedPatient.GetComponent<DeathTimer>().elapsedTime = 0; // time of his life
        SpawnedPatient.GetComponent<DeathTimer>().countdown = 7; // for how long he can live without help
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
            TimeCheck();
        }
    }


    void TimeCheck()
    {
        Debug.Log(elapsedTime);

        if (elapsedTime > 1)
        {

            if (elapsedTime % SpawnTime == 0)
            {
                Debug.Log("Spawning patient");
                Spawning();
            }
        }
    }



}
