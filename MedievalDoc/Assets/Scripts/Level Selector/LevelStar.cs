using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStar : MonoBehaviour
{
    private List<Image> starImg = new List<Image>();
    // Start is called before the first frame update
    
    void Start()
    {    
        foreach  (Transform t in transform) {
            if (t.GetComponent<Image>()) {
                starImg.Add(t.GetComponent<Image>());
            }
        }
    }

    private void ChangeStarColor(int starsCount) {
        Debug.Log("Zmiana koloru");
        for (int i = 0; i < starsCount; i++) {
            starImg[i].color = new Color32(229, 235, 52, 100);
        }
    }

}
