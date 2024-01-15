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
    public void SetNotebookSymptoms(List<SicknessScriptableObject.SymptomStruct> symptoms)
    {
        symptomsText.text = "";
        foreach (var symptom in symptoms)
        {
            symptomsText.text += $"-{symptom.GetSymptomName()} \n";
        }
        if(symptomsText.text == "")
        {
            symptomsText.text = "Chlop zdrowy B)";
        }
    }
    public void SetNotebookHistory(string story)
    {
        historyText.text = patient.patientStory;
        //TODO: SET HISTORIA
    }

    void Start()
    {
        SetNotebookSymptoms(patient.sickness.symptomList);
        SetNotebookHistory(patient.patientStory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
