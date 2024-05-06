using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Names", menuName = "ScriptableObjects/Names", order = 3)]
public class Names : ScriptableObject
{
    [SerializeField] public List<string> maleNames;
    [SerializeField] public List<string> femaleNames;
    [SerializeField] public List<string> lastNames;

    public string GetRandomName()
    {
        int gender = Random.Range(0, 1); // 0 - male 1 - female
        if (gender == 0)
            return maleNames[Random.Range(0, maleNames.Count - 1)] + lastNames[Random.Range(0, lastNames.Count - 1)];
        else
            return femaleNames[Random.Range(0, femaleNames.Count - 1)] + lastNames[Random.Range(0, lastNames.Count - 1)];
    }
}
