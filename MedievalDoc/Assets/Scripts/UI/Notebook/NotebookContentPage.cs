using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NotebookContentPage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pageTitleField;
    [SerializeField] TextMeshProUGUI pageContentField;
    [SerializeField] Image iconField;

    public void GeneratePage(DiscoveredData data)
    {
        pageTitleField.text = data.name;
        pageContentField.text = data.description;
        iconField.sprite = data.icon;
    }

}
