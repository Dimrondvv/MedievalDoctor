using ECM2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class SleepingBed : MonoBehaviour
{
    [SerializeField] DayAndNightController dayAndNightController;
    [SerializeField] float speedUpTime;
    private Vector3 playerPositionOnInteraction;
    private Vector3 playerPositionOnBed;
    private bool amimir;
    private GameObject interactedObject;
    private PickupController player;
    private void Start()
    {
        PickupController.OnInteract.AddListener(SleepTime);
        //player = PlayerManager.Instance.Player;
        DayAndNightController.DayActivation.AddListener(WakeyWakey);
    }

    private void WakeyWakey()
    {
        if (amimir)
        {
            player.GetComponent<CharacterMovement>().enabled = true;
            player.GetComponent<Character>().enabled = true;
            interactedObject.layer = 7;
            amimir = false;
        }
        //if (amimir)
        //{
        //    player.GetComponent<CharacterMovement>().enabled = true;
        //    player.GetComponent<Character>().enabled = true;
        //    interactedObject.GetComponent<Furniture>().enabled = true;
        //    player.transform.position = playerPositionOnInteraction;
        //    interactedObject.layer = 7;
        //    amimir = false;
        //}
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
            playerPositionOnInteraction = player.transform.position;
            playerPositionOnBed = interactedObject.transform.position;
            Debug.Log(playerPositionOnInteraction);
            Debug.Log(playerPositionOnBed);
            interactedObject.layer = 0;
            player.GetComponent<CharacterMovement>().enabled = false;
            player.GetComponent<Character>().enabled = false;
            player.transform.position = playerPositionOnBed;
            dayAndNightController.TimeMultiplier = speedUpTime;
        }
        //else 
        //{
        //    amimir = true;
        //    this.interactedObject = interactedObject;
        //    playerPositionOnInteraction = player.transform.position;
        //    playerPositionOnBed = interactedObject.transform.position;
        //    Debug.Log(playerPositionOnInteraction);
        //    Debug.Log(playerPositionOnBed);
        //    interactedObject.GetComponent<Furniture>().enabled = false;
        //    player.GetComponent<CharacterMovement>().enabled = false;
        //    player.GetComponent<Character>().enabled = false;
        //    //this.player.transform.position = interactedObject.transform.position;
        //    TeleportToBed(playerPositionOnBed);
        //    interactedObject.layer = 0;
        //    dayAndNightController.TimeMultiplier = speedUpTime;
        //}
    }
}
