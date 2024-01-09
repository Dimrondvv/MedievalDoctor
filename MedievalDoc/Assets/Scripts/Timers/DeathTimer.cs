using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    public int elapsedTime;
    public int damage;
    public bool isAlive = true;
    Patient Patient;
    SpawnPatientTimer SpawnPatientTimer;

    void Start()
    {
        InvokeRepeating("OneSecondTimer", 2, 1);
    }

    void CheckAlive()
    {
        if(Patient.health <= 0)
        {
            isAlive = false;
            Patient.Death();
        }
    }

    private void Awake()
    {
        Patient = GetComponent<Patient>();
        SpawnPatientTimer = GetComponent<SpawnPatientTimer>();
    }

    void OneSecondTimer()
    {
        elapsedTime+=1;
        TakeDamage();
        CheckAlive();         
    }

    void TakeDamage()
    {
        Patient.health -= damage;
    }
}
