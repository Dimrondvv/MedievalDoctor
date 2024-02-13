using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<Tool> possessedTools; // Tools you have
    [SerializeField] private List<Tool> fullToolList;
    [SerializeField] private int numberOfUpgradesOnUpgrade;
    //private List<Symptom> curableSymptoms; // Symptoms that are curable with your current equipment

    private List<string> fullToolListNames;
    private List<string> possessedToolsNames;

    private void MakeToolForMe(string toolName)
    {
        //mo¿e coœ takiego?
        //newtool = Instantiate(GameObject.Find(toolName));
        //i po tym do possessedToolsNames dodaæ newtool'a
        //
    }
    private void MakeMeChoose(List<string> toolList)
    {
        List<string> toolsToBeChoosed;
        //List<Tool> toolPool = fullToolListNames.Except(possessedToolsNames).ToList(); // List of not earned yet items
        if (toolList.Count < numberOfUpgradesOnUpgrade)
        {
            toolsToBeChoosed = toolList.OrderBy(x => UnityEngine.Random.value).Take(numberOfUpgradesOnUpgrade).ToList();
        } else
        {
            toolsToBeChoosed = toolList.OrderBy(x => UnityEngine.Random.value).Take(toolList.Count).ToList();
        }

        // Tu se gracz wybiera np pierwszy element :clueless:
        MakeToolForMe(toolsToBeChoosed[0]);
    }


    private void Start() // i made it up UwU :3 :D :) ;p
    {
        fullToolListNames = fullToolList.Select(o => o.name).ToList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) // https://youtu.be/ujUKcNZ-rK4?si=9zcX_2YXuFKOEOUh
        {
            possessedToolsNames = possessedTools.Select(o => o.name).ToList(); // to do eventu z upgradem wywaliæ
            MakeMeChoose(fullToolListNames.Except(possessedToolsNames).ToList());
        }
    }
}
