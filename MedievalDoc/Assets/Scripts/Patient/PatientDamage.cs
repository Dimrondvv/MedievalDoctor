using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientDamage : MonoBehaviour
{
    public int damage;
    Patient patient;
    private int tempelapsedTime;
    [SerializeField] PatientHealthBar patientHealthBar;

    private void Start()
    {
        patient = GetComponent<Patient>();
        tempelapsedTime = TimerManager.Instance.ElapsedTime;
        //patientHealthBar = GetComponent<PatientHealthBar>();
    }

    private void CheckAlive()
    {
        if (patient.Health <= 0)
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
        if(patient.Immune){

        }
        else{
            for (int i = 0; i < patient.sickness.symptomList.Count; i++)
            {
                patient.Health -= patient.sickness.symptomList[i].symptom.damage;
                patientHealthBar.healthBar();
                CheckAlive();
                
            }
        }      
    }
}
