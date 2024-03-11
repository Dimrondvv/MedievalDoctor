using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PatronScriptableObject", menuName = "ScriptableObjects/Patron", order = 3)]

public class PatronScriptableObject : ScriptableObject
{
    public string patronName;
    public Mesh prefab;
    public List<Requirements> requirementsToSpawn;
    public List<QuestScriptableObject> questList;

    public bool CheckReq(GameManager gameManager)
    {
        bool reqMet = false;
        bool[] reqList = new bool[requirementsToSpawn.Count];

        for (int i = 0; i < requirementsToSpawn.Count; i++)
        {
            reqList[i] = false;
            if (requirementsToSpawn[i].questAction == QuestAction.AddSymptom &&
                gameManager.ListOfAddedSymptoms[requirementsToSpawn[i].symptom] >= requirementsToSpawn[i].requiredAmmount)
            {
                reqList[i] = true;
            }
            if (requirementsToSpawn[i].questAction == QuestAction.RemoveSymptom &&
                gameManager.ListOfRemovedSymptoms[requirementsToSpawn[i].symptom] >= requirementsToSpawn[i].requiredAmmount)
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
    [System.Serializable]
    public struct Requirements
    {
        public Symptom symptom;
        public QuestAction questAction;
        public int requiredAmmount;
    }

}
