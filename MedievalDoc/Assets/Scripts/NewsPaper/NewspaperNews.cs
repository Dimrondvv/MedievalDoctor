using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewspaperScriptable", menuName = "ScriptableObjects/NewspaperNews", order = 1)]
public class NewspaperNews : ScriptableObject
{
    public List<string> EventText;
    public List<int> EventDay;
}