using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject notebookCanvas;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] private GameObject uiPrefab;
   
    private PlayerInputActions playerInputActions;
    public GameObject UiPrefab { get { return uiPrefab; } }

    private GameObject instantiatedNotebook;
    private bool isNotebookEnabled = false;
    private bool isPauseEnabled = false;
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
        instantiatedNotebook = Instantiate(notebookCanvas);
        instantiatedNotebook.GetComponent<PatientNotebook>().Patient = patient;
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
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
}
