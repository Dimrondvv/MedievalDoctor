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
        List<Upgrade> upgradesChecked = new List<Upgrade>();

        if (upgradeCount > upgrades.Count) //Avoid infinite while loops
        {
            Debug.Log("Not enough upgrades to pick from");
            return upgrades;
        }


        while (selectedUpgrades.Count < upgradeCount)
        {
            int rand = Random.Range(0, upgrades.Count);
            bool reqNotmet = false;
            if (!selectedUpgrades.Contains(upgrades[rand]) && !upgradesChecked.Contains(upgrades[rand]))
            {
                foreach(var req in upgrades[rand].requirements)
                {
                    if (!req.CheckRequirement())
                        reqNotmet = true;
                }
                if(!reqNotmet)
                    selectedUpgrades.Add(upgrades[rand]);
                upgradesChecked.Add(upgrades[rand]);
            }
            if (upgrades.Count - upgradesChecked.Count == 0)
                break;
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
        List<Transform> spawnPoints = App.Instance.GameplayCore.UpgradeManager.roomSpawnPoints;
        GameObject.Instantiate(upgrade.roomPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)]);
    }

}
