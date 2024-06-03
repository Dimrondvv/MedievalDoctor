using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterQuest", menuName = "ScriptableObjects/CharacterQuest")]
public class CharacterQuest : Quest
{
    public CharacterQuestType questType;
    
}
public enum CharacterQuestType
{
    heal = 0,
    kill =  1
}