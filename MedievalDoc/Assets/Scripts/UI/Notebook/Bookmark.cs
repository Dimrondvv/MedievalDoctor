using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class Bookmark : MonoBehaviour
{
    [SerializeField] string bookmarkName;
    [SerializeField] TextMeshProUGUI nameTextField;
    [SerializeField] GridLayoutGroup iconLayout;
    [SerializeField] NotebookContentPage notebookContentPage;

    private Dictionary<string, DiscoveredData> bookmarkData;


    private void OnEnable()
    {
        NotebookDataHandler handler = App.Instance.GameplayCore.UIManager.NotebookDataHandler; //Get the data handler from UI manager
        bookmarkData = handler.RequestBookmarkData(bookmarkName); //Get the bookmark data from handler
        InstantiateBookmark();
    }

    private void InstantiateBookmark() //Dont initialize if the grid children count is higher than 0, since it means that the object has already been initialized
    {
        if (iconLayout.transform.childCount > 0)
        {
            notebookContentPage.GeneratePage(bookmarkData.First().Value);
            return;
        }
        GenerateBookmarkIcons();
        notebookContentPage.GeneratePage(bookmarkData.First().Value);
    }

    private void GenerateBookmarkIcons()
    {

        foreach (var data in bookmarkData.Values)
        {
            GameObject imageObject = CreateIconButton(data);
            imageObject.transform.SetParent(iconLayout.transform);
        }
    }

    private GameObject CreateIconButton(DiscoveredData data)
    {
        GameObject imageObject = new GameObject($"Icon {data.name}");
        Image img = imageObject.AddComponent<Image>();
        Button button = imageObject.AddComponent<Button>();
        button.onClick.AddListener(delegate { notebookContentPage.GeneratePage(data); });
        img.sprite = data.icon;

        return imageObject;
    }
}
