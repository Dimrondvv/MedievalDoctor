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
    [SerializeField] Image background;
    public bool isActive = false;

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
        background.gameObject.SetActive(true);
        isActive = true;
    }

    public void ClearUpgradeBoard()
    {
        for(int i = 0; i < upgradeIcons.Count; i++)
        {
            upgradeIcons[i].sprite = null;
            upgradeNames[i].text = "";
            upgradeDescriptions[i].text = "";
        }
        background.gameObject.SetActive(false);
        isActive = false;
    }
    private void SetUpgradeSlot(int index, Upgrade upgrade)
    {
        upgradeIcons[index].sprite = upgrade.upgradeIcon;
        upgradeNames[index].text = upgrade.upgradeName;
        upgradeDescriptions[index].text = upgrade.upgradeDescription;
        var btn = upgradeIcons[index].gameObject.GetComponent<Button>();
        if (btn == null)
        {
            btn = upgradeIcons[index].gameObject.AddComponent<Button>();
        }
        else
        {
            btn.onClick.RemoveAllListeners();
        }
        btn.onClick.AddListener(delegate { OnClickUpgrade(upgrade); });

    }

    private void OnClickUpgrade(Upgrade upgrade)
    {
        UpgradeSelection.SelectUpgrade(upgrade);
        ClearUpgradeBoard();
    }
}
