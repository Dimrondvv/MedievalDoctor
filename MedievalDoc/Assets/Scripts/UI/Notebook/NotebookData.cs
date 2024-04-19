using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NotebookData
{
    //KEY - discovered thing name || VALUE - discovered thing description
    public Dictionary<string, string> discoveredSicknesses;
    public Dictionary<string, string> discoveredRecipes;
    public Dictionary<string, string> discoveredIngredients;
    public Dictionary<string, string> discoveredTools;

    //KEY - Name of thing || VALUE - number of interaction times 
    public Dictionary<string, int> sicknessesDiscoveredDuringRun;
    public Dictionary<string, int> recipesDiscoveredDuringRun;
    public Dictionary<string, int> ingredientsDiscoveredDuringRun;
    public Dictionary<string, int> toolsDiscoveredDuringRun;


    public NotebookData(int par = 1)
    {
        discoveredSicknesses = new Dictionary<string, string>();
        discoveredRecipes = new Dictionary<string, string>();
        discoveredIngredients = new Dictionary<string, string>();
        discoveredTools = new Dictionary<string, string>();

        toolsDiscoveredDuringRun = new Dictionary<string, int>();
        sicknessesDiscoveredDuringRun = new Dictionary<string, int>();
        recipesDiscoveredDuringRun = new Dictionary<string, int>();
        ingredientsDiscoveredDuringRun = new Dictionary<string, int>();
    }
}
