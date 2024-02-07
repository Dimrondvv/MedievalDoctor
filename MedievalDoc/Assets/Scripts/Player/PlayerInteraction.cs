using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange;
    private PlayerInputActions playerInputActions;
    private PickupController controller;
    
    private void Start()
    {
        controller = GetComponent<PickupController>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += PlayerInteract;
        playerInputActions.Player.InteractAnimation.started += OnInteractionStart;
        playerInputActions.Player.InteractAnimation.canceled += OnInteractionExit;
    }

    private void OnInteractionStart(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        if (SharedOverlapBox.HighestCollider.gameObject.layer != 7)
            return;
        PlayerManager.Instance.GetAnimator.SetBool("performingAction", true);
        Debug.Log("started");
    }

    private void OnInteractionExit(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        PlayerManager.Instance.GetAnimator.SetBool("performingAction", false);
        Debug.Log("canceled");
    }



    void PlayerInteract(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        Collider highestCollider = SharedOverlapBox.HighestCollider;
        if (highestCollider.GetComponent<PickupController>() == null)
        {
            PickupController.OnInteract.Invoke(highestCollider.gameObject, controller);
        }
        
    }

}

