using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPickup : MonoBehaviour
{
    //private GameObject player;
    private GameObject finger;
    private Transform toolPickupPoint;
    private PickupController playerController;

    private GameObject pickedToolObject;

    void Start()
    {
        // Pickup tool event Listener
        PickupController.OnPickup.AddListener(PickupTool);
        PickupController.OnPutdown.AddListener(PutdownTool);

        toolPickupPoint = PlayerManager.Instance.PickupController.GetToolPickupPoint();
        finger = PlayerManager.Instance.PickupController.GetFingerObject();
        playerController = PlayerManager.Instance.PickupController.GetPickupController();
    }

    private void Update()
    {
        //RotatePickedObject(pickedToolObject, toolPickupPoint);
    }

    private void PickupTool(GameObject pickedTool, Transform objectPoint) {
        if (pickedTool == this.gameObject) {
            if (objectPoint != null && objectPoint.GetComponentInChildren<ItemLayDownPoint>() != null) {
                objectPoint.GetComponentInChildren<ItemLayDownPoint>().checkIfOccupied = false;
            }

            pickedToolObject = pickedTool;
            
            playerController.SetPickedItem(pickedTool);
        }
    }

    private void PutdownTool(PickupController pickedTool, Transform pickupPoint) {
        if (pickedTool.PickedItem != null && playerController.PickedItem.GetComponent<Tool>() != null && pickupPoint && pickupPoint.GetComponentInChildren<ItemLayDownPoint>() && !pickupPoint.GetComponentInChildren<ItemLayDownPoint>().checkIfOccupied) {
            GameObject putDownTool = pickedTool.PickedItem;
            pickupPoint.GetComponentInChildren<ItemLayDownPoint>().checkIfOccupied = true;
            putDownTool.GetComponent<MeshCollider>().enabled = true;
            var offset = putDownTool.GetComponent<MeshCollider>().bounds.size;
            putDownTool.transform.position = pickupPoint.GetComponentInChildren<ItemLayDownPoint>().transform.position + new Vector3(0, offset.y/2, 0);
            putDownTool.transform.rotation = pickupPoint.GetComponentInChildren<ItemLayDownPoint>().transform.rotation;
            putDownTool.transform.SetParent(pickupPoint);
            playerController.PickedItem = null;
        }
    }

    private void RotatePickedObject(GameObject pickedTool, Transform objectPoint){
        if (pickedTool != null && objectPoint != null)
        {
            pickedTool.transform.eulerAngles = objectPoint.eulerAngles + new Vector3(0f, 90f, 0);
        }
    }
}
