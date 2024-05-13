using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    [SerializeField] public GameObject roomPrefab;
    [SerializeField] public List<SicknessScriptableObject> sicknessesAdded;
    [SerializeField] public string upgradeName;
    [SerializeField] public string upgradeDescription;
    [SerializeField] public Sprite upgradeIcon;

}
