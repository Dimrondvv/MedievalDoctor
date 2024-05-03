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
    [SerializeField] private NotebookDataHandler notebookDataHandler;

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

    public NotebookDataHandler NotebookDataHandler
    {
        get { return notebookDataHandler; }
        private set { notebookDataHandler = value; }
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
    private void Awake()
    {
        App.Instance.GameplayCore.RegisterUIManager(this);
    }

    private void Start()
    {
        uiPrefab.SetActive(true);
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Pause.performed += PauseMenuFunction;
    }
    private void OnDestroy()
    {
        App.Instance.GameplayCore.UnregisterUIManager();
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

    private void EnableNotebook()
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
    private void DisableNoteBook()
    {
        isNotebookEnabled = false;
        Destroy(instantiatedNotebook);
    }

    public void ChangeNotebookState()
    {
        if (isNotebookEnabled)
            DisableNoteBook();
        else 
            EnableNotebook();

    }

    
}
