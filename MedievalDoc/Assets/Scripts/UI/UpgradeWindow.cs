using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindow : MonoBehaviour
{
    [SerializeField] private GameObject updateUI;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<Image> buttonSprites;
    public string PlayerChoice(List<string> toolList, List<Tool> fullToolList)
    {
        updateUI.SetActive(true);
        //Debug.Log(fullToolList[0].name);
        for (int tool = 0; tool < toolList.Count; tool++)
        {
            
            buttons[tool].gameObject.SetActive(true);
            buttonSprites[tool].gameObject.SetActive(true);

        }

        return "chuj";
    }
}
