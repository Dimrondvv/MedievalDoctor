using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestScriptableObject", menuName = "ScriptableObjects/Quest", order = 4)]

public class QuestScriptableObject : ScriptableObject
{
    public string questName;
    public int goldReward;
    public int scoreReward;
    public int daysToFinish;
    public List<Task> tasks;

    [System.Serializable]
    public struct Task
    {
        public Symptom symptom;
        public QuestAction questAction;
        public int requiredAmmount;
    }



    public bool CheckQuest(Symptom symptom, PatronCharacter patronCharacter, QuestAction action)
    {
        bool reqMet = false;
        int temp = 0;

        for (int i = 0; i < patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks.Count; i++) // check every task for requirements
        {



            if (symptom == patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].symptom
                && action == patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].questAction
                && patronCharacter.ListOfAddedSymptomsForQuest[symptom] >= patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].requiredAmmount)
            {
                Debug.Log("hej");
            }

                //if (patronCharacter.ListOfAddedSymptomsForQuest[symptom] >= patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].requiredAmmount
                //    && symptom == patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].symptom ||
                //    patronCharacter.ListOfRemovedSymptomsForQuest[symptom] >= patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].requiredAmmount
                //    && symptom == patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].symptom
                //    )
                //{
                //    Debug.Log("git");
                //}
                //{
                //    Debug.Log("added gut");
                //    addedReqMet = true;

                //}
                //if (patronCharacter.ListOfRemovedSymptomsForQuest[symptom] >= patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].requiredAmmount
                //    && symptom == patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].symptom)
                //{
                //    Debug.Log("removed gut");
                //    removedReqMet = true;
                //}


            }

        //for (int i = 0; i < patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks.Count; i++)
        //{
        //    if (patronCharacter.QuestChecks[i] == true)
        //    {
        //        temp += 1;
        //    }
        //}

            //for (int i = 0; i < patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks.Count; i++)
            //{
            //    if (patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].reqMet == false)
            //    {
            //        //reqMet = false;
            //    }
            //}

            //if(addedReqMet == true && removedReqMet == true)
            //{
            //    reqMet = true;
            //}


            return reqMet;
    }

}

public enum QuestAction
{
    AddSymptom = 0,
    RemoveSymptom = 1
}