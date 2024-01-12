using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientDamage : MonoBehaviour
{
    public int damage;
    Patient patient;
    private int tempelapsedTime;


    private void Start()
    {
        patient = GetComponent<Patient>();
        tempelapsedTime = TimerManager.Instance.ElapsedTime;

    }

    private void CheckAlive()
    {
        if (patient.health <= 0)
        {
            patient.isAlive = false;
            patient.Death();
        }
    }

    private void Update()
    {
        
        if(tempelapsedTime < TimerManager.Instance.ElapsedTime)
        {
            TakeDamage();
            tempelapsedTime = TimerManager.Instance.ElapsedTime;
        }
    }

    private void TakeDamage()
    {

        for (int i = 0; i < patient.sickness.symptomList.Count; i++)
        {
            patient.health -= patient.sickness.symptomList[i].symptom.damage;
            CheckAlive();
        }
        
    }
}
