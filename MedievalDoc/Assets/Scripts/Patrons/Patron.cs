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


    [System.Serializable]
    public struct Requirements
    {
        public Symptom symptom;
        [Header("true - add \nfalse - remove")]
        public bool state;
        public int requiredAmmount;
    }



    [System.Serializable]
    public struct QuestList
    {
        public List<QuestScriptableObject> quests;
    }

}
