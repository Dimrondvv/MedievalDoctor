using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Data;
public class GameManager : MonoBehaviour
{
    [SerializeField] int maxDeaths;
    [SerializeField] public int bedHealingValue;
    [SerializeField] public List<Quest> quests;
    [SerializeField] public List<TutorialQuest> tutorialQuests;
    [SerializeField] public int dayOfWin;
    [SerializeField] public List<Tool> starterTools;
    public InteractionLog interactionLog;
    public InteractionLog localInteractionLog;
    public int deathCounter;
    
    private int choosenLevel;
    public int ChoosenLevel
    {
        get { return choosenLevel; }
        set { choosenLevel = value; }
    }

    [SerializeField] private int royalTax;
    public int RoyalTax
    {
        get { return royalTax; }
        set { royalTax = value; }
    }

    private int madCounter;
    public int MadCounter
    {
        get { return madCounter; }
        set { madCounter = value;  }
    }

    private bool isNight;
    public bool IsNight
    {
        get { return isNight; }
        set { isNight = value; }
    }

    private int[] levelStarsCount;
    public int[] LevelStarsCount {
        get { return levelStarsCount; }
        set { LevelStarsCount = value; }
    }

    [SerializeField] private int delayQuestInSeconds;
    public int DelayQuestInSeconds
    {
        get { return delayQuestInSeconds; }
        set { delayQuestInSeconds = value; }
    }

    private Symptom[] listOfSymptoms;

    public Symptom[] ListOfSymptoms
    {
        get { return listOfSymptoms; }
        set { listOfSymptoms = value; }
    }

    private Dictionary<Symptom, int> listOfAddedSymptoms = new Dictionary<Symptom, int>();
    private Dictionary<Symptom, int> listOfRemovedSymptoms = new Dictionary<Symptom, int>();
    public UnityEvent<Symptom> SymptomAddedToDictionary = new UnityEvent<Symptom>(); //Invoked when symptom is added to dictionary
    public UnityEvent OnGameWin = new UnityEvent();
    public UnityEvent OnStarCount = new UnityEvent();
    public UnityEvent OnLevelComplete = new UnityEvent();

    public Dictionary<Symptom, int> ListOfRemovedSymptoms
    {
        get { return listOfRemovedSymptoms; }
        set { listOfRemovedSymptoms = value; }
    }

    public Dictionary<Symptom, int> ListOfAddedSymptoms
    {
        get { return listOfAddedSymptoms; }
        set { listOfAddedSymptoms = value; }
    }

    [SerializeField] private List<Patient> listOfCurrentPatients = new List<Patient>();
    public List<Patient> ListOfCurrentPatients
    {
        get { return listOfCurrentPatients; }
        set { listOfCurrentPatients = value; }
    }


    private void Awake() 
    { 

        App.Instance.GameplayCore.RegisterGameManager(this);
    }

    private void OnEnable()
    {
        Patient.OnAddSymptom.AddListener(AddedSymptom);
        Patient.OnRemoveSymptom.AddListener(RemovedSymptom);
        Patient.OnPatientDeath.AddListener(RemovedPatient);
        Tool.OnToolInteract.AddListener(ToolUsed);
        PickupController.OnInteract.AddListener(ObjectInteracted);
        Patient.OnPatientDeath.AddListener(PatientKilled);
        Patient.OnCureDisease.AddListener(PatientCured);
        OnGameWin.AddListener(EndGame);
        
    }

    private void Start()
    {
        if (App.Instance.GameplayCore.SaveManager != null)
        {
            listOfSymptoms = Data.ImportJsonData.symptomConfig;
            foreach (Symptom symptom in listOfSymptoms)
            {
                listOfAddedSymptoms.Add(symptom, 0);
                listOfRemovedSymptoms.Add(symptom, 0);
            }
        }
        else
            App.Instance.GameplayCore.OnSaveManagerRegistered.AddListener(SetUpListsOfSymptoms);

        localInteractionLog = new InteractionLog();
        levelStarsCount = new int[20]; // Ilosc poziomow;

    }


    private void Update()
    {
        if(QuestFunctionality.currentQuest != null)
        {
            QuestFunctionality.CheckCompletion();
        }
    }

