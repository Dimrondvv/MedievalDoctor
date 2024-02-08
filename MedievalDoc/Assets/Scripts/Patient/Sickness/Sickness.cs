using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SicknessScriptable", menuName = "ScriptableObjects/Sickness", order = 1)]
public class SicknessScriptableObject : ScriptableObject
{
    public string sicknessName;
    public List<SymptomStruct> symptomList;
    public List<string> stories;
    public List<SolutionStruct> solutionList;


    [System.Serializable]
    public struct SymptomStruct
    {
        public Symptom symptom;
        public bool isHidden;


        public string GetSymptomName()
        {
            return symptom.symptomName;
        }
    }

    [System.Serializable]
    public struct SolutionStruct
    {
        public Symptom symptom;
        public List<Symptom> symptomsNotPresentToCure;
        public List<Symptom> symptomsPresentToCure;
    }

    public void RemSymptom(Symptom symptom)
    {
        foreach(var item in symptomList)
        {
            if(item.symptom == symptom)
            {
                symptomList.Remove(item);
                return;
            }
        }
    }

    public bool RemoveSymptom(Symptom symptom)
    {
        foreach(var item in solutionList)
        {
            if (item.symptom == symptom && CheckSymptom(symptom))
            {
                bool canBeCured = true;
                foreach(var sympt in item.symptomsNotPresentToCure) //Check if symptom has required symptoms cured
                {
                    foreach (var smpt in symptomList) 
                    {
                        if (smpt.symptom == sympt)
                        {
                            return false;
                        }
                    }
                }
                
                foreach(var sympt in item.symptomsPresentToCure) //Check if symptom has required symptoms cured
                {
                    bool isPresent = false;

                    foreach (var smpt in symptomList) 
                    {
                        if (smpt.symptom == sympt)
                        {
                            isPresent = true;
                        }
                    }
                    if (!isPresent)
                        return false;
                }
                if (canBeCured)
                {
                    RemSymptom(item.symptom);
                    return true;
                }
            }
        }
        return false;
    }
    public bool CheckSymptom(Symptom symptom)
    {
        foreach (var item in symptomList)
        {
            if (item.symptom == symptom)
            {
                return true;
            }
        }
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
