using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SicknessScriptable", menuName = "ScriptableObjects/Sickness", order = 1)]
public class SicknessScriptableObject : ScriptableObject
{
    public List<GameObject> toolsRequired;
    public string sicknessName;
    public List<SymptomStruct> symptomList;
    public List<string> stories;

    [System.Serializable]
    public struct SymptomStruct
    {
        public Symptom symptom;
        public bool isTreatable;
        public bool isCritical;

        public string GetSymptomName()
        {
            return symptom.symptomName;
        }
    }

    public void AddSymptom(Symptom symptom)
    {
        symptomList.Add(new SymptomStruct { symptom = symptom, isTreatable = true, isCritical = false});
    }
    public void RemoveSymptom(Symptom symptom)
    {
        foreach(var item in symptomList)
        {
            if (item.symptom == symptom)
            {
                symptomList.Remove(item);
                return;
            }
        }
        Debug.Log("Symptom not found");
    }
    public void CheckSymptom(Symptom symptom)
    {
        foreach (var item in symptomList)
        {
            if (item.symptom == symptom)
            {
                Debug.Log($"Symptom found {item.symptom.name}");
                return;
            }
        }
        Debug.Log("Symptom not found");
    }
    public void ListSymptoms()
    {

        foreach(SymptomStruct symptom in symptomList)
        {
            Debug.Log(symptom.GetSymptomName());
        }
    }
}
