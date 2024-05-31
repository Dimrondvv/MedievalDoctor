using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : ScriptableObject
{
    public string questName;
    public string description;
    public int reward;
    public List<Requirement> requirements;
}