    private void SetUpListsOfSymptoms(SaveManager manager)
    {
        foreach (Symptom symptom in listOfSymptoms)
        {
            listOfAddedSymptoms.Add(symptom, 0);
            listOfRemovedSymptoms.Add(symptom, 0);
        }
    }
    private void RemovedPatient(Patient patient)
    {
        listOfCurrentPatients.Remove(patient);
    }

    private void EndGame()
    {
        App.Instance.GameplayCore.UIManager.DisplayWinMessage();
        Time.timeScale = 0;
    }

    private void AddedSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        if (interactionLog.symptomsCaused.ContainsKey(symptom.symptomName))
        {
            interactionLog.symptomsCaused[symptom.symptomName]++;
            if(localInteractionLog.symptomsCaused.ContainsKey(symptom.symptomName))
                localInteractionLog.symptomsCaused[symptom.symptomName]++;
            else
                localInteractionLog.symptomsCaused.Add(symptom.symptomName, 1);
        }
        else
        {
            interactionLog.symptomsCaused.Add(symptom.symptomName, 1);
            if (localInteractionLog.symptomsCaused.ContainsKey(symptom.symptomName))
                localInteractionLog.symptomsCaused[symptom.symptomName]++;
            else
                localInteractionLog.symptomsCaused.Add(symptom.symptomName, 1);
        }
    }

    private void RemovedSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        if (interactionLog.symptomsCured.ContainsKey(symptom.symptomName))
        {
            interactionLog.symptomsCured[symptom.symptomName]++;
            if (localInteractionLog.symptomsCured.ContainsKey(symptom.symptomName))
                localInteractionLog.symptomsCured[symptom.symptomName]++;
            else
                localInteractionLog.symptomsCured.Add(symptom.symptomName, 1);
        }
        else
        {
            interactionLog.symptomsCured.Add(symptom.symptomName, 1);
            if (localInteractionLog.symptomsCured.ContainsKey(symptom.symptomName))
                localInteractionLog.symptomsCured[symptom.symptomName]++;
            else
                localInteractionLog.symptomsCured.Add(symptom.symptomName, 1);
        }
    }
    private void ToolUsed(GameObject tool, Patient patient)
    {

        if (interactionLog.toolsUsed.ContainsKey(tool.name))
        {
            interactionLog.toolsUsed[tool.name]++;
            if(localInteractionLog.toolsUsed.ContainsKey(tool.name))
                localInteractionLog.toolsUsed[tool.name]++;
            else
                localInteractionLog.toolsUsed.Add(tool.name, 1);
        }
        else
        {
            interactionLog.toolsUsed.Add(tool.name, 1);
            if (localInteractionLog.toolsUsed.ContainsKey(tool.name))
                localInteractionLog.toolsUsed[tool.name]++;
            else
                localInteractionLog.toolsUsed.Add(tool.name, 1);
        }
    }
    private void ObjectInteracted(GameObject obj, PickupController pc)
    {
        if (localInteractionLog.objectsInteracted.ContainsKey(obj.name))
        {
            localInteractionLog.objectsInteracted[obj.name]++;
        }
        else
        {
            localInteractionLog.objectsInteracted.Add(obj.name, 1);
        }
    }
    private void PatientKilled(Patient patient)
    {
        if (localInteractionLog.patientsKilled.ContainsKey(patient.PatientName))
        {
            localInteractionLog.patientsKilled[patient.PatientName]++;
        }
        else
        {
            localInteractionLog.patientsKilled.Add(patient.PatientName, 1);
        }
    }
    private void PatientCured(Patient patient)
    {
        if (localInteractionLog.patientsCured.ContainsKey(patient.PatientName))
        {
            localInteractionLog.patientsCured[patient.PatientName]++;
        }
        else
        {
            localInteractionLog.patientsCured.Add(patient.PatientName, 1);
        }
    }

    private void PatientMad(Patient patient)
    {
        if (localInteractionLog.patientsMad.ContainsKey(patient.PatientName))
        {
            localInteractionLog.patientsMad[patient.PatientName]++;
        } else
        {
            localInteractionLog.patientsMad.Add(patient.PatientName, 1);
        }
    }

    private void OnDestroy()
    {
        App.Instance.GameplayCore.UnregisterGameManager();
    }

    public void CheckDeathCounter()
    {
        if(deathCounter >= maxDeaths){
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}