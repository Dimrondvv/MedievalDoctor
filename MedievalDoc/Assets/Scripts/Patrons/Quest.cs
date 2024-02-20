using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestScriptableObject", menuName = "ScriptableObjects/Quest", order = 4)]

public class QuestScriptableObject : ScriptableObject
{
    public string questName;
    public int goldReward;
    public int scoreReward;
    public int daysToFinish;

    public QuestType type;

    public List<Task> tasks;




    [System.Serializable]
    public struct Task
    {
        public Symptom symptom;
        public QuestAction questAction;
        public int requiredAmmount;
    }

    public bool CheckQuest(Symptom symptom, PatronCharacter patronCharacter)
    {
        bool reqMet = false;
        List<Task> patronCharacterTasks = patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks;
        bool[] reqList = new bool[patronCharacterTasks.Count];

        for (int i = 0; i < patronCharacterTasks.Count; i++)
        {
            reqList[i] = false;
            if (patronCharacterTasks[i].questAction == QuestAction.AddSymptom &&
                patronCharacter.ListOfAddedSymptomsForQuest[patronCharacterTasks[i].symptom] >= patronCharacterTasks[i].requiredAmmount)
            {
                reqList[i] = true;
            }
            if (patronCharacterTasks[i].questAction == QuestAction.RemoveSymptom &&
                patronCharacter.ListOfRemovedSymptomsForQuest[patronCharacterTasks[i].symptom] >= patronCharacterTasks[i].requiredAmmount)
            {
                reqList[i] = true;
            }
        }
        if (reqList.All(x => x))
        {
            reqMet = true;
        }
        return reqMet;
    }
}

public enum QuestType
{
    SymptomQuest = 0,
    PatientQuest = 1
}



public enum QuestAction
{
    AddSymptom = 0,
    RemoveSymptom = 1
}