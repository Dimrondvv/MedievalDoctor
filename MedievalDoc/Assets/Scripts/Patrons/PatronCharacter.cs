using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronCharacter : MonoBehaviour
{
    [SerializeField] GameObject patron;
    private int questID;
    private bool isQuestActive=false;
    public bool IsQuestActive
    {
        get { return isQuestActive; }
        set { isQuestActive = value; }
    }
    private PatronScriptableObject patronType;
    public PatronScriptableObject PatronType
    {
        get { return patronType; }
        set { patronType = value; }
    }
    private string stateTXT="";
    public string StateTXT
    {
        get { return stateTXT; }
        set { stateTXT = value; }
    }



    private void Awake()
    {
        patron.GetComponent<BoxCollider>().enabled = true;
        patron.GetComponent<MeshRenderer>().enabled = true;
        patron.GetComponent<MeshRenderer>().material = patronType.color;

        RandomizeQuest();
    }

    private void RandomizeQuest()
    {
        questID = Random.Range(0, patronType.questList.Count);
        isQuestActive = true;

        if (patronType.questList[questID].state)
        {
            stateTXT = "Remove";
        }
        else
        {
            stateTXT = "Add";
        }

    }

}
