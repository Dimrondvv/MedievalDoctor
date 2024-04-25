using System.Collections.Generic;

[System.Serializable]
public struct NotebookData
{
    public bool wasDataInitialized;

    //KEY - discovered thing name || VALUE - discovered thing description
    public Dictionary<string, string> discoveredSicknesses;
    public Dictionary<string, string> discoveredRecipes;
    public Dictionary<string, string> discoveredIngredients;
    public Dictionary<string, string> discoveredTools;
    public Dictionary<string, string> discoveredPatrons;

    //KEY - Name of thing || VALUE - number of interaction times 
    public Dictionary<string, int> sicknessesDiscoveredDuringRun;
    public Dictionary<string, int> recipesDiscoveredDuringRun;
    public Dictionary<string, int> ingredientsDiscoveredDuringRun;
    public Dictionary<string, int> toolsDiscoveredDuringRun;
    public Dictionary<string, int> patronsDiscoveredDuringRun;

    public void InitializeData()
    {
        wasDataInitialized = true;

        discoveredSicknesses = new Dictionary<string, string>();
        discoveredRecipes = new Dictionary<string, string>();
        discoveredIngredients = new Dictionary<string, string>();
        discoveredTools = new Dictionary<string, string>();
        discoveredPatrons = new Dictionary<string, string>();

        toolsDiscoveredDuringRun = new Dictionary<string, int>();
        sicknessesDiscoveredDuringRun = new Dictionary<string, int>();
        recipesDiscoveredDuringRun = new Dictionary<string, int>();
        patronsDiscoveredDuringRun = new Dictionary<string, int>();
    }
}
