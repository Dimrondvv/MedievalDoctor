using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Requirement
{
    public RequirementType type;
    public int count;
    public string name;
    public bool isLocal;

    public bool CheckRequirement()
    {
        InteractionLog log;

        if(type == RequirementType.UpgradesPossesed)
        {
            return count == App.Instance.GameplayCore.UpgradeManager.upgradesPossesed;
        }

        if (!isLocal)
        {
            log = App.Instance.GameplayCore.GameManager.interactionLog;
        }
        else
        {
            log = App.Instance.GameplayCore.GameManager.localInteractionLog;
        }
        List<Dictionary<string, int>> interactions = new List<Dictionary<string, int>>();
        interactions.Add(log.symptomsCured);
        interactions.Add(log.symptomsCaused);
        interactions.Add(log.toolsUsed);
        interactions.Add(log.patientsCured);
        interactions.Add(log.patientsKilled);
        interactions.Add(log.objectsInteracted);

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
    ToolUsed = 2,
    PatientCured = 3,
    PatientKilled = 4,
    ObjectInteracted = 5,
    UpgradesPossesed = 6
};
