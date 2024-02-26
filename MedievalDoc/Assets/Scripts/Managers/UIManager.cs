using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject notebookCanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject uiPrefab;
    [SerializeField] private Canvas loadingScreen;
   
    private PlayerInputActions playerInputActions;
    public GameObject UiPrefab { get { return uiPrefab; } }
    public GameObject PauseMenu { get { return pauseMenu; } }
    private bool isPauseEnabled = false;
    public bool IsPauseEnabled
    {
        get
        {
            return isPauseEnabled;
        }
        set{
            isPauseEnabled = value;
        }
    }


    private GameObject instantiatedNotebook;
    private bool isNotebookEnabled = false;
    public bool IsNotebookEnabled
    {
        get
        {
            return isNotebookEnabled;
        }
        set{
            isNotebookEnabled = value;
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

    public void DisableLoadingScreen()
    {
        loadingScreen.enabled = false;
    }

    public void PauseMenuFunction(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {

        if(isNotebookEnabled){  // disable notebook instead of pausing the game
            isNotebookEnabled = false;
            Destroy(instantiatedNotebook);
        }

        //GetComponent<Pause>().isPaused
        else if (!pauseMenu.GetComponent<Pause>().isPaused)
        {
            pauseMenu.GetComponent<Pause>().PauseFunction();
            isPauseEnabled = true;
            // turn on canvas
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.GetComponent<Pause>().ResumeFunction();
            isPauseEnabled = false;
            // turn off canvas
            pauseMenu.SetActive(false);
        }
    }

    public void EnableNotebook()
    {

        if(isPauseEnabled){
            return;
        }
        else{
        instantiatedNotebook = Instantiate(notebookCanvas);
        instantiatedNotebook.SetActive(true);
        isNotebookEnabled = true; 
        } 
    }
    public void DisableNoteBook()
    {
        isNotebookEnabled = false;
        Destroy(instantiatedNotebook);
    }

    private void ChangeNotebookState(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        if (isNotebookEnabled)
            DisableNoteBook();
        else 
            EnableNotebook();

    }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        uiPrefab.SetActive(true);
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Pause.performed += PauseMenuFunction;
        playerInputActions.Player.Journal.performed += ChangeNotebookState;
    }
}
