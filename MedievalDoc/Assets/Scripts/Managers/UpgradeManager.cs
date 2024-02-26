using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<Tool> possessedTools; // Tools you have
    [SerializeField] private List<Tool> fullToolList;
    [SerializeField] private UpgradeWindow upgradeWindow;
    [SerializeField] private List<ItemLayDownPoint> leydownPoints;
    [SerializeField] private Sprite potka; // temp potka dla sprajtów których nie ma
    //private List<Symptom> curableSymptoms; // Symptoms that are curable with your current equipment
    private List<string> fullToolListNames;
    private List<string> possessedToolsNames;
    public void MakeToolForMe(string toolName)
    {
        for (int i = 0; i < fullToolList.Count; i++)
        {
            if (toolName == fullToolList[i].name)
            {
                Debug.Log("co jest");
                Instantiate(fullToolList[i], new Vector3(11.38f, 1f, 21.75f), Quaternion.identity);
                possessedTools.Add(fullToolList[i]);
                return;
            }
        }
    }



    private void Start() // i made it up UwU :3 :D :) ;p
    {
        fullToolListNames = fullToolList.Select(o => o.name).ToList();
    }

    private void ToolUpgrade() // https://youtu.be/ujUKcNZ-rK4?si=9zcX_2YXuFKOEOUh
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
        List<string> toolsToBeChoosed;
        if (fullToolListNames.Count >= 3)
        {
            toolsToBeChoosed = fullToolListNames.OrderBy(x => UnityEngine.Random.value).Take(3).ToList();
        }
        else
        {
            toolsToBeChoosed = fullToolListNames.OrderBy(x => UnityEngine.Random.value).Take(fullToolListNames.Count).ToList();
        }
        List<Sprite> imageList = new List<Sprite>();
        for (int i = 0; i < toolsToBeChoosed.Count; i++)
        {
            for (int j = 0; j < fullToolList.Count; j++)
            {
                if (toolsToBeChoosed[i] == fullToolList[j].name)
                {
                    imageList.Add(fullToolList[j].ItemIcon);
                }
            }
        }
        upgradeWindow.PlayerChoice(toolsToBeChoosed, imageList, "tool");
    }

    private void CraftingUpgrade()
    {

    }

    private void Update() // to do eventu z upgradem wywaliæ
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            //ToolUpgrade(); // bo¿e coœ polskê odpalaj to tylko raz bo olaboga
            CraftingUpgrade();
        }
    }
}
