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

        toolPickupPoint = PlayerManager.Instance.PickupController.GetToolPickupPoint();
        finger = PlayerManager.Instance.PickupController.GetFingerObject();
        pickupController = PlayerManager.Instance.PickupController.GetPickupController();
    }


    private void PickupTool(GameObject pickedTool, Transform objectPoint) {
        if (objectPoint == null) {
            return;
        }

        ItemLayDownPoint LayDownPoint = objectPoint.GetComponentInChildren<ItemLayDownPoint>();

        if (pickedTool == this.gameObject) {
            if (objectPoint != null) {
                LayDownPoint.checkIfOccupied = false;
            }

            pickupController.SetPickedItem(pickedTool);

            pickedTool.transform.position = toolPickupPoint.position;
            pickedTool.transform.SetParent(finger.transform);
            pickedTool.GetComponent<MeshCollider>().enabled = false;
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

            Vector3 offset = pickedToolMeshCollider.bounds.size;

            putDownTool.transform.position = LayDownPoint.transform.position + new Vector3(0, offset.y/2, 0);
            putDownTool.transform.rotation = LayDownPoint.transform.rotation;
            putDownTool.transform.SetParent(pickupPoint);
            pickedTool.SetPickedItem(null);
        }
    }
}
