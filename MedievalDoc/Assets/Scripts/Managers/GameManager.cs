using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int maxDeaths;
    [SerializeField] public int bedHealingValue;
    [SerializeField] public List<Quest> quests;
    [SerializeField] public List<TutorialQuest> tutorialQuests;
    public InteractionLog interactionLog;
    public InteractionLog localInteractionLog;
    public int deathCounter;
    private int royalTax; // w starcie przypisywana wartoœæ -150 c:
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

    [SerializeField] private int delayQuestInSeconds;
    public int DelayQuestInSeconds
    {
        get { return delayQuestInSeconds; }
        set { delayQuestInSeconds = value; }
    }

    [SerializeField] private List<Symptom> listOfSymptoms;

    public List<Symptom> ListOfSymptoms
    {
        get { return listOfSymptoms; }
        set { listOfSymptoms = value; }
    }

    private Dictionary<Symptom, int> listOfAddedSymptoms = new Dictionary<Symptom, int>();
    private Dictionary<Symptom, int> listOfRemovedSymptoms = new Dictionary<Symptom, int>();
    public UnityEvent<Symptom> SymptomAddedToDictionary = new UnityEvent<Symptom>(); //Invoked when symptom is added to dictionary

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
        
    }

    private void Start()
    {
        royalTax = 150;
        foreach (Symptom symptom in listOfSymptoms)
        {
            listOfAddedSymptoms.Add(symptom, 0);
            listOfRemovedSymptoms.Add(symptom, 0);
        }
        localInteractionLog = new InteractionLog();
    }

    private void Update()
    {
        if(QuestFunctionality.currentQuest != null)
        {
            QuestFunctionality.CheckCompletion();
        }
    }

    private void RemovedPatient(Patient patient)
    {
        listOfCurrentPatients.Remove(patient);
    }


    private void AddedSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        if (interactionLog.symptomsCaused.ContainsKey(symptom.symptomName))
        {
            interactionLog.symptomsCaused[symptom.symptomName]++;
            localInteractionLog.symptomsCaused[symptom.symptomName]++;
        }
        else
        {
            localInteractionLog.symptomsCaused.Add(symptom.symptomName, 1);
        }
    }

    private void RemovedSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        if (interactionLog.symptomsCured.ContainsKey(symptom.symptomName))
        {
            interactionLog.symptomsCured[symptom.symptomName]++;
            localInteractionLog.symptomsCured[symptom.symptomName]++;
        }
        else
        {
            interactionLog.symptomsCured.Add(symptom.symptomName, 1);
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