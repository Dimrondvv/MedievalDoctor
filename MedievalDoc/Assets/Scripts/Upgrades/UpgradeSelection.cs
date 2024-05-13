using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UpgradeSelection
{

    public static List<Upgrade> PickUpgrades(int upgradeCount)
    {

        List<Upgrade> selectedUpgrades = new List<Upgrade>();
        List<Upgrade> upgrades = App.Instance.GameplayCore.UpgradeManager.upgrades;

        if (upgradeCount > upgrades.Count) //Avoid infinite while loops
        {
            Debug.Log("Not enough upgrades to pick from");
            return upgrades;
        }


        while (selectedUpgrades.Count < upgradeCount)
        {
            int rand = Random.Range(0, upgrades.Count);

            if (!selectedUpgrades.Contains(upgrades[rand]))
            {
                selectedUpgrades.Add(upgrades[rand]);
            }
        }

        return selectedUpgrades;
    }

    public static void SelectUpgrade(Upgrade upgrade)
    {
        foreach(var sickness in upgrade.sicknessesAdded)
        {
            App.Instance.GameplayCore.PatientManager.sicknessPool.Add(sickness);
        }
        Debug.Log($"Selected upgrade {upgrade.upgradeName}");
        //TODO - spawn room
    }

}
