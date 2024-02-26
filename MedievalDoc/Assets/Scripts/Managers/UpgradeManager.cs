using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<Tool> possessedTools; // Tools you have
    [SerializeField] private List<Tool> fullToolList;
    [SerializeField] private UpgradeWindow upgradeWindow;
    [SerializeField] private List<ItemLayDownPoint> counterList;
    //private List<Symptom> curableSymptoms; // Symptoms that are curable with your current equipment
    private List<string> fullToolListNames;
    private List<string> possessedToolsNames;

    public void MakeToolForMe(string toolName)
    {
        
        for (int i = 0; i < fullToolList.Count; i++)
        {
            if (toolName == fullToolList[i].name)
            {
                for( int j = 0; j < counterList.Count; j++)
                {
                    if (!counterList[j].checkIfOccupied)
                    {
                        Instantiate(fullToolList[i], counterList[j].transform.position, Quaternion.identity);
                        possessedTools.Add(fullToolList[i]);
                        return;
                    }
                }
                
            }
        }
        //ListOfSymptoms(); // tu aktualizuj liste curable symptomów
    }

    /*private void ListOfSymptoms()
    {
        for(int i = 0; i < possessedTools.Count; i++)
        {
            for(int j  = 0; j < possessedTools[i].SymptomsRemoved.Count; j++)
            {
                Debug.Log(possessedTools[i].SymptomsRemoved[j]);
                curableSymptoms.Add(possessedTools[i].SymptomsRemoved[j]);
                if (!curableSymptoms.Contains(possessedTools[i].SymptomsRemoved[j]))
                {
                    
                    Debug.Log(curableSymptoms.Count);
                }
            }
        }
        //curableSymptoms = curableSymptoms.Distinct().ToList();
    }*/

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
        Time.timeScale = 0f;
        upgradeWindow.PlayerChoice(toolsToBeChoosed, fullToolList);
    }


    private void Start() // i made it up UwU :3 :D :) ;p
    {
        fullToolListNames = fullToolList.Select(o => o.name).ToList();
    }

    private void UpgradeMyClinic() // https://youtu.be/ujUKcNZ-rK4?si=9zcX_2YXuFKOEOUh
    {
        possessedToolsNames = possessedTools.Select(o => o.name).ToList();

        for (int i = 0; i < possessedToolsNames.Count; i++)
        {
            for (int j = 0; j < fullToolListNames.Count; j++)
            {
                if (possessedToolsNames[i] == fullToolListNames[j])
                {
                    fullToolListNames.RemoveAt(j);
                }
            }
        }
        MakeMeChoose(fullToolListNames);
    }
    private void Update() // to do eventu z upgradem wywaliæ
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UpgradeMyClinic();
        }
    }
}
