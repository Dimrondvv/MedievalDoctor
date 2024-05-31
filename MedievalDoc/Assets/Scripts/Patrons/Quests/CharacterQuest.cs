using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterQuest", menuName = "ScriptableObjects/CharacterQuest")]
public class CharacterQuest : ScriptableObject
{
    public string questName;
    public string description;
    public int reward;
    public CharacterQuestType questType;

}
public enum CharacterQuestType
{
    heal,
    kill,
}