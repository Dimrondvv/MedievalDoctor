using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange;
    [SerializeField] private float interactionTime;
    private PlayerInputActions playerInputActions;
    private PickupController controller;
    private bool hasInteracted;
    
    private void Start()
    {
        controller = GetComponent<PickupController>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        //playerInputActions.Player.Interact.performed += PlayerInteract;
        //playerInputActions.Player.Interact.started += OnInteractionStart;
        //playerInputActions.Player.Interact.canceled += OnInteractionExit;

        playerInputActions.Player.InteractPress.started += Interact;
    }

    private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        if (SharedOverlapBox.HighestCollider == null)
            return;
        if (SharedOverlapBox.HighestCollider.gameObject.layer != 7)
            return;
        

        StartCoroutine(InteractionCoroutine(interactionTime));
        GetComponent<ProgressBar>().StartProgressBar(interactionTime);
    }


    private void OnInteractionExit(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        GetComponent<ProgressBar>().StopProgressBar();
    }



    void PlayerInteract(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        Collider highestCollider = SharedOverlapBox.HighestCollider;
        if (highestCollider.GetComponent<PickupController>() == null)
        {
            PickupController.OnInteract.Invoke(highestCollider.gameObject, controller);
        }
        GetComponent<ProgressBar>().StopProgressBar();
    }


    IEnumerator InteractionCoroutine(float interactTime)
    {
        float time = 0.0f;
        while (time < interactTime && playerInputActions.Player.InteractPress.IsPressed())
        {
            if (!SharedOverlapBox.IsInteractable) //Stop the interaction if player leaves the hitbox of interactable object
            {
                hasInteracted = false;
                GetComponent<ProgressBar>().StopProgressBar();
                yield break;
            }

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (interactTime > time) //Stop the interaction if player stops holding the button before time ends
        {
            hasInteracted = false;
            GetComponent<ProgressBar>().StopProgressBar();
            yield break;
        }
        Collider highestCollider = SharedOverlapBox.HighestCollider;
        if (highestCollider.GetComponent<PickupController>() == null)
        {
            Debug.Log("Interacting");
            GetComponent<ProgressBar>().StopProgressBar();
            PickupController.OnInteract.Invoke(highestCollider.gameObject, controller);
        }
    }

}

