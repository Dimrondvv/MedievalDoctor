using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int maxDeaths;
    [SerializeField] public int bedHealingValue;
    public InteractionLog interactionLog;
    public InteractionLog localInteractionLog;
    public int deathCounter;
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
        
    }

    private void Start()
    {
        foreach (Symptom symptom in listOfSymptoms)
        {
            listOfAddedSymptoms.Add(symptom, 0);
            listOfRemovedSymptoms.Add(symptom, 0);
        }
        localInteractionLog = new InteractionLog();
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
            localInteractionLog.toolsUsed[tool.name]++;
        }
        else
        {
            interactionLog.toolsUsed.Add(tool.name, 1);
            localInteractionLog.toolsUsed.Add(tool.name, 1);
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