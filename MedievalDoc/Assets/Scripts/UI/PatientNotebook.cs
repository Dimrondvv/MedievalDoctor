using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PatientNotebook : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI symptomsText;
    [SerializeField] private TextMeshProUGUI historyText;
    private Patient patient;
    public Patient Patient
    {
        get { return patient; }
        set { patient = value; }
    }
    public void SetNotebookSymptoms(Patient patient)
    {
        symptomsText.text = "";
        foreach (var symptom in patient.DiscoveredSymptoms)
        {
            symptomsText.text += $"-{symptom} \n";
        }
        if(symptomsText.text == "")
        {
            symptomsText.text = "No symptoms";
        }
    }
    public void SetNotebookHistory(string story)
    {
        historyText.text = patient.patientStory;
        //TODO: SET HISTORIA
    }

    void Start()
    {
        SetNotebookSymptoms(patient);
        SetNotebookHistory(patient.patientStory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
