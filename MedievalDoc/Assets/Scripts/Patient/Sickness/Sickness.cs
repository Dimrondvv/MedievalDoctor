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

    
}