using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

[System.Serializable]
public class NotebookData
{
    public bool wasDataInitialized { get; set; }

    //KEY - discovered thing name || VALUE - discovered thing data
    [JsonProperty]public Dictionary<string, DiscoveredData> discoveredSicknesses { get; set; }
    [JsonProperty] public Dictionary<string, DiscoveredData> discoveredRecipes { get; set; }
    [JsonProperty] public Dictionary<string, DiscoveredData> discoveredIngredients { get; set; }
    [JsonProperty] public Dictionary<string, DiscoveredData> discoveredTools { get; set; }
    [JsonProperty] public Dictionary<string, DiscoveredData> discoveredPatrons { get; set; }



    //KEY - Name of thing || VALUE - number of interaction times 
    [JsonProperty] public Dictionary<string, int> sicknessesDiscoveredDuringRun { get; set; }
    [JsonProperty] public Dictionary<string, int> recipesDiscoveredDuringRun { get; set; }
    [JsonProperty] public Dictionary<string, int> ingredientsDiscoveredDuringRun { get; set; }
    [JsonProperty] public Dictionary<string, int> toolsDiscoveredDuringRun { get; set; }
    [JsonProperty] public Dictionary<string, int> patronsDiscoveredDuringRun { get; set; }

    public NotebookData()
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

    public NotebookData(bool wasDataInitialized, Dictionary<string, DiscoveredData> discoveredSicknesses, Dictionary<string, DiscoveredData> discoveredRecipes, Dictionary<string, DiscoveredData> discoveredIngredients, Dictionary<string, DiscoveredData> discoveredTools, Dictionary<string, DiscoveredData> discoveredPatrons, Dictionary<string, int> sicknessesDiscoveredDuringRun, Dictionary<string, int> recipesDiscoveredDuringRun, Dictionary<string, int> ingredientsDiscoveredDuringRun, Dictionary<string, int> toolsDiscoveredDuringRun, Dictionary<string, int> patronsDiscoveredDuringRun)
    {
        this.wasDataInitialized = wasDataInitialized;
        this.discoveredSicknesses = discoveredSicknesses;
        this.discoveredRecipes = discoveredRecipes;
        this.discoveredIngredients = discoveredIngredients;
        this.discoveredTools = discoveredTools;
        this.discoveredPatrons = discoveredPatrons;
        this.sicknessesDiscoveredDuringRun = sicknessesDiscoveredDuringRun;
        this.recipesDiscoveredDuringRun = recipesDiscoveredDuringRun;
        this.ingredientsDiscoveredDuringRun = ingredientsDiscoveredDuringRun;
        this.toolsDiscoveredDuringRun = toolsDiscoveredDuringRun;
        this.patronsDiscoveredDuringRun = patronsDiscoveredDuringRun;
    }
}
