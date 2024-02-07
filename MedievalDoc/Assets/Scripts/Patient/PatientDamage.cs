using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientDamage : MonoBehaviour
{
    Patient patient;
    [SerializeField] PatientHealthBar patientHealthBar;

    private void Start()
    {
        patient = GetComponent<Patient>();
        InvokeRepeating("TakeDamage", 0, 1);
    }

    private void CheckAlive()
    {
        if (patient.Health <= 0)
        {
            patient.isAlive = false;
            patient.Death();
        }
    }

    private void TakeDamage()
    {
        if(patient.Immune){
            // Do Nothing
        }
        else{
            for (int i = 0; i < patient.Sickness.symptomList.Count; i++)
            {
                patient.Health -= patient.Sickness.symptomList[i].symptom.damage;
                patientHealthBar.healthBar();
                CheckAlive();    
            }
            for (int i = 0; i < patient.AdditionalSymptoms.Count; i++)
            {
                patient.Health -= patient.AdditionalSymptoms[i].damage;
                patientHealthBar.healthBar();
                CheckAlive();
            }
        }      
    }
}
