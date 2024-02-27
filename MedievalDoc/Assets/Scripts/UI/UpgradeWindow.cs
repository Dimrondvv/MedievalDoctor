using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeWindow : MonoBehaviour
{
    [SerializeField] private GameObject updateUI;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<TextMeshProUGUI> buttonText;
    [SerializeField] private List<Image> buttonSprites;
    [SerializeField] private UpgradeManager upgradeManager;
    private List<Tool> availableToolList;
    private string typUpgrade;


    public void PlayerChoice(List<string> toolList, List<Sprite> imageList, string typ)
    {
        typUpgrade = typ;
        buttons[0].onClick.AddListener(Click1);
        buttons[1].onClick.AddListener(Click2);
        buttons[2].onClick.AddListener(Click3);
        Time.timeScale = 0f;
        updateUI.SetActive(true);
        for ( int i = 0; i < toolList.Count; i++ )
        {
            buttons[i].gameObject.SetActive(true);
            buttonText[i].text = toolList[i];
            buttonSprites[i].gameObject.SetActive(true);
            buttonSprites[i].sprite = imageList[i];
        }

    }
    

    private void Click1()
    {
        if (typUpgrade == "tool")
        {
            upgradeManager.MakeToolForMe(buttonText[0].text);
        } else if(typUpgrade == "crafting")
        {
            upgradeManager.MakeCrafting(buttonText[0].text);
        } else if (typUpgrade == "plant")
        {
            upgradeManager.MakePlant(buttonText[0].text);
        }
        
        
        DisableDilemma();
    }
    private void Click2()
    {
        if (typUpgrade == "tool")
        {
            upgradeManager.MakeToolForMe(buttonText[1].text);
        }
        else if (typUpgrade == "crafting")
        {
            upgradeManager.MakeCrafting(buttonText[1].text);
        } else if (typUpgrade == "plant")
        {
            upgradeManager.MakePlant(buttonText[1].text);
        }
        DisableDilemma();
    }
    private void Click3()
    {
        if (typUpgrade == "tool")
        {
            upgradeManager.MakeToolForMe(buttonText[2].text);
        }
        else if (typUpgrade == "crafting")
        {
            // tu nie rób bo nie ma 3 treraz
        }
        else if (typUpgrade == "plant")
        {
            //tu zrob item inny
        }
        DisableDilemma();
    }

    private void DisableDilemma()
    {
        Time.timeScale = 1f;
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
            buttonSprites[i].gameObject.SetActive(false);
            buttonText[i].text = null;
            buttonSprites[i].sprite = null;
        }
        updateUI.SetActive(false);
        
    }


}
