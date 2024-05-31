using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class InteractionLog
{
    //KEY - NAME | VALUE - COUNT
    [JsonProperty] public Dictionary<string, int> symptomsCured { get; set; }
    [JsonProperty] public Dictionary<string, int> symptomsCaused { get; set; }
    [JsonProperty] public Dictionary<string, int> toolsUsed { get; set; }

    public InteractionLog(Dictionary<string, int> symptomsCured, Dictionary<string, int> symptomsCaused, Dictionary<string, int> toolsUsed)
    {
        this.symptomsCured = symptomsCured;
        this.symptomsCaused = symptomsCaused;
        this.toolsUsed = toolsUsed;
    }
    public InteractionLog()
    {
        symptomsCured = new Dictionary<string, int>();
        symptomsCaused = new Dictionary<string, int>();
        toolsUsed = new Dictionary<string, int>();
    }
}
