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
        if (pickedTool != this.gameObject) {
            return;
        }

        if (pickedTool == this.gameObject) {

            ItemLayDownPoint parentLayDown = transform.parent.GetComponent<ItemLayDownPoint>();
            if(parentLayDown != null)
                parentLayDown.checkIfOccupied = false;
            
            pickupController.SetPickedItem(pickedTool);
        }
    }

    private void PutdownTool(PickupController pickedTool, Transform pickupPoint) {
        if (pickedTool.PickedItem == null || pickupPoint == null) {
            return;
        }


        Tool tool = pickedTool.PickedItem.GetComponent<Tool>();
        ItemLayDownPoint LayDownPoint = pickupPoint.GetComponentInChildren<ItemLayDownPoint>();
        TrashCan trashCan = pickupPoint.GetComponent<TrashCan>();

        if (trashCan != null)
        {
            var player = App.Instance.GameplayCore.PlayerManager.PickupController.GetPlayerGameObject();
            var item = player.gameObject.GetComponent<PickupController>().PickedItem;
            if (item == null)
            {
                return;
            }
            if(pickupPoint.name == trashCan.name)
            {
                Destroy(item.gameObject);
            }
        }


        if (tool && LayDownPoint && !LayDownPoint.checkIfOccupied) {
            GameObject putDownTool = pickedTool.PickedItem;
            MeshCollider pickedToolMeshCollider = putDownTool.GetComponent<MeshCollider>();
     
            LayDownPoint.checkIfOccupied = true;
            pickedToolMeshCollider.enabled = true;


            putDownTool.transform.position = LayDownPoint.transform.position;
            putDownTool.transform.rotation = LayDownPoint.transform.rotation;
            putDownTool.transform.SetParent(LayDownPoint.transform);
            pickedTool.PickedItem = null;
        }
    }
}
