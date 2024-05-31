using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SicknessQuest", menuName = "ScriptableObjects/SicknessQuest")]
public class SicknessQuest : Quest
{
    public SicknessQuestType questType;
}
public enum SicknessQuestType
{
    cure,
    cause,
}