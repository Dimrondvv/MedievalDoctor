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
        if (patient.Health <= 0 && patient.IsAlive == true)
        {
            patient.IsAlive = false;
            patient.Death();
        }
    }

    private void TakeDamage()
    {
        if (patient.Immune || !patient.IsAlive)
            return;
        if (patient.AngryMeter == patient.MaximumAnger)
            return;

        foreach (var symptStruct in patient.Symptoms)
        {
            try
            {
                patient.Health -= System.Int32.Parse(symptStruct.symptomDmg);
                patientHealthBar.healthBar();
                CheckAlive();
            }
            catch
            {
                Debug.LogError($"Cannon convert {symptStruct.symptomDmg} to int");
            }
        }     
    }
}
