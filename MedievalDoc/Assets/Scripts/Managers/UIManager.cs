using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject notebookCanvas;
    [SerializeField] GameObject patientCardCanvas;
    [SerializeField] GameObject newspaperCanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject uiPrefab;
    [SerializeField] private Canvas loadingScreen;
    [SerializeField] private NotebookDataHandler notebookDataHandler;
    [SerializeField] public QuestUI questUI;
    [SerializeField] public UpgradeUI upgradeUI;
    [SerializeField] public NewsPaper newsPaper;
    [SerializeField] public TextMeshProUGUI winText;
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
    private GameObject currentNewspaperUI;
    private bool isNotebookEnabled = false;
    private bool isPatientCardEnabled = false;
    private bool isNewspaperEnabled = false;
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

    public bool IsNewspaperEnabled
    {
        get
        {
            return isNewspaperEnabled;
        }
        set
        {
            isNewspaperEnabled = value;
        }
    }


    private void Awake()
    {
        App.Instance.GameplayCore.RegisterUIManager(this);
        App.Instance.GameplayCore.DaySummaryManager.onNewDay.AddListener(UpdateNewspaper);

    }

    private void Start()
    {
        Debug.Log("Dzia³a UIManager");
        //QuestFunctionality.TutorialQuestLine();
        uiPrefab.SetActive(true);
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Pause.performed += PauseMenuFunction;
        App.Instance.GameplayCore.GameManager.OnGameWin.AddListener(DisplayWinMessage);
    }


    private void OnDestroy()
    {
        App.Instance.GameplayCore.UnregisterUIManager();
    }

    public void DisplayWinMessage() => winText.gameObject.SetActive(true);
    public void InitializeUpgradeBoard() => upgradeUI.InitializeUpgradeBoard();
    public void DisableUpgradeBoard() => upgradeUI.ClearUpgradeBoard();

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

    private void EnableNewspaper() {
        isNewspaperEnabled = true;
        currentNewspaperUI = Instantiate(newspaperCanvas);  
    }

    public void DisableNewspaper() {
        isNewspaperEnabled = false;
        Destroy(currentNewspaperUI);
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

    public void ChangeNewspaperState()
    {
        if (isNewspaperEnabled)
            DisableNewspaper();
        else
            EnableNewspaper();
    }

    private void UpdateNewspaper()
    {

        if (newsPaper != null)
        {
            newsPaper.UpgradeText("DUPIS£AW");
        }
    }
}
