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
        Collider[] hitColliders = Physics.OverlapBox(transform.rotation * (Vector3.forward - new Vector3(0, 0, 0.25f)) + transform.position + new Vector3(0, 1f, 0), new Vector3(0.25f, 1f, 0.25f), transform.rotation);
        if (hitColliders.Length == 0)
        {
            return;
        }

        Collider highestCollider = hitColliders[0];
        Transform itemPoint = null;
        foreach (Collider collider in hitColliders)
        {
            if (collider.transform.position.y > highestCollider.transform.position.y) {
                highestCollider = collider;
            }
                

            if (collider.GetComponentInChildren<ItemLayDownPoint>() || collider.GetComponentInChildren<PatientLayDownPoint>())
                itemPoint = collider.transform;
        }
        
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
