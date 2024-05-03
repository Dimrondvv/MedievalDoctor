using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Bookmark : MonoBehaviour
{
    [SerializeField] string bookmarkName;
    [SerializeField] TextMeshProUGUI nameTextField;
    [SerializeField] GridLayoutGroup iconLayout;

    private Dictionary<string, DiscoveredData> bookmarkData;


    private void OnEnable()
    {
        NotebookDataHandler handler = App.Instance.GameplayCore.UIManager.NotebookDataHandler; //Get the data handler from UI manager
        bookmarkData = handler.RequestBookmarkData(bookmarkName); //Get the bookmark data from handler
        InstantiateBookmark();
    }

    private void InstantiateBookmark()
    {
        if (iconLayout.transform.childCount > 0) //Dont initialize if the grid children count is higher than 0, since it means that the object has already been initialized
            return;

        GenerateBookmarkIcons();
    }

    private void GenerateBookmarkIcons()
    {

        foreach (var data in bookmarkData.Values)
        {
            GameObject imageObject = new GameObject($"Icon {data.name}");
            imageObject.transform.SetParent(iconLayout.transform);
            Image img = imageObject.AddComponent<Image>();
            img.sprite = data.icon;
        }
    }



}
