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

    void Start()
    {
        buttons[0].onClick.AddListener(Click1);
        buttons[1].onClick.AddListener(Click2);
        buttons[2].onClick.AddListener(Click3);
    }

    public void PlayerChoice(List<string> toolList, List<Tool> fullToolList)
    {
        updateUI.SetActive(true);
        availableToolList = fullToolList; // czy to jest tu potrzebne? :suscat:
        
        for( int i = 0; i < buttons.Count; i++ )
        {
            buttons[i].gameObject.SetActive(true);
            buttonText[i].text = toolList[i];
            buttonSprites[i].gameObject.SetActive(true);
            for( int j = 0; j < fullToolList.Count; j++)
            {
                if (toolList[i] == fullToolList[j].name)
                {
                    buttonSprites[i].sprite = fullToolList[j].ItemIcon;
                }
            }
        }
    }
    

    private void Click1()
    {
        upgradeManager.MakeToolForMe(buttonText[0].text);
        DisableDilemma();
    }
    private void Click2()
    {
        upgradeManager.MakeToolForMe(buttonText[1].text);
        DisableDilemma();
    }
    private void Click3()
    {
        upgradeManager.MakeToolForMe(buttonText[2].text);
        DisableDilemma();
    }

    private void DisableDilemma()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
            buttonSprites[i].gameObject.SetActive(false);
        }
        updateUI.SetActive(false);
        Time.timeScale = 1f;
    }


}
