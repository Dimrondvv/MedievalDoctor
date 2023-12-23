using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SicknessScriptable", menuName = "ScriptableObjects", order = 1)]
public class SicknessScriptableObject : ScriptableObject
{
    public List<GameObject> toolsRequired;
    public string sicknessName;
}
