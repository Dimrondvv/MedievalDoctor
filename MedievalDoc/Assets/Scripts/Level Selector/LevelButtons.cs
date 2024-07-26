using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class LevelButtons : MonoBehaviour
{
    public static int levelID;
    public static int UnlockedLevels;
    [SerializeField] private List<Button> levelBtnList;
    
    private void Start()
    {
        if (UnlockedLevels == 0) {
            UnlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);
        }

        int i = 0;

        foreach (var btn in levelBtnList) {
            if (UnlockedLevels >= i) {
                btn.interactable = true;
            }

            if (i < UnlockedLevels) {
                for (int j = 0; j < levelBtnList[i].gameObject.transform.childCount; j++) {
                    if (levelBtnList[i].gameObject.transform.GetChild(j).GetComponent<Image>() && j <= App.Instance.GameplayCore.GameManager.LevelStarsCount[i]) {
                        levelBtnList[i].gameObject.transform.GetChild(j).GetComponent<Image>().color = new Color32(229, 235, 52, 100);
                    }
                }
            }

            i++;
            btn.gameObject.GetComponentInChildren<TMP_Text>().text = i.ToString();
            btn.onClick.AddListener(delegate { LoadLevel(int.Parse(btn.gameObject.GetComponentInChildren<TMP_Text>().text)); });
        }
    } 

    private void LoadLevel(int i)
    {   
        App.Instance.GameplayCore.PressPlay();
        levelID = i;
        App.Instance.GameplayCore.GameManager.ChoosenLevel = i;
    }
}
