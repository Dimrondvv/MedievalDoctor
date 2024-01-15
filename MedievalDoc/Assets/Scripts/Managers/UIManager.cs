using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject notebookCanvas;
    [SerializeField] GameObject uiPrefab;

    private GameObject instantiatedNotebook;
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

    
    public void EnableNotebook(Patient patient)
    {
        SicknessScriptableObject sickness = patient.sickness; 

        instantiatedNotebook = Instantiate(notebookCanvas);
        instantiatedNotebook.GetComponent<PatientNotebook>().Sickness = sickness;
        instantiatedNotebook.SetActive(true);
        isNotebookEnabled = true;
        
        
    }
    public void DisableNoteBook()
    {
        isNotebookEnabled = false;
        Destroy(instantiatedNotebook);
    }

    private void ChangeNotebookState(Patient patient)
    {
        if (isNotebookEnabled)
            DisableNoteBook();
        else 
            EnableNotebook(patient);

    }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        PatientEventManager.Instance.OnHandInteract.AddListener(ChangeNotebookState);
        uiPrefab.SetActive(true);
    }
}
