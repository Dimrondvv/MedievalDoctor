using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<Tool> possessedTools; // Tools you have
    [SerializeField] private List<Tool> fullToolList;
    [SerializeField] private UpgradeWindow upgradeWindow;
    //private List<Symptom> curableSymptoms; // Symptoms that are curable with your current equipment

    private List<string> fullToolListNames;
    private List<string> possessedToolsNames;
    private List<string> toolP;

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
        if (toolList.Count >= 3)
        {
            toolsToBeChoosed = toolList.OrderBy(x => UnityEngine.Random.value).Take(3).ToList();
        } else
        {
            toolsToBeChoosed = toolList.OrderBy(x => UnityEngine.Random.value).Take(toolList.Count).ToList();
        }

        //Debug.Log(upgradeWindow.PlayerChoice(toolsToBeChoosed, fullToolList));
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
            
            for(int i = 0;i < possessedToolsNames.Count;i++)
            {
                for(int j = 0;j < fullToolListNames.Count;j++)
                {
                    if (possessedToolsNames[i] != fullToolListNames[j])
                    {
                        toolP.Add(fullToolListNames[j]);
                    }
                }
            }
            /*for (int i = 0; i < toolP.Count;i++)
            {
                Debug.Log(toolP[i]);
            }*/
            
            //MakeMeChoose(fullToolListNames.Except(possessedToolsNames).ToList());
        }
    }
}
