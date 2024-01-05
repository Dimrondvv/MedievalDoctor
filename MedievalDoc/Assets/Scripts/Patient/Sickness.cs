using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SicknessScriptable", menuName = "ScriptableObjects/Sickness", order = 1)]
public class SicknessScriptableObject : ScriptableObject
{
    public List<GameObject> toolsRequired;
    public string sicknessName;
    public int timeToDieMin;
    public int timeToDieMax;

    public List<SymptomStruct> symptomList;

    [System.Serializable]
    public struct SymptomStruct
    {
        public Symptom symptom;
        public bool isTreatable;
        public bool isCritical;
    }
}
