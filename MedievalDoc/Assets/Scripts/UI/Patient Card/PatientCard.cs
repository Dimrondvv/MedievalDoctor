using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PatientCard : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] GameObject defaultState;
    [SerializeField] GameObject patientState;
    [SerializeField] TextMeshProUGUI patientName; 
    [SerializeField] TextMeshProUGUI patientSymptomList; 
    [SerializeField] TextMeshProUGUI patientStory; 

    void Start()
    {
        PatientManager.OnPatientSpawn.AddListener(GoToPatientState);
        Patient.OnPatientDeath.AddListener(ReturnToDefaultState);
    }

    

    private void ReturnToDefaultState(Patient patient)
    {
        patientState.SetActive(false);
        defaultState.SetActive(true);
    }
    private void GoToPatientState(Patient patient)
    {
        defaultState.SetActive(false);
        patientState.SetActive(true);
        FillPatientInformation(patient);
    }

    private void FillPatientInformation(Patient patient)
    {

    }
}
