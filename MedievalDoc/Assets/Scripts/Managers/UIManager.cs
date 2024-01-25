using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject notebookCanvas;
    [SerializeField] private GameObject uiPrefab;
    public GameObject UiPrefab { get { return uiPrefab; } }

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
    private PlayerInputActions playerInputActions;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                Debug.Log("UIManager is null");
            return instance;
        }
    }



    
    private void EnableNotebook()
    {
        instantiatedNotebook = Instantiate(notebookCanvas);
        instantiatedNotebook.SetActive(true);
        isNotebookEnabled = true;
        
        
    }
    private void DisableNoteBook()
    {
        isNotebookEnabled = false;
        Destroy(instantiatedNotebook);
    }

    public void ChangeNotebookState(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        if (isNotebookEnabled)
            DisableNoteBook();
        else 
            EnableNotebook();
    }

    private void Awake()
    {
        instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Journal.performed += ChangeNotebookState;
    }
    private void Start()
    {
        uiPrefab.SetActive(true);
    }
}
