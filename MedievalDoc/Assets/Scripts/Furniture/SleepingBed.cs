using ECM2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SleepingBed : MonoBehaviour
{
    [SerializeField] DayAndNightController dayAndNightController;
    [SerializeField] float speedUpTime;
    private Vector3 playerPositionOnInteraction;
    private Vector3 playerPositionOnBed;
    private bool amimir;
    private GameObject interactedObject;
    private PickupController player;
    [SerializeField] private Canvas blackscreen;
    [SerializeField] private float timeToFade;
    private bool fadeout;
    private bool fadeIn;
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
        PickupController.OnInteract.AddListener(SleepTime);
        DayAndNightController.DayActivation.AddListener(WakeyWakey);
    }

    private void Update()
    {
        if (fadeIn)
        {
            if (blackscreen.GetComponent<CanvasGroup>().alpha <= 1)
            {
                blackscreen.GetComponent<CanvasGroup>().alpha += timeToFade * Time.deltaTime;

                if (blackscreen.GetComponent<CanvasGroup>().alpha == 1)
                {
                    dayAndNightController.TimeMultiplier = speedUpTime;
                    fadeIn = false;
                }
            }
        }
        if (fadeout)
        {
            if (blackscreen.GetComponent<CanvasGroup>().alpha >= 0)
            {
                blackscreen.GetComponent<CanvasGroup>().alpha -= timeToFade * Time.deltaTime;

                if (blackscreen.GetComponent<CanvasGroup>().alpha == 0)
                {
                    fadeout = false;
                    dayAndNightController.TimeMultiplier = dayAndNightController.BasicTimeMultiplayer;
                    player.GetComponent<CharacterMovement>().enabled = true;
                    player.GetComponent<Character>().enabled = true;
                }
            }
        }
    }

    private void WakeyWakey()
    {
        if (amimir)
        {
            fadeout = true;
            amimir = false;
            interactedObject.layer = 7;
            CancelInvoke();
            dotNumber = 0;
        }
    }

    private void SleepTime(GameObject interactedObject, PickupController player)
    {
        if (interactedObject != gameObject)
            return;

        if (!App.Instance.GameplayCore.GameManager.IsNight)
        {
            Debug.Log("You can only sleep at night!");
            return;
        }
        else
        {
            amimir = true;
            this.player = player;
            this.interactedObject = interactedObject;
            Debug.Log("A mimir");
            interactedObject.layer = 0;
            blackscreen.GetComponent<CanvasGroup>().alpha = 0;
            player.GetComponent<CharacterMovement>().enabled = false;
            player.GetComponent<Character>().enabled = false;
            //blackscreen.color.a = 255f;
            InvokeRepeating("Dots", 0, 0.4f);
            fadeIn = true;
        }
    }

    private void Dots()
    {
        text.text = "Sleeping" + dots[dotNumber];
        dotNumber += 1;

        if(dotNumber == dots.Count)
        {
            dotNumber = 0;
        }
    }
}
