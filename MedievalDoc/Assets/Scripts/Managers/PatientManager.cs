using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientManager : MonoBehaviour
{
    private static PatientManager instance;
    public static PatientManager Instance
    {
        get 
        { 
            return instance;
        }
    }

    public List<Patient> patients;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    private void Start()
    {
        Patient.OnReleasePatient.AddListener(RemovePatientFromList);
        Patient.OnPatientDeath.AddListener(RemovePatientFromList);
    }

    private void RemovePatientFromList(Patient patient)
    {
        patients.Remove(patient);
    }
}
