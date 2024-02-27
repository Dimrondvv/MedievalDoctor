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
    [SerializeField] private List<Sprite> craftingSprites;
    [SerializeField] private List<Crafting> craftings;
    [SerializeField] private List<Sprite> plantSprites;
    [SerializeField] private List<ItemChest> plantBoxes;
    [SerializeField] private DayAndNightController dayController;
    [SerializeField] private List<ItemChest> bodyPartsBoxes;
    [SerializeField] private Furniture counter;
    private List<string> fullToolListNames;
    private List<string> possessedToolsNames;
    private List<bool> upgradeCheckList = new List<bool> { false, false, false };

    private void Start() 
    {
        fullToolListNames = fullToolList.Select(o => o.name).ToList();
    }

    public void MakePlant(string plantName)
    {
        if (plantName == "FeverPlant")
        {
            Instantiate(plantBoxes[0], new Vector3(15f, 0f, 19f), Quaternion.identity); // spawn FeverPlantBox
        }
        else if (plantName == "AntidotumPlant")
        {
            Instantiate(plantBoxes[1], new Vector3(15f, 0f, 19f), Quaternion.identity); // spawn AntidotumPlantBox
        }
    }

    public void MakeToolForMe(string toolName)
    {
        for (int i = 0; i < fullToolList.Count; i++)
        {
            if (toolName == fullToolList[i].name)
            {
                Instantiate(counter, new Vector3(8.8f, 0f, 21.9f), Quaternion.identity); //spawn Counter
                Instantiate(fullToolList[i], new Vector3(8.8f, 1f, 21.9f), Quaternion.identity); //spawn Tool
                possessedTools.Add(fullToolList[i]);
                if (toolName == "CuttingSaw")
                {
                    Instantiate(bodyPartsBoxes[0], new Vector3(15f, 0f, 21.9f), Quaternion.identity); //spawn ArmBox
                }
                else if (toolName == "Scalpel")
                {
                    Instantiate(bodyPartsBoxes[1], new Vector3(15f, 0f, 21.9f), Quaternion.identity); //spawn HeartBox
                }
                return;
            }
        }
    }
    
    public void MakeCrafting(string craftingName)
    {
        if (craftingName == "MagicalCrafting")
        {
            Instantiate(craftings[0], new Vector3(16f, 0f, 16f), Quaternion.identity); // spawn MagicalCrafting
        }
        else if (craftingName == "MetalCrafting")
        {
            Instantiate(craftings[1], new Vector3(16f, 0f, 16f), Quaternion.identity); // spawn MetalCrafting
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


    

    private void CraftingUpgrade()
    {
        List<string> craftingNames = new List<string> { "MagicalCrafting", "MetalCrafting" };
        upgradeWindow.PlayerChoice(craftingNames, craftingSprites, "crafting");
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
