using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestScriptableObject", menuName = "ScriptableObjects/Quest", order = 4)]

public class QuestScriptableObject : ScriptableObject
{
    public string questName;
    public int goldReward;
    public int scoreReward;

    public Symptom symptom;
    public QuestAction questAction;
    public int requiredAmmount;
    public int daysToFinish;

}


public enum QuestAction
{
    AddSymptom = 0,
    RemoveSymptom = 1
}