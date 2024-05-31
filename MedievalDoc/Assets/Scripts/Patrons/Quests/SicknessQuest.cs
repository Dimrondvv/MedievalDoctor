using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SicknessQuest", menuName = "ScriptableObjects/SicknessQuest")]
public class SicknessQuest : ScriptableObject
{
    public string questName;
    public string description;
    public int reward;
    public SicknessQuestType questType;

}
public enum SicknessQuestType
{
    cure,
    cause,
}