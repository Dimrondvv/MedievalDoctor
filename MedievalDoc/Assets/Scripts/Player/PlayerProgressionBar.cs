using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgressionBar : MonoBehaviour
{
    [SerializeField] private Image barImage;
    private float progress;
    public void progressBar()
    {
        progress += 1f;
        barImage.fillAmount = (float)progress / (float)100;
    }

    void Start()
    {
        progress = 0f;
        barImage.fillAmount = 0f;
    }

    void Update()
    {
        // mo¿na se spacje wcisn¹æ i ez 
        transform.eulerAngles = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(true);
            progressBar();
        }
        /*if (progress==1/*tutaj czy interakcja siê dzieje* )
        {
            gameObject.SetActive(true);
            progressBar();
        } else
        {
            gameObject.SetActive(false);
        }*/
    }
}
