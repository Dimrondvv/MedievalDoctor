using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientManager : MonoBehaviour
{

    public List<Patient> patients;

    private void Awake()
    {
        App.Instance.GameplayCore.RegisterPatientManager(this);
    }
    private void Start()
    {
        Patient.OnCureDisease.AddListener(RemovePatientFromList);
        Patient.OnPatientDeath.AddListener(RemovePatientFromList);
    }
    private void OnDestroy()
    {
        App.Instance.GameplayCore.UnregisterPatientManager();
    }

    private void RemovePatientFromList(Patient patient)
    {
        patients.Remove(patient);
        
    }
}
