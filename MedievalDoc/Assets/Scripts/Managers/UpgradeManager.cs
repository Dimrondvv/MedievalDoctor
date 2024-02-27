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
    //[SerializeField] private List<ItemLayDownPoint> leydownPoints;
    [SerializeField] private List<Sprite> craftingSprites;
    [SerializeField] private List<Crafting> craftings;
    [SerializeField] private List<Sprite> plantSprites;
    [SerializeField] private List<ItemChest> plantBoxes;
    //private List<Symptom> curableSymptoms; // Symptoms that are curable with your current equipment
    
    [SerializeField] private DayAndNightController dayController;
    private List<string> fullToolListNames;
    private List<string> possessedToolsNames;
    private List<bool> upgradeCheckList = new List<bool> { false, false, false };




    private void Start() // i made it up UwU :3 :D :) ;p
    {
        fullToolListNames = fullToolList.Select(o => o.name).ToList();
    }

    public void MakeToolForMe(string toolName)
    {
        for (int i = 0; i < fullToolList.Count; i++)
        {
            if (toolName == fullToolList[i].name)
            {
                Instantiate(fullToolList[i], new Vector3(11.38f, 1f, 21.75f), Quaternion.identity);
                possessedTools.Add(fullToolList[i]);
                return;
            }
        }
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


    public void MakeCrafting(string craftingName) // tak wiem ze tragedia ale trzeba skleic 
    {
        if(craftingName == "MagicalCrafting")
        {
            Instantiate(craftings[0], new Vector3(15f, 0f, 22f), Quaternion.identity);
        }
        else if (craftingName == "MetalCrafting")
        {
            Instantiate(craftings[1], new Vector3(15f, 0f, 22f), Quaternion.identity);
        }
    }

    private void CraftingUpgrade()
    {
        List<string> craftingNames = new List<string> { "MagicalCrafting", "MetalCrafting" };
        upgradeWindow.PlayerChoice(craftingNames, craftingSprites, "crafting");
    }

    public void MakePlant(string plantName) // tak wiem ze tragedia ale trzeba skleic 
    {
        if (plantName == "FeverPlant")
        {
            Instantiate(plantBoxes[0], new Vector3(15f, 0f, 19.5f), Quaternion.identity);
        }
        else if (plantName == "AntidotumPlant")
        {
            Instantiate(plantBoxes[1], new Vector3(15f, 0f, 19.5f), Quaternion.identity);
        }
    }

    private void PlantUpgrade()
    {
        List<string> planboxesNames = new List<string> { "FeverPlant", "AntidotumPlant" };
        upgradeWindow.PlayerChoice(planboxesNames, plantSprites, "plant");
    }

    private void Update() 
    {
        if (dayController.DayCounter == 5 && upgradeCheckList[0] == false)
        {
            upgradeCheckList[0] = true;
            ToolUpgrade();
        }
        if (dayController.DayCounter == 7 && upgradeCheckList[1] == false)
        {
            upgradeCheckList[1] = true;
            CraftingUpgrade();
        }
        if (dayController.DayCounter == 3 && upgradeCheckList[2] == false)
        {
            upgradeCheckList[2] = true;
            PlantUpgrade();
        }
    }
}
