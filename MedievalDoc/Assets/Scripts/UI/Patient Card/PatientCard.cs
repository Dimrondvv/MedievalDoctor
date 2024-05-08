using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PatientCard : MonoBehaviour
{
    [SerializeField] public Image background;
    [SerializeField] GameObject defaultState;
    [SerializeField] GameObject patientState;
    [SerializeField] TextMeshProUGUI patientName; 
    [SerializeField] TextMeshProUGUI patientSymptomList; 
    [SerializeField] TextMeshProUGUI patientStory; 

    void Start()
    {
        PatientManager.OnPatientSpawnFinalized.AddListener(GoToPatientState);
        Patient.OnPatientDeath.AddListener(ReturnToDefaultState);
        Patient.OnAddSymptom.AddListener(UpdateSymptoms);
        Patient.OnRemoveSymptom.AddListener(UpdateSymptoms);
        
    }

    

    private void ReturnToDefaultState(Patient patient)
    {
        patientSymptomList.text = ""; //Reset patient symptom list
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
        patientName.text = patient.PatientName;
        patientStory.text = patient.patientStory;

        UpdateSymptoms(null, patient, null);
    }

    private void UpdateSymptoms(Symptom smpt, Patient patient, Tool tool)
    {
        patientSymptomList.text = ""; //Reset patient symptom list
        foreach (var symptom in patient.symptoms)
        {
            patientSymptomList.text += $"- {symptom.GetSymptomName()} \n";
        }
    }
}
