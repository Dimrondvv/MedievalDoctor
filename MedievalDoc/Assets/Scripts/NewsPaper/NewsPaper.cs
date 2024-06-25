using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewsPaper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI newsPaperText;
   
    public void UpgradeText(string text)
    {
        if (newsPaperText != null)
        {
            newsPaperText.text = text;
        }
    }
}
