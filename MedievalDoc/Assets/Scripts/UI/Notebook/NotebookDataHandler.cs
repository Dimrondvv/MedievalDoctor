using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookDataHandler : MonoBehaviour
{
    [SerializeField] string notebookDataFileName;
    [SerializeField] int interactionsRequired; //Field to see how many interactions are required with something to add it to discovered
    NotebookData data;
    SaveManager saveManager;
    // Start is called before the first frame update
    void Start()
    {
        saveManager = App.Instance.GameplayCore.SaveManager;
        if(saveManager == null) //Wait for save manager to register if its null
        {
            App.Instance.GameplayCore.OnSaveManagerRegistered.AddListener(WaitForManagerRegister);
        }
        else
        {
            LoadNotebookData();
        }
        AddEventListeners();
        
    }
    private void OnDestroy()
    {
        SaveNotebookData();
    }
    private void WaitForManagerRegister(SaveManager mng)
    {
        saveManager = mng;

       
        LoadNotebookData();
    }
    private void LoadNotebookData()
    {
        try
        {
            data = saveManager.LoadGameData<NotebookDataRootObject>(notebookDataFileName + ".json").ntData;
        }
        catch
        {
            Debug.Log("Creating new instance of NotebookData");
            data = new NotebookData();
        }
    }

    private void AddEventListeners()
    {
        Patient.OnCureDisease.AddListener(AddCuredSicknessToDict);
        Tool.OnToolInteract.AddListener(AddDiscoveredToolToDict);
        Crafting.OnCraftingCompleted.AddListener(AddDiscoveredRecipeToDict);
        PatronsManager.OnPatronSpawned.AddListener(AddDiscoveredPatronToDict);
    }

    private void SaveNotebookData()
    {
        if (!data.wasDataInitialized)
            data.wasDataInitialized = true;
        saveManager.SaveGameData<NotebookDataRootObject>(new NotebookDataRootObject { ntData = data }, notebookDataFileName + ".json");
    }

    private void AddCuredSicknessToDict(Patient curedPatient)
    {
        string sicknessName = curedPatient.Sickness.sicknessName;
        string sicknessDecsription = curedPatient.Sickness.sicknessDescritpion;
        
        if (data.discoveredSicknesses.ContainsKey(sicknessName)) //If sickness is already discovered return
            return;

        DiscoveredData sicknessData = new DiscoveredData(sicknessName, sicknessDecsription, "avx");

        if (data.sicknessesDiscoveredDuringRun.ContainsKey(sicknessName))
        {
            data.sicknessesDiscoveredDuringRun[sicknessName]++; //Increase count of sickness discovered during run, if the count discovered is higher than required add it to discovered permanently and remove from discovered during run
            
            if(data.sicknessesDiscoveredDuringRun[sicknessName] >= interactionsRequired)
            {
                data.discoveredSicknesses.Add(sicknessName, sicknessData);
                data.sicknessesDiscoveredDuringRun.Remove(sicknessName);
            }
        }
        else //If sickness not present in discovered during run add it to it
        {
            data.sicknessesDiscoveredDuringRun.Add(sicknessName, 1);

            if (data.sicknessesDiscoveredDuringRun[sicknessName] >= interactionsRequired)
            {
                data.discoveredSicknesses.Add(sicknessName, sicknessData);
                data.sicknessesDiscoveredDuringRun.Remove(sicknessName);
            }
        }
    }
    private void AddDiscoveredToolToDict(GameObject tool, Patient patient)
    {
        Tool toolUsed = tool.GetComponent<Tool>();
        string toolName = toolUsed.ToolName;
        string toolDescription = toolUsed.ToolDescription;
        Sprite toolIcon = toolUsed.ItemIcon;
        if (data.discoveredTools.ContainsKey(toolName)) //Return if tool already discovered
            return;

        DiscoveredData toolData = new DiscoveredData(toolName, toolDescription, toolIcon.name);
        

        if (data.toolsDiscoveredDuringRun.ContainsKey(toolName)) //Same as in sickness
        {
            data.toolsDiscoveredDuringRun[toolName]++;

            if (data.toolsDiscoveredDuringRun[toolName] >= interactionsRequired)
            {
                data.discoveredTools.Add(toolName, toolData);
                data.toolsDiscoveredDuringRun.Remove(toolName);
            }
        }
        else
        {
            data.toolsDiscoveredDuringRun.Add(toolName, 1);

            if (data.toolsDiscoveredDuringRun[toolName] >= interactionsRequired)
            {
                data.discoveredTools.Add(toolName, toolData);
                data.toolsDiscoveredDuringRun.Remove(toolName);
            }
        }
    }
    private void AddDiscoveredRecipeToDict(Recipe recipe)
    {
        string recipeName = recipe.recipeName;
        string recipeDescription = recipe.recipeDescription;
        if (data.discoveredRecipes.ContainsKey(recipeName)) //Return if recipe already discovered
            return;

        DiscoveredData recipesData = new DiscoveredData(recipeName, recipeDescription, "axc"); 
        

        if (data.recipesDiscoveredDuringRun.ContainsKey(recipeName)) //Same as in sickness
        {
            data.recipesDiscoveredDuringRun[recipeName]++;

            if (data.recipesDiscoveredDuringRun[recipeName] >= interactionsRequired)
            {
                data.discoveredRecipes.Add(recipeName, recipesData);
                data.recipesDiscoveredDuringRun.Remove(recipeName);
            }
        }
        else
        {
            data.recipesDiscoveredDuringRun.Add(recipeName, 1);

            if (data.recipesDiscoveredDuringRun[recipeName] >= interactionsRequired)
            {
                data.discoveredRecipes.Add(recipeName, recipesData);
                data.recipesDiscoveredDuringRun.Remove(recipeName);
            }
        }
    }
    private void AddDiscoveredPatronToDict(PatronScriptableObject patron)
    {
        string patronName = patron.patronName;
        string patronDescription = patron.patronDescription;
        if (data.discoveredPatrons.ContainsKey(patronName)) //Return if recipe already discovered
            return;

        DiscoveredData patronData = new DiscoveredData(patronName, patronDescription, "acs");

        if (data.patronsDiscoveredDuringRun.ContainsKey(patronName)) //Same as in sickness
        {
            data.patronsDiscoveredDuringRun[patronName]++;

            if (data.patronsDiscoveredDuringRun[patronName] >= interactionsRequired)
            {
                data.discoveredPatrons.Add(patronName, patronData);
                data.patronsDiscoveredDuringRun.Remove(patronName);
            }
        }
        else
        {
            data.patronsDiscoveredDuringRun.Add(patronName, 1);

            if (data.patronsDiscoveredDuringRun[patronName] >= interactionsRequired)
            {
                data.discoveredPatrons.Add(patronName, patronData);
                data.patronsDiscoveredDuringRun.Remove(patronName);
            }
        }
    }
}
