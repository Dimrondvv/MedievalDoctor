using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class PatientCard : MonoBehaviour
{
    [SerializeField] public Image background;
    [SerializeField] GameObject defaultState;
    [SerializeField] GameObject patientState;
    [SerializeField] TextMeshProUGUI patientName; 
    [SerializeField] TextMeshProUGUI patientSymptomList; 
    [SerializeField] TextMeshProUGUI patientStory;
    private AudioSource patientReleaseAudio;


    private void Start()
    {
        PatientManager.OnPatientSpawnFinalized.AddListener(GoToPatientState);
        Patient.OnPatientDeath.AddListener(ReturnToDefaultState);
        Patient.OnAddSymptom.AddListener(UpdateSymptoms);
        Patient.OnRemoveSymptom.AddListener(UpdateSymptoms);
        patientReleaseAudio = GetComponent<AudioSource>();

        //playerInputActions = new PlayerInputActions();
        //playerInputActions.Player.Enable();
        //playerInputActions.Player.PatientCard.performed += CloseCard;

    }

    //private void CloseCard(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    //{

    //}
    public void HandlePatientRelease()
    {
        var patient = App.Instance.GameplayCore.PatientManager.patients[0];
        patientReleaseAudio.Play();
        ReturnToDefaultState(patient);
        PatientManager.ReleasePatient.Invoke();
        App.Instance.GameplayCore.UIManager.DisablePatientCard();
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
            if(!symptom.isHidden)
                patientSymptomList.text += $"- {symptom.GetSymptomName()} \n";
        }
    }
}
