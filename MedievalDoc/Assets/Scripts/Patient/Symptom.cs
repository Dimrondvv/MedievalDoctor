using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SymptomScriptable", menuName = "ScriptableObjects/Symptom", order = 2)]
public class Symptom : ScriptableObject
{
    public string symptomName;
    //todo: animacja
    public List<GameObject> toolsRequired;
    public int damage; //how much hp does symptom take per tick
}
