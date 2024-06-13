using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientAngry : MonoBehaviour
{
    Patient patient;
    [SerializeField] PatientAngryBar patientAngryBar;

    private void Start()
    {
        patient = GetComponent<Patient>();
        InvokeRepeating("MadMeter", 0, 1);
    }

    private void MadMeter()
    {
        if (!patient.IsAlive || patient.AngryMeter == patient.MaximumAnger)
        {
            return;
        }
        else
        {
            patient.IncreaseMaddness(1);
            patientAngryBar.angryBar();
            CheckAnger();
        }
    }

    private void CheckAnger()
    {
        if (patient.AngryMeter == patient.MaximumAnger)
        {
            patient.RageQuit();
        }
    }

}
