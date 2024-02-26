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
    private string typUpgradu;


    public void PlayerChoice(List<string> toolList, List<Sprite> imageList, string typ)
    {
        typUpgradu = typ;
        buttons[0].onClick.AddListener(Click1);
        buttons[1].onClick.AddListener(Click2);
        buttons[2].onClick.AddListener(Click3);
        Time.timeScale = 0f;
        updateUI.SetActive(true);
        for ( int i = 0; i < buttons.Count; i++ )
        {
            buttons[i].gameObject.SetActive(true);
            buttonText[i].text = toolList[i];
            buttonSprites[i].gameObject.SetActive(true);
            buttonSprites[i].sprite = imageList[i];
        }

    }
    

    private void Click1()
    {
        if (typUpgradu == "tool")
        {
            upgradeManager.MakeToolForMe(buttonText[0].text);
        } else if(typUpgradu == "crafting")
        {
            // tu zrob crafting
        }
        else
        {
            //tu zrob item inny
        }
        
        
        DisableDilemma();
    }
    private void Click2()
    {
        if (typUpgradu == "tool")
        {
            upgradeManager.MakeToolForMe(buttonText[1].text);
        }
        else if (typUpgradu == "crafting")
        {
            // tu zrob crafting
        }
        else
        {
            //tu zrob item inny
        }
        DisableDilemma();
    }
    private void Click3()
    {
        if (typUpgradu == "tool")
        {
            upgradeManager.MakeToolForMe(buttonText[2].text);
        }
        else if (typUpgradu == "crafting")
        {
            // tu zrob crafting
        }
        else
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
