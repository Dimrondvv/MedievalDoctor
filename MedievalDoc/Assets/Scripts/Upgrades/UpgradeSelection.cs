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
        foreach (var sickness in upgrade.sicknessesAdded)
        {
            App.Instance.GameplayCore.PatientManager.sicknessPool.Add(sickness);
        }
        Debug.Log($"Selected upgrade {upgrade.upgradeName}");
        List<RoomSpawnPoint> spawnPoints = App.Instance.GameplayCore.UpgradeManager.roomSpawnPoints;
        int index;
        do
        {
            if (spawnPoints.Count > 1)
                index = Random.Range(0, spawnPoints.Count);
            else
                index = 0;
        } while (!spawnPoints[index].CheckSpawnConditions());
        var room = GameObject.Instantiate(upgrade.roomPrefab, spawnPoints[index].transform);
        int i = 0;
        foreach (Transform child in room.transform)
        {
            if (child.gameObject.name.Contains("Counter"))
            {
                GameObject.Instantiate(upgrade.toolsAdded[i++].gameObject, child.GetComponentInChildren<ItemLayDownPoint>().gameObject.transform);
            }
        }
        spawnPoints.RemoveAt(index);
        App.Instance.GameplayCore.UpgradeManager.upgradesPossesed++;
    }

}
