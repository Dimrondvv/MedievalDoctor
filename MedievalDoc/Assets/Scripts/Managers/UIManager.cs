using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject notebookCanvas;
    [SerializeField] TextMeshProUGUI symptomsText;
    [SerializeField] TextMeshProUGUI historyText;

    private bool isNotebookEnabled = false;
    public bool IsNotebookEnabled
    {
        get
        {
            return isNotebookEnabled;
        }
    }

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                Debug.Log("UIManager is null");
            return instance;
        }
    }

    public void SetNotebookSymptoms(List<SicknessScriptableObject.SymptomStruct> symptoms)
    {
        symptomsText.text = "";
        foreach(var symptom in symptoms)
        {
            symptomsText.text += $"-{symptom.GetSymptomName()} \n";
        }
    }
    public void SetNotebookHistory()
    {
        historyText.text = "Lorem ipsum dolores chuj";
        //TODO: SET HISTORIA
    }
    public void EnableNotebook(SicknessScriptableObject sickness)
    {
        notebookCanvas.SetActive(true);
        isNotebookEnabled = true;
        SetNotebookSymptoms(sickness.symptomList);
        SetNotebookHistory();
        
    }
    public void DisableNoteBook()
    {
        isNotebookEnabled = false;
        notebookCanvas.SetActive(false);
    }


    private void Awake()
    {
        instance = this;
    }
}
