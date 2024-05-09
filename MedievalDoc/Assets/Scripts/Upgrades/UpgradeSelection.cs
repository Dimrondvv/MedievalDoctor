using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSelection : MonoBehaviour
{
    private List<Upgrade> upgrades;
    [SerializeField] private List<Image> upgradeIcons;
    [SerializeField] private List<string> upgradeNames;
    [SerializeField] private List<string> upgradeDescription;
    // Start is called before the first frame update
    void Start()
    {
        upgrades = App.Instance.GameplayCore.UpgradeManager.upgrades;
    }

    
    private void SelectUpgrade(Upgrade upgrade)
    {
        foreach(var sickness in upgrade.sicknessesAdded)
        {
            App.Instance.GameplayCore.PatientManager.sicknessPool.Add(sickness);
        }
        //TODO - spawn room
    }

}
