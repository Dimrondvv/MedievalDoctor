using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPickup : MonoBehaviour
{
    //private GameObject player;
    private GameObject finger;
    private Transform toolPickupPoint;
    private PlayerController playerController;

    private GameObject pickedToolObject;

    void Start()
    {
        // Pickup tool event Listener
        PlayerController.OnPickup.AddListener(PickupTool);
        PlayerController.OnPutdown.AddListener(PutdownTool);

        toolPickupPoint = PlayerManager.Instance.PlayerController.GetToolPickupPoint();
        //player = PlayerManager.Instance.PlayerController.GetPlayerGameObject();
        finger = PlayerManager.Instance.PlayerController.GetFingerObject();
        playerController = PlayerManager.Instance.PlayerController.GetPlayerController();
    }

    private void Update()
    {
        //RotatePickedObject(pickedToolObject, toolPickupPoint);
    }

    private void PickupTool(GameObject pickedTool, Transform objectPoint) {
        if (pickedTool == this.gameObject) {
            if (objectPoint != null) {
                objectPoint.GetComponentInChildren<ItemLayDownPoint>().checkIfOccupied = false;
            }

            pickedToolObject = pickedTool;

            playerController.SetPickedItem(pickedTool);
            pickedTool.GetComponent<Collider>().enabled = false;
            pickedTool.transform.position = toolPickupPoint.position;
            pickedTool.transform.SetParent(finger.transform);

            //  var lastChild = player.transform.childCount - 1;

            //player.transform.GetChild(lastChild).localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void PutdownTool(PlayerController pickedTool, Transform pickupPoint) {
        if (pickedTool.PickedItem != null && playerController.PickedItem.GetComponent<Tool>() != null && pickupPoint && pickupPoint.GetComponentInChildren<ItemLayDownPoint>() && !pickupPoint.GetComponentInChildren<ItemLayDownPoint>().checkIfOccupied) {
            GameObject putDownTool = pickedTool.PickedItem;
            pickupPoint.GetComponentInChildren<ItemLayDownPoint>().checkIfOccupied = true;
            putDownTool.transform.position = pickupPoint.GetComponentInChildren<ItemLayDownPoint>().transform.position;
            putDownTool.transform.rotation = pickupPoint.GetComponentInChildren<ItemLayDownPoint>().transform.rotation;
            putDownTool.transform.SetParent(pickupPoint);
            putDownTool.GetComponent<Collider>().enabled = true;
            playerController.SetPickedItem(null);
        }
    }

    private void RotatePickedObject(GameObject pickedTool, Transform objectPoint){
        if (pickedTool != null && objectPoint != null)
        {
            pickedTool.transform.eulerAngles = objectPoint.eulerAngles + new Vector3(0f, 90f, 0);
        }
    }
}
