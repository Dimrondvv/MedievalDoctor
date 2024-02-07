using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{

    [SerializeField] private float pickupRange;
    private PickupController playerController;
    private PlayerInputActions playerInputActions;
    




    private void Start() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Pickup.performed += PlayerPickup;

        playerController = PlayerManager.Instance.PickupController.GetPickupController();
    }


    void PlayerPickup(UnityEngine.InputSystem.InputAction.CallbackContext callback) {
        Collider highestCollider = SharedOverlapBox.HighestCollider;
        Transform itemPoint = SharedOverlapBox.ItemPoint;

        if (playerController.PickedItem == null) {
            if (highestCollider.GetComponent<PickupController>() == null)
            {
                PickupController.OnPickup?.Invoke(highestCollider.gameObject, itemPoint);
            }
        } else {
                PickupController.OnPutdown?.Invoke(playerController, itemPoint);
        }
    }
}
