using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : ScriptableObject
{
    [SerializeField] public GameObject roomPrefab;
    [SerializeField] public List<SicknessScriptableObject> sicknessesAdded;
    [SerializeField] public string upgradeName;
    [SerializeField] public string upgradeDescription;
    [SerializeField] public Image upgradeIcon;

}
