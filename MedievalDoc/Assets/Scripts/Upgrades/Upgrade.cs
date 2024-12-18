using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    [SerializeField] public GameObject roomPrefab;
    [SerializeField] public List<Data.Sickness> sicknessesAdded;
    [SerializeField] public string upgradeName;
    [SerializeField] public string upgradeDescription;
    [SerializeField] public Sprite upgradeIcon;
    [SerializeField] public List<Requirement> requirements;
    [SerializeField] public List<Tool> toolsAdded;

}
