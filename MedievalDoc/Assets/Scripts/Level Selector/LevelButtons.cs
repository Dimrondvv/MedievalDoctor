using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class LevelButtons : MonoBehaviour
{
    public static int levelID;
    [SerializeField] private List<Button> levelBtnList;
    
    private void Start()
    {
        int i = 0;
        foreach (var btn in levelBtnList) {
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
