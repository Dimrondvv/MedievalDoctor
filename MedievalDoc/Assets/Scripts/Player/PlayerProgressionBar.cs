﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgressionBar : MonoBehaviour
{
    [SerializeField] private Image barImage;
    private float progress;
    public void progressBar()
    {
        progress += 1.1f;
        barImage.fillAmount = (float)progress / (float)100;
    }
    void Start()
    {
        progress = 0f;
        barImage.fillAmount = 0f;
    }
    void FixedUpdate()
    {
        transform.eulerAngles = Vector3.zero;
        if (PlayerManager.Instance.GetAnimator.GetBool("performingAction"))
        {
            if (barImage.fillAmount != 1)
            {
                gameObject.GetComponent<Canvas>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<Canvas>().enabled = false;
            }
            
            progressBar();
        } else
        {
            gameObject.GetComponent<Canvas>().enabled = false;
            progress = 0;
            progressBar();
        }
    }
}