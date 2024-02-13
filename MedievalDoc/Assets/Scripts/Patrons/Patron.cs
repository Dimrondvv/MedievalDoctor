using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatronScriptableObject", menuName = "ScriptableObjects/Patron", order = 3)]

public class PatronScriptableObject : ScriptableObject
{
    public string patronName;
    public Material color;
    public List<Requirements> requirementsToSpawn;
    public List<QuestScriptableObject> questList;
   
    public bool CheckReq(Symptom symptom, GameManager gameManager)
    {
        bool reqMet = false;
        for(int i=0; i<requirementsToSpawn.Count; i++)
        {
            if (gameManager.ListOfAddedSymptoms[symptom] == requirementsToSpawn[i].requiredAmmount && requirementsToSpawn[i].symptom == symptom && requirementsToSpawn[i].questAction == QuestAction.AddSymptom)
            {
                reqMet = true;
            }
            if (gameManager.ListOfRemovedSymptoms[symptom] == requirementsToSpawn[i].requiredAmmount && requirementsToSpawn[i].symptom == symptom && requirementsToSpawn[i].questAction == QuestAction.RemoveSymptom)
            {
                reqMet = true;
            }
        }
        return reqMet;
    }
    [System.Serializable]
    public struct Requirements
    {
        public Symptom symptom;
        public QuestAction questAction;
        public int requiredAmmount;
    }

    [System.Serializable]
    public struct QuestList
    {
        public List<QuestScriptableObject> quests;
    }

}
