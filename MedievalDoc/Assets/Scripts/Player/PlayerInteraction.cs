using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange;
    private PlayerInputActions playerInputActions;


    private void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += PlayerInteract;
    }


    void PlayerInteract(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        
        Collider[] hitColliders = Physics.OverlapBox(transform.rotation * Vector3.forward + transform.position + new Vector3(0, 1f, 0), transform.localScale + new Vector3(0, 1f, 0), transform.rotation);
        List<Collider> interactableColliders = new List<Collider>();
        foreach (Collider collider in hitColliders) //Add all collider with interactable script to the list
        {
            if (collider.transform.GetComponent<Interact>() != null)
            {
                interactableColliders.Add(collider);
            }

        }
        if (interactableColliders.Count > 0)
        {
            Collider highestCollider = interactableColliders[0]; //Set the collider with highest y as the highewst collider
            foreach (Collider collider in interactableColliders)
            {
                if (collider.transform.position.y > highestCollider.transform.position.y)
                    highestCollider = collider;
            }
            highestCollider.transform.GetComponent<Interact>().CallInteract(GetComponent<PlayerController>());
        }
    }

}

