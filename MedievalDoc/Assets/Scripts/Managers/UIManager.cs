using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject notebookCanvas;
    [SerializeField] GameObject patientCardCanvas;
    [SerializeField] GameObject DebugUICanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject uiPrefab;
    [SerializeField] private Canvas loadingScreen;
    [SerializeField] private NotebookDataHandler notebookDataHandler;
    [SerializeField] public QuestUI questUI;
    [SerializeField] public UpgradeUI upgradeUI;
    [SerializeField] public TextMeshProUGUI winText;
    private PlayerInputActions playerInputActions;
    public GameObject UiPrefab { get { return uiPrefab; } }
    public GameObject PauseMenu { get { return pauseMenu; } }
    private bool isPauseEnabled = false;

    private GameObject currentCheatCanvas;
    public bool IsPauseEnabled
    {
        get
        {
            return isPauseEnabled;
        }
        set {
            isPauseEnabled = value;
        }
    }

    public NotebookDataHandler NotebookDataHandler
    {
        get { return notebookDataHandler; }
        private set { notebookDataHandler = value; }
    }

    private GameObject instantiatedUI;
    private bool isNotebookEnabled = false;
    private bool isPatientCardEnabled = false;
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
    public bool IsPatientCardEnabled
    {
        get
        {
            return isPatientCardEnabled;
        }
        set
        {
            isPatientCardEnabled = value;
        }
    }


    private void Awake()
    {
        App.Instance.GameplayCore.RegisterUIManager(this);
    }

    private void Start()
    {
        //QuestFunctionality.TutorialQuestLine();
        uiPrefab.SetActive(true);
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Debug.Enable();
        playerInputActions.Player.Pause.performed += PauseMenuFunction;
        playerInputActions.Debug.OpenDebug.performed += OpenDebugWindow;
        App.Instance.GameplayCore.GameManager.OnGameWin.AddListener(DisplayWinMessage);
    }
    private void OnDestroy()
    {
        App.Instance.GameplayCore.UnregisterUIManager();
    }

    public void DisplayWinMessage() => winText.gameObject.SetActive(true);
    public void InitializeUpgradeBoard() => upgradeUI.InitializeUpgradeBoard();
    public void DisableUpgradeBoard() => upgradeUI.ClearUpgradeBoard();
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    private void OpenDebugWindow(UnityEngine.InputSystem.InputAction.CallbackContext callback) 
    {
        if (currentCheatCanvas == null)
            currentCheatCanvas = Instantiate(DebugUICanvas);
        else
            Destroy(currentCheatCanvas);
    }
#endif
    public void UpgradeBoard()
    {
        if (!upgradeUI.isActive)
            InitializeUpgradeBoard();
        else
            DisableUpgradeBoard();
    }

    public void DisableLoadingScreen()
    {
        loadingScreen.enabled = false;
    }

    public void PauseMenuFunction(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {

        if(isNotebookEnabled){  // disable notebook instead of pausing the game
            isNotebookEnabled = false;
            Destroy(instantiatedUI);
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
        instantiatedUI = Instantiate(notebookCanvas);
        instantiatedUI.SetActive(true);
        isNotebookEnabled = true; 
        } 
    }
    private void DisableNoteBook()
    {
        isNotebookEnabled = false;
        Destroy(instantiatedUI);
    }

    private void EnablePatientCard()
    {
        isPatientCardEnabled = true;
        patientCardCanvas.GetComponent<PatientCard>().background.gameObject.SetActive(true);
    }
    public void DisablePatientCard()
    {
        isPatientCardEnabled = false;
        patientCardCanvas.GetComponent<PatientCard>().background.gameObject.SetActive(false);
    }

    public void ChangeNotebookState()
    {
        if (isNotebookEnabled)
            DisableNoteBook();
        else 
            EnableNotebook();
    }
    public void ChangePatientCardState()
    {
        if (isPatientCardEnabled)
            DisablePatientCard();
        else
            EnablePatientCard();
    }
}
