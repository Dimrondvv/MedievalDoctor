using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestScriptableObject", menuName = "ScriptableObjects/Quest", order = 4)]

public class QuestScriptableObject : ScriptableObject
{
    public string questName;
    public int reward;

    public Symptom symptom;
    [Header("true - add \nfalse - remove")]
    public bool state;
    public int requiredAmmount;
    public int daysToFinish;

}
