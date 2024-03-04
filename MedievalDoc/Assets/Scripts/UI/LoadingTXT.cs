using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTXT : MonoBehaviour
{
    [SerializeField] private List<Sprite> loadingScreens = new List<Sprite>();
    private List<string> dots = new List<string>();
    private int dotNumber;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        dotNumber = 0;
        dots.Add("");
        dots.Add(".");
        dots.Add("..");
        dots.Add("...");
        InvokeRepeating("LoadingTxt", 0, 0.5f);
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }

    private void LoadingTxt()
    {
        text.text = "Loading" + dots[dotNumber];
        dotNumber += 1;

        if (dotNumber == dots.Count)
        {
            dotNumber = 0;
        }
        Debug.Log(text.text);
    }
}
