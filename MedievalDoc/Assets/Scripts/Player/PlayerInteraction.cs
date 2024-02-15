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
        float interactTime = 0;

        if (controller.PickedItem != null)
        {
            if (controller.PickedItem.GetComponent<Tool>())
            {
                interactTime = controller.PickedItem.GetComponent<Tool>().InteractionTime;
            }
            else
            {
                interactTime = interactionTime;
            }
        }
        else if (SharedOverlapBox.HighestCollider.GetComponent<Crafting>())
        {
            interactTime = SharedOverlapBox.HighestCollider.GetComponent<Crafting>().InteractionTime;
        }
        else
        {
            interactTime = interactionTime;
        }

        if (interactTime == 0)
            interactTime = interactionTime;


        StartCoroutine(InteractionCoroutine(interactionTime));
    }

    IEnumerator InteractionCoroutine(float interactTime)
    {
        float time = 0.0f;
        GetComponent<ProgressBar>().StartProgressBar(interactTime);
        while (time < interactTime && playerInputActions.Player.InteractPress.IsPressed())
        {


            if (!SharedOverlapBox.IsInteractable) //Stop the interaction if player leaves the hitbox of interactable object
            {
                Debug.Log(SharedOverlapBox.HighestCollider.gameObject);
                hasInteracted = false;
                GetComponent<ProgressBar>().StopProgressBar();
                yield break;
            }

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log(playerInputActions.Player.InteractPress.IsPressed());
        if (interactTime > time) //Stop the interaction if player stops holding the button before time ends
        {
            Debug.Log("Break");
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

