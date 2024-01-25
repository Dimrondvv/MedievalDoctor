using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PatientNotebook : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI symptomsText;
    [SerializeField] private TextMeshProUGUI historyText;
    [SerializeField] private Image patientColorField;
    [SerializeField] private TextMeshProUGUI patientName;
    private PlayerInputActions playerInputActions;
    private Patient patient;
    private int currentPatientIndex = 0;
    public Patient Patient
    {
        get { return patient; }
        set { patient = value; }
    }
    public void SetNotebookSymptoms(Patient patient)
    {
        symptomsText.text = "";
        foreach (var symptom in patient.DiscoveredSymptoms.Values)
        {
            symptomsText.text += $"-{symptom} \n";
        }
        if(symptomsText.text == "")
        {
            symptomsText.text = "No symptoms";
        }
    }
    public void SetNotebookHistory(Patient patient)
    {
        historyText.text = patient.patientStory;
        //TODO: SET HISTORIA
    }
    public void SetPatientColor(Patient patient)
    {
        patientColorField.color = patient.GetComponentInChildren<Renderer>().material.color;
    }
    public void SetPatientName(Patient patient)
    {
        patientName.text = patient.PatientName;
    }

    public void ChangePatient(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        currentPatientIndex += (int)playerInputActions.Player.RotateBlueprint.ReadValue<float>();
        if (currentPatientIndex >= PatientManager.Instance.patients.Count)
        {
            currentPatientIndex = 0;
        }

        SetNotebookSymptoms(PatientManager.Instance.patients[currentPatientIndex]);
        SetNotebookHistory(PatientManager.Instance.patients[currentPatientIndex]);
        SetPatientColor(PatientManager.Instance.patients[currentPatientIndex]);
        SetPatientName(PatientManager.Instance.patients[currentPatientIndex]);
    }

    void Start()
    {

        SetNotebookSymptoms(PatientManager.Instance.patients[0]);
        SetNotebookHistory(PatientManager.Instance.patients[0]);
        SetPatientColor(PatientManager.Instance.patients[0]);
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.RotateBlueprint.performed += ChangePatient;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
