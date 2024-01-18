using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SicknessScriptable", menuName = "ScriptableObjects/Sickness", order = 1)]
public class SicknessScriptableObject : ScriptableObject
{
    public string sicknessName;
    public List<SymptomStruct> symptomList;
    public List<string> stories;
    public List<Symptom> solution;


    [System.Serializable]
    public struct SymptomStruct
    {
        public Symptom symptom;
        public bool isHidden;

        public List<Symptom> symptomsRequiredToCure;

        public string GetSymptomName()
        {
            return symptom.symptomName;
        }
    }

    
    public bool RemoveSymptom(Symptom symptom)
    {
        
        foreach(var item in symptomList)
        {
            if (item.symptom == symptom)
            {
                foreach(var sympt in item.symptomsRequiredToCure) //Check if symptom has required symptoms cured
                {
                    foreach (var smpt in symptomList) 
                    {
                        if (smpt.symptom == sympt)
                        {
                            Debug.Log("Cant be cured");
                            return false;
                        }
                    }
                }
            }
        }
        Debug.Log("Symptom not found");
        return false;
    }
    public bool CheckSymptom(Symptom symptom)
    {
        foreach (var item in symptomList)
        {
            if (item.symptom == symptom)
            {
                Debug.Log($"Symptom found {item.symptom.name}");
                return true;
            }
        }
        Debug.Log("Symptom not found");
        return false;
    }
    
    public void ListSymptoms()
    {

        foreach(SymptomStruct symptom in symptomList)
        {
            Debug.Log(symptom.GetSymptomName());
        }
    }
}
