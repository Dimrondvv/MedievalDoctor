using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] List<Image> upgradeIcons;
    [SerializeField] List<TextMeshProUGUI> upgradeNames;
    [SerializeField] List<TextMeshProUGUI> upgradeDescriptions;


    // Start is called before the first frame update
    void Start()
    {
        ClearUpgradeBoard();
    }

    public void InitializeUpgradeBoard()
    {
        List <Upgrade> upgrades = UpgradeSelection.PickUpgrades(upgradeIcons.Count);

        for(int i = 0; i < upgrades.Count; i++)
        {
            SetUpgradeSlot(i, upgrades[i]);
        }
    }

    private void ClearUpgradeBoard()
    {
        for(int i = 0; i < upgradeIcons.Count; i++)
        {
            upgradeIcons[i] = null;
            upgradeNames[i].text = "";
            upgradeDescriptions[i].text = "";
        }
    }
    private void SetUpgradeSlot(int index, Upgrade upgrade)
    {
        upgradeIcons[index] = upgrade.upgradeIcon;
        upgradeNames[index].text = upgrade.upgradeName;
        upgradeDescriptions[index].text = upgrade.upgradeDescription;
    }
}
