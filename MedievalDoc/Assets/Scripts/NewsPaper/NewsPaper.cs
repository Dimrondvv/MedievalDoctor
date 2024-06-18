using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewsPaper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI newsPaperText;
    // Start is called before the first frame update
    void Start()
    {
        //App.Instance.GameplayCore.DaySummaryManager.onNewDay.AddListener(test);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpgradeText(string text)
    {
        newsPaperText.text = text;
    }
}
