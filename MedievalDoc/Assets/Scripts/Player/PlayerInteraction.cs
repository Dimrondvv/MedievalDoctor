using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange;
    private PlayerInputActions playerInputActions;
    private PlayerController controller;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += PlayerInteract;
    }


    void PlayerInteract(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        
        Collider[] hitColliders = Physics.OverlapBox(transform.rotation * Vector3.forward + transform.position + new Vector3(0, 1f, 0), transform.localScale + new Vector3(0, 1f, 0), transform.rotation);

        Collider highestCollider = hitColliders[0];

        foreach(Collider collider in hitColliders)
        {                
            if(collider.transform.position.y > highestCollider.transform.position.y)
                highestCollider = collider;
        }
        if (highestCollider.GetComponent<PlayerController>() == null)
        {
            PlayerController.OnInteract.Invoke(highestCollider.gameObject, controller);
        }
    }

}

