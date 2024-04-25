using System.Collections.Generic;

[System.Serializable]
public struct NotebookData
{
    public bool wasDataInitialized;

    //KEY - discovered thing name || VALUE - discovered thing data
    public Dictionary<string, DiscoveredData> discoveredSicknesses;
    public Dictionary<string, DiscoveredData> discoveredRecipes;
    public Dictionary<string, DiscoveredData> discoveredIngredients;
    public Dictionary<string, DiscoveredData> discoveredTools;
    public Dictionary<string, DiscoveredData> discoveredPatrons;



    //KEY - Name of thing || VALUE - number of interaction times 
    public Dictionary<string, int> sicknessesDiscoveredDuringRun;
    public Dictionary<string, int> recipesDiscoveredDuringRun;
    public Dictionary<string, int> ingredientsDiscoveredDuringRun;
    public Dictionary<string, int> toolsDiscoveredDuringRun;
    public Dictionary<string, int> patronsDiscoveredDuringRun;

    public void InitializeData()
    {
        wasDataInitialized = true;

        discoveredSicknesses = new Dictionary<string, DiscoveredData>();
        discoveredRecipes = new Dictionary<string, DiscoveredData>();
        discoveredIngredients = new Dictionary<string, DiscoveredData>();
        discoveredTools = new Dictionary<string, DiscoveredData>();
        discoveredPatrons = new Dictionary<string, DiscoveredData>();

        toolsDiscoveredDuringRun = new Dictionary<string, int>();
        sicknessesDiscoveredDuringRun = new Dictionary<string, int>();
        recipesDiscoveredDuringRun = new Dictionary<string, int>();
        patronsDiscoveredDuringRun = new Dictionary<string, int>();
    }
}
