using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class PatronCharacter : MonoBehaviour
{
    [SerializeField] private GameObject patron;
    [SerializeField] private DayAndNightController dayController;
    [SerializeField] private SpawnPatientTimer spawnPatientTimer;

    private int questID;
    public int QuestID
    {
        get { return questID; }
        set { questID = value; }
    }
    GameManager gameManager;


    private bool isQuestActive=false;
    public bool IsQuestActive
    {
        get { return isQuestActive; }
        set { isQuestActive = value; }
    }
    private PatronScriptableObject patronType;
    public PatronScriptableObject PatronType
    {
        get { return patronType; }
        set { patronType = value; }
    }

    private Dictionary<Symptom, int> listOfAddedSymptomsForQuest = new Dictionary<Symptom, int>();
    public Dictionary<Symptom, int> ListOfAddedSymptomsForQuest
    {
        get { return listOfAddedSymptomsForQuest; }
        set { listOfAddedSymptomsForQuest = value; }
    }

    private Dictionary<Symptom, int> listOfRemovedSymptomsForQuest = new Dictionary<Symptom, int>();
    public Dictionary<Symptom, int> ListOfRemovedSymptomsForQuest
    {
        get { return listOfRemovedSymptomsForQuest; }
        set { listOfRemovedSymptomsForQuest = value; }
    }

    private int deadLineDay;
    public int DeadLineDay
    {
        get { return deadLineDay; }
        set { deadLineDay = value; }
    }

    private int deadLineHour;
    public int DeadLineHour
    {
        get { return deadLineHour; }
        set { deadLineHour = value; }
    }

    private Patient killThisPatient;
    public Patient KillThisPatient
    {
        get { return killThisPatient; }
        set { killThisPatient = value; }
    }


    private void Start()
    {
        //RandomizeQuest();
        StartCoroutine(DelayBetweenQuests());
    }

    private void Update()
    {
        
    }


    private void OnEnable()
    {
        if (App.Instance.GameplayCore.GameManager != null)
        {
            gameManager = App.Instance.GameplayCore.GameManager;
        }
        // copy of dictionaries that are empty
        foreach (Symptom symptom in gameManager.ListOfSymptoms)
        {
            listOfAddedSymptomsForQuest.Add(symptom, 0);
            listOfRemovedSymptomsForQuest.Add(symptom, 0);
        }
        Patient.OnAddSymptom.AddListener(AddedSymptom);
        Patient.OnRemoveSymptom.AddListener(RemovedSymptom);
        Patient.OnPatientDeath.AddListener(PatientDeath);
    }

    private void RandomizeQuest()
    {
        Debug.Log("randomizing quest");
        questID = Random.Range(0, patronType.questList.Count);
        deadLineDay = patronType.questList[questID].daysToFinish + dayController.CurrentTime.Day;
        deadLineHour = dayController.CurrentTime.Hour;
        foreach (Symptom symptom in gameManager.ListOfSymptoms)
        {
            listOfAddedSymptomsForQuest[symptom] = 0;
            listOfRemovedSymptomsForQuest[symptom] = 0;
        }
        if (patronType.questList[questID].type == QuestType.SymptomQuest)
        {
            Debug.Log("Symptom quest");
        }
        if (patronType.questList[questID].type == QuestType.PatientQuest)
        {
            killThisPatient = spawnPatientTimer.TrySpawning();
            Debug.Log("Kill this dude:" + killThisPatient.PatientName);
            Debug.Log("Patient quest");
        }

        isQuestActive = true;
    }

    public void DisableQuest()
    {
        IsQuestActive = false;
        StartCoroutine(DelayBetweenQuests());
    }

    private void PatientDeath(Patient patient)
    {
        if(isQuestActive)
        {
            if(patient == killThisPatient)
            {
                Debug.Log("dedek");
                RewardForQuest();
            }
        }
    }

    private void AddedSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        listOfAddedSymptomsForQuest[symptom] += 1;
        if (isQuestActive) { 
            CheckQuest(symptom, QuestAction.AddSymptom);
        }
    }

    private void RemovedSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        listOfRemovedSymptomsForQuest[symptom] += 1;
        if (isQuestActive)
        {
            CheckQuest(symptom, QuestAction.RemoveSymptom);
        }
    }

    private void CheckQuest(Symptom symptom, QuestAction action)
    {
        if(patronType.questList[questID].CheckQuest(symptom, this) && isQuestActive == true)
        {
            RewardForQuest();
        }
    }

    private void RewardForQuest()
    {
        PlayerManager playerManager = App.Instance.GameplayCore.PlayerManager;
        playerManager.Score += patronType.questList[questID].scoreReward;
        playerManager.Money += patronType.questList[questID].goldReward;
        isQuestActive = false;
        StartCoroutine(DelayBetweenQuests());
    }

    public IEnumerator DelayBetweenQuests()
    {
        yield return new WaitForSeconds(gameManager.DelayQuestInSeconds);
        if (!App.Instance.GameplayCore.GameManager.IsNight) { RandomizeQuest(); }
    }

}
