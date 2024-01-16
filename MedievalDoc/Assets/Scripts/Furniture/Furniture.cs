using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    private int picUpRange = 1;
    [SerializeField] GameObject player;
    [SerializeField] Transform furniturePickupPoint;
    private PlayerInputActions playerInputActions;
    [SerializeField] private PlayerController playerController; // na testa

    public bool picked = false;



    Transform objTransform;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }


    

    private void OnEnable() {
        PlayerController.OnPickup.AddListener(PickupFurniture);
        PlayerController.OnPutdown.AddListener(PutdownFurniture);
    }

    private void OnDisable() {
        PlayerController.OnPickup.RemoveListener(PickupFurniture);
        PlayerController.OnPutdown.RemoveListener(PutdownFurniture);
    }

    private void PickupFurniture(GameObject pickedFurniture) {
        if (pickedFurniture == this.gameObject) {
            playerController.SetPickedItem(pickedFurniture);
            Debug.Log("Podnieœ");
            pickedFurniture.GetComponent<Collider>().enabled = false;
            pickedFurniture.transform.position = furniturePickupPoint.position;
            pickedFurniture.GetComponent<SnapBlueprint>().CreateBlueprint(pickedFurniture);
            pickedFurniture.transform.SetParent(player.transform);

            var lastChild = player.transform.childCount - 1;
            //Debug.Log(picked);

            player.transform.GetChild(lastChild).localEulerAngles = new Vector3(0, 0, 0);
        }   
    }

    private void PutdownFurniture(PlayerController pickedFurniture, Transform trans) {
        if (pickedFurniture.PickedItem != null && playerController.PickedItem.GetComponent<Furniture>() != null) {
            Debug.Log("FURNITURE - Putdown");
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
