using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PatientNotebook : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI symptomsText;
    [SerializeField] private TextMeshProUGUI historyText;

    private SicknessScriptableObject sickness;
    public SicknessScriptableObject Sickness
    {
        get { return sickness; }
        set { sickness = value; }
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
    public void SetNotebookHistory()
    {
        historyText.text = "Lorem ipsum dolores chuj";
        //TODO: SET HISTORIA
    }

    void Start()
    {
        SetNotebookSymptoms(sickness.symptomList);
        SetNotebookHistory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
