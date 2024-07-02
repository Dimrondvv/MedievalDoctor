using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectToolsIntoCounters : MonoBehaviour
{
    [SerializeField] List<GameObject> counters;
    void Start()
    {
        GameManager gameManager = App.Instance.GameplayCore.GameManager;
        for(int i = 0; i < gameManager.starterTools.Count; i++)
        {
            if (i < counters.Count)
            {
                var tool = Instantiate(gameManager.starterTools[i], counters[i].GetComponentInChildren<ItemLayDownPoint>().transform);
                counters[i].GetComponentInChildren<ItemLayDownPoint>().checkIfOccupied = true;
                tool.name = (gameManager.starterTools[i].name).ToString();
            }
            else
                throw new System.Exception("Not enough counters to instantiate all tools");
        }
    }

}
