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
    [SerializeField] private List<Button> bookmarks;
    private PlayerInputActions playerInputActions;
    private Patient patient;
    private int currentPatientIndex = 0;
    private int currentBookmarkIndex = 0;
    public Patient Patient
    {
        get { return patient; }
        set { patient = value; }
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Bookmarks.performed += ChangeBookMark;

    }

    public void SetNotebookSymptoms(Patient patient)
    {
        if (patient == null)
            return;
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
        if (patient == null)
            return;
        historyText.text = patient.patientStory;
        //TODO: SET HISTORIA
    }
    public void SetPatientColor(Patient patient)
    {
        if (patient == null)
            return;
        patientColorField.color = patient.GetComponentInChildren<Renderer>().material.color;
    }
    public void SetPatientName(Patient patient)
    {
        if (patient == null)
        {
            patientName.text = "No patients";
            return;
        }
        patientName.text = patient.PatientName;
    }

    public void ChangeBookMark(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        currentBookmarkIndex += (int)playerInputActions.Player.Bookmarks.ReadValue<float>();
        if (currentBookmarkIndex >= bookmarks.Count)
        {
            currentBookmarkIndex = 0;
        }
        else if (currentBookmarkIndex < 0)
        {
            currentBookmarkIndex = bookmarks.Count - 1;
        }
        bookmarks[currentBookmarkIndex].onClick.Invoke();
    }
}
