using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
public class HelperFunctions
{
    public static Symptom SymptomLookup(string symptomKey) //Find a symptom using it's key
    {
        foreach(Symptom symptom in ImportJsonData.symptomConfig)
        {
            if(symptomKey == symptom.symptomID)
                return symptom;
        }

        return null;
    }

    public static Sickness SicknessLookup(string sicknessKey) { //Find a sickness using it's key
        foreach (Sickness sickness in ImportJsonData.sicknessConfig) {
            if (sicknessKey == sickness.sicknessID)
                return sickness;
        }

        return null;
    }

    public static ItemChanger ChangerLookup(string changerID)
    {
        foreach(ItemChanger changer in ImportJsonData.itemChangers)
        {
            if(changerID == changer.changerID)
                return changer;
        }

        return null;
    }

    public static SicknessContainers SicknessContainersLookup(string sickContainersKey) { //Find a sickContainer using it's key
        foreach (SicknessContainers sicknessContainers in ImportJsonData.sicknessContainersConfig)
        {
            if (sickContainersKey.ToLower() == sicknessContainers.key)
                return sicknessContainers;
            }       

        return null;
    }

    public static ToolChest ToolChestLookup(string toolChestKey)
    {
        foreach (ToolChest toolChest in ImportJsonData.toolChestConfig)
        {
            if (toolChest.chestID == toolChestKey)
            {
                return toolChest;
            }
        }

        return null;
    }

    public static GameObject ChestItemLookup(string chestName) 
    { 
        foreach(ToolChest toolChest in ImportJsonData.toolChestConfig)
        {
            if(toolChest.chestID == chestName)
            {
                return Resources.Load<GameObject>("Tools/" + ToolLookup(toolChest.toolID).toolPrefab);
            }
        }
        foreach (Data.ItemChest chest in ImportJsonData.itemChests)
        {
            if (chest.chestID == chestName)
            {
                return Resources.Load<GameObject>("Items/" + chest.itemID);
            }
        }

        Debug.LogError($"Prefab {chestName} is named wrong or item/took ID doesn't exist");
        return null;
    }

    public static CraftingTable CraftingLookup(string craftingKey)
    {
        foreach (CraftingTable crafting in ImportJsonData.craftingTables)
        {
            if (craftingKey == crafting.craftingID)
                return crafting;
        }

        return null;
    }
    public static Recipes RecipeLookup(string recipeKey)
    {
        foreach(Recipes recipe in ImportJsonData.recipes)
        {
            try
            {
                if (recipeKey == recipe.recipeID)
                    return recipe;
            }
            catch
            {
                Debug.LogError($"Error on Recipe key: {recipeKey} RecipeID: {recipe.recipeID}");

            }
        }

        return null;
    }
    public static Data.Description.Sicknesses SicknessDescriptionLookup(string descriptionKey)
    {
        foreach(Data.Description.Sicknesses sicknessDescription in ImportJsonData.sicknessDescriptions)
        {
            if (sicknessDescription.sicknessDescription == descriptionKey)
                return sicknessDescription;
        }

        return null;
    }
    public static Data.Tool ToolLookup(GameObject toolObject)
    {
        foreach (Data.Tool tool in ImportJsonData.toolConfig)
        {
            if (toolObject.name == tool.toolPrefab)
                return tool;
        }

        return null;
    }
    public static Data.Tool ToolLookup(string toolID)
    {
        foreach (Data.Tool tool in ImportJsonData.toolConfig)
        {
            if (toolID == tool.toolID)
                return tool;
        }

        return null;
    }
    public static bool CanSymptomBeRemoved(Symptom symptom, Patient patient)
    {
        SymptomDependencies dependency = new SymptomDependencies();

        foreach(var dependencyIterator in ImportJsonData.symptomDependenciesConfig)
        {
            if(symptom.symptomID == dependencyIterator.symptomID)
                dependency = dependencyIterator;
        }
        if(dependency.symptomID == null)
        {
            Debug.LogWarning($"Cannon find dependencies for symptom: {symptom.symptomName}");
            return true;
        }
        if(dependency.symptomsRequiredToRemove != null)
        {
            foreach(var symptomReq in dependency.symptomsRequiredToRemove)
            {
                if (!patient.symptoms.Contains(SymptomLookup(symptomReq)))
                    return false;
            }
        }
        if(dependency.symptomsBlockingRemove != null)
        {
            foreach (var symptomReq in dependency.symptomsBlockingRemove)
            {
                if (patient.symptoms.Contains(SymptomLookup(symptomReq)))
                    return false;
            }
        }

        return true;
    }
    public static bool CanSymptomBeAdded(Symptom symptom, Patient patient)
    {
        SymptomDependencies dependency = new SymptomDependencies();


        foreach (var dependencyIterator in ImportJsonData.symptomDependenciesConfig)
        {
            if (dependencyIterator == null)
                return true;
            if (symptom.symptomID == dependencyIterator.symptomID)
                dependency = dependencyIterator;
        }
        if (dependency.symptomID == null)
        {
            Debug.LogWarning($"Cannon find dependencies for symptom: {symptom.symptomName}");
            return true;
        }
        if (dependency.symptomsRequiredToAdd != null)
        {
            foreach (var symptomReq in dependency.symptomsRequiredToAdd)
            {
                if (!patient.symptoms.Contains(SymptomLookup(symptomReq)))
                    return false;
            }
        }
        if (dependency.symptomsBlockingAdd != null)
        {
            foreach (var symptomReq in dependency.symptomsBlockingAdd)
            {
                if (patient.symptoms.Contains(SymptomLookup(symptomReq)))
                    return false;
            }
        }

        return true;
    }

    public static Symptom[] GetSymptomsAddedOnRemove(Symptom symptom)
    {
        SymptomDependencies dependency = new SymptomDependencies();
        foreach (var dependencyIterator in ImportJsonData.symptomDependenciesConfig)
        {
            if (symptom.symptomID == dependencyIterator.symptomID)
                dependency = dependencyIterator;
        }
        if (dependency.symptomsAddOnRemove == null)
        {
            Debug.LogWarning($"No symptoms added on remove for symptom: {symptom.symptomName}");
            return null;
        }
        Symptom[] addOnRemoveArray = new Symptom[dependency.symptomsAddOnRemove.Length];
        for(int i = 0; i < dependency.symptomsAddOnRemove.Length; i++)
        {
            addOnRemoveArray[i] = SymptomLookup(dependency.symptomsAddOnRemove[i]);
        }
        return addOnRemoveArray;
    }
}
