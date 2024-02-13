using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronCharacter : MonoBehaviour
{
    [SerializeField] private GameObject patron;
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

    private void Start()
    {
        RandomizeQuest();
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
        Debug.Log("gdfshd");
    }

    private void RandomizeQuest()
    {
        questID = Random.Range(0, patronType.questList.Count);

        foreach (Symptom symptom in gameManager.ListOfSymptoms)
        {
            listOfAddedSymptomsForQuest[symptom] = 0;
            listOfRemovedSymptomsForQuest[symptom] = 0;
        }

        isQuestActive = true;
    }
    private void AddedSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        listOfAddedSymptomsForQuest[symptom] += 1;
        Debug.Log(listOfAddedSymptomsForQuest[symptom]);
        CheckQuest(symptom);
    }

    private void RemovedSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        listOfRemovedSymptomsForQuest[symptom] += 1;
        Debug.Log(listOfRemovedSymptomsForQuest[symptom]);
        CheckQuest(symptom);
    }

    private void CheckQuest(Symptom symptom)
    {

    }

}
