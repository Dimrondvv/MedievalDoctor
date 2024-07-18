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
        
        Debug.Log($"DUPSKO {UnlockedLevels}");
        int i = 0;
        foreach (var btn in levelBtnList) {
            if (UnlockedLevels >= i) {
                btn.interactable = true;
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
