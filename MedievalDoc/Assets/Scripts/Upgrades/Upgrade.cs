using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] List<SicknessScriptableObject> sicknessesAdded;
    [SerializeField] string upgradeName;
    [SerializeField] string upgradeDescription;
    [SerializeField] Image upgradeIcon;

}
