using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientLayDownPoint : MonoBehaviour
{
    [SerializeField] private bool isOccupied;
    public bool IfOccupied {
        get { return isOccupied; }
        set { isOccupied = value; }
    }

    private void Start()
    {
        Patient.OnCureDisease.AddListener(ReleaseBed);
        Patient.OnPatientDeath.AddListener(ReleaseBed);
    }

    private void ReleaseBed(Patient patient)
    {
        if (!GetComponentInChildren<Patient>() && isOccupied == true)
        {
            isOccupied = false;
        }
    }
}
