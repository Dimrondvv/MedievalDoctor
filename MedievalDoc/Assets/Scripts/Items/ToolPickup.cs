using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPickup : MonoBehaviour
{
    //private GameObject player;
    private GameObject finger;
    private Transform toolPickupPoint;
    private PickupController pickupController;



    void Start()
    {
        // Pickup tool event Listener
        PickupController.OnPickup.AddListener(PickupTool);
        PickupController.OnPutdown.AddListener(PutdownTool);

        PlayerManager playerManager = App.Instance.GameplayCore.PlayerManager;
        toolPickupPoint = playerManager.PickupController.GetToolPickupPoint();
        finger = playerManager.PickupController.GetFingerObject();
        pickupController = playerManager.PickupController.GetPickupController();
    }


    private void PickupTool(GameObject pickedTool, Transform objectPoint) {
        if (objectPoint == null) {
            return;
        }

        ItemLayDownPoint LayDownPoint = objectPoint.GetComponentInChildren<ItemLayDownPoint>();

        if (pickedTool == this.gameObject) {
            if (objectPoint != null && objectPoint.GetComponentInChildren<ItemLayDownPoint>() != null) {
                objectPoint.GetComponentInChildren<ItemLayDownPoint>().checkIfOccupied = false;
            }

            
            pickupController.SetPickedItem(pickedTool);
        }
    }

    private void PutdownTool(PickupController pickedTool, Transform pickupPoint) {
        if (pickedTool.PickedItem == null || pickupPoint == null) {
            return;
        }


        Tool tool = pickedTool.PickedItem.GetComponent<Tool>();
        ItemLayDownPoint LayDownPoint = pickupPoint.GetComponentInChildren<ItemLayDownPoint>();

        if (tool && LayDownPoint && !LayDownPoint.checkIfOccupied) {
            GameObject putDownTool = pickedTool.PickedItem;
            MeshCollider pickedToolMeshCollider = putDownTool.GetComponent<MeshCollider>();
     
            LayDownPoint.checkIfOccupied = true;
            pickedToolMeshCollider.enabled = true;


            putDownTool.transform.position = LayDownPoint.transform.position;
            putDownTool.transform.rotation = LayDownPoint.transform.rotation;
            putDownTool.transform.SetParent(pickupPoint);
            pickedTool.PickedItem = null;
        }
    }
}
