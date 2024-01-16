using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{

    [SerializeField] private float pickupRange;
    [SerializeField] private PlayerController playerController;
    private PlayerInputActions playerInputActions;
    




    private void Start() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Pickup.performed += PlayerPickup;
    }


    void PlayerPickup(UnityEngine.InputSystem.InputAction.CallbackContext callback) {
        Collider[] hitColliders = Physics.OverlapBox(transform.rotation * Vector3.forward + transform.position + new Vector3(0, 1f, 0), new Vector3(0.5f, 1f, 0.5f), transform.rotation);
        if (hitColliders.Length == 0)
        {
            return;
        }

        Collider highestCollider = hitColliders[0];
        Transform itemLaydownPoint = null;
        foreach (Collider collider in hitColliders)
        {
            if (collider.transform.position.y > highestCollider.transform.position.y)
                highestCollider = collider;

            if (collider.GetComponentInChildren<ItemLayDownPoint>() || collider.GetComponentInChildren<PatientLayDownPoint>())
                itemLaydownPoint = collider.transform;
        }
        
        if (playerController.PickedItem == null) {
            if (highestCollider.GetComponent<PlayerController>() == null)
            {
                Debug.Log("Podnosi: " + highestCollider);
                PlayerController.OnPickup.Invoke(highestCollider.gameObject);
            }
        } else {
                PlayerController.OnPutdown.Invoke(playerController, itemLaydownPoint);
        }
    }

    private void Update() {
        VisualiseBox.DisplayBox(transform.rotation * Vector3.forward + transform.position + new Vector3(0, 1f, 0), new Vector3(0.5f, 1f, 0.5f), transform.rotation);
    }
}
