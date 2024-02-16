using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int maxDeaths;
    [SerializeField] public int bedHealingValue;
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



    private void Awake() 
    { 

        App.Instance.GameplayCore.RegisterGameManager(this);
    }

    private void OnEnable()
    {
        Patient.OnAddSymptom.AddListener(AddedSymptom);
        Patient.OnRemoveSymptom.AddListener(RemovedSymptom);
    }

    private void Start()
    {
        foreach (Symptom symptom in listOfSymptoms)
        {
            listOfAddedSymptoms.Add(symptom, 0);
            listOfRemovedSymptoms.Add(symptom, 0);
        }
    }

    public void AddedSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        listOfAddedSymptoms[symptom] += 1;
        SymptomAddedToDictionary.Invoke(symptom);
    }

    private void RemovedSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        listOfRemovedSymptoms[symptom] += 1;
        SymptomAddedToDictionary.Invoke(symptom);
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