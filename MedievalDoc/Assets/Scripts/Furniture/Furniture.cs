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
        
        furniturePickupPoint = App.Instance.GameplayCore.PlayerManager.PickupController.GetFurniturePickupPoint();
        player = App.Instance.GameplayCore.PlayerManager.PickupController.GetPlayerGameObject();
        playerController = App.Instance.GameplayCore.PlayerManager.PickupController.GetPickupController();
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
            playerController.PickedItem = pickedFurniture;
            pickedFurniture.GetComponent<Collider>().enabled = false;
            Collider pickedFurnitureCollider = pickedFurniture.GetComponent<Collider>();
            SnapBlueprint pickedFurnitureBlueprint = pickedFurniture.GetComponent<SnapBlueprint>();
            
            pickedFurnitureCollider.enabled = false;
            pickedFurniture.transform.position = furniturePickupPoint.position;
            pickedFurnitureBlueprint.CreateBlueprint(pickedFurniture);
            pickedFurniture.transform.SetParent(player.transform);

            var lastChild = player.transform.childCount - 1;

            player.transform.GetChild(lastChild).localEulerAngles = new Vector3(0, 0, 0);
        }   
    }

    private void PutdownFurniture(PickupController pickedFurniture, Transform transform) {
        if (pickedFurniture.PickedItem == null) {
            return;
        }

        Furniture furniture = playerController.PickedItem.GetComponent<Furniture>();

        if ( furniture ) {
            GameObject putDownFurniture = pickedFurniture.PickedItem;
            SnapBlueprint blueprint = putDownFurniture.GetComponent<SnapBlueprint>();
            BlueprintTrigger blueprintTrigger = blueprint.Blueprint.GetComponent<BlueprintTrigger>();


            if (blueprintTrigger.isPlacable) {
                putDownFurniture.transform.position = blueprint.Blueprint.transform.position;
                putDownFurniture.transform.rotation = blueprint.Blueprint.transform.rotation;

                blueprint.DestroyBlueprint();
                
                putDownFurniture.transform.SetParent(null);
                putDownFurniture.GetComponent<Collider>().enabled = true;
                playerController.PickedItem = null;
                picked = false;
                
            }
        }
    }
}
