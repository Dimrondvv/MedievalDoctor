using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Requirement
{
    public RequirementType type;
    public int count;
    public string name;

    public bool CheckRequirement()
    {
        InteractionLog log = App.Instance.GameplayCore.GameManager.interactionLog;
        List<Dictionary<string, int>> interactions = new List<Dictionary<string, int>>();
        interactions.Add(log.symptomsCured);
        interactions.Add(log.symptomsCaused);
        interactions.Add(log.toolsUsed);

        if (!interactions[(int)type].ContainsKey(name))
            return false;

        if (interactions[(int)type][name] >= count)
            return true;
        return false;
    }
}


public enum RequirementType
{
    SymptomCured = 0,
    SymptomCaused = 1,
    ToolUsed = 2
};