using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    private GameObject player;
    private Transform furniturePickupPoint;
    private PickupController playerController;

    private PlayerInputActions playerInputActions;

    public bool picked = false;



    Transform objTransform;

    private void Start() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        furniturePickupPoint = PlayerManager.Instance.PickupController.GetFurniturePickupPoint();
        player = PlayerManager.Instance.PickupController.GetPlayerGameObject();
        playerController = PlayerManager.Instance.PickupController.GetPickupController();
    }


    

    private void OnEnable() {
        PickupController.OnPickup.AddListener(PickupFurniture);
        PickupController.OnPutdown.AddListener(PutdownFurniture);
    }

    private void OnDisable() {
        PickupController.OnPickup.RemoveListener(PickupFurniture);
        PickupController.OnPutdown.RemoveListener(PutdownFurniture);
    }

    private void PickupFurniture(GameObject pickedFurniture, Transform objectPoint) {
        if (pickedFurniture == this.gameObject) {
            playerController.SetPickedItem(pickedFurniture);
            pickedFurniture.GetComponent<Collider>().enabled = false;
            pickedFurniture.transform.position = furniturePickupPoint.position;
            pickedFurniture.GetComponent<SnapBlueprint>().CreateBlueprint(pickedFurniture);
            pickedFurniture.transform.SetParent(player.transform);

            var lastChild = player.transform.childCount - 1;

            player.transform.GetChild(lastChild).localEulerAngles = new Vector3(0, 0, 0);
        }   
    }

    private void PutdownFurniture(PickupController pickedFurniture, Transform trans) {
        if (pickedFurniture.PickedItem != null && playerController.PickedItem.GetComponent<Furniture>() != null) {
            GameObject putDownFurniture = pickedFurniture.PickedItem;
            if (putDownFurniture.GetComponent<SnapBlueprint>().Blueprint.GetComponent<BlueprintTrigger>().isPlacable) {
                putDownFurniture.transform.position = putDownFurniture.GetComponent<SnapBlueprint>().Blueprint.transform.position;
                putDownFurniture.transform.rotation = putDownFurniture.GetComponent<SnapBlueprint>().Blueprint.transform.rotation;
                putDownFurniture.GetComponent<SnapBlueprint>().DestroyBlueprint();
                putDownFurniture.transform.SetParent(null);
                picked = false;
                putDownFurniture.GetComponent<Collider>().enabled = true;
                playerController.SetPickedItem(null);
            }
        }
    }
}
