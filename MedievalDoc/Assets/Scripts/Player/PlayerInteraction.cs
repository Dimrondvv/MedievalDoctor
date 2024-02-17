using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange;
    [SerializeField] private float defaultInteractionTime;
    private PlayerInputActions playerInputActions;
    private PickupController controller;
    //private bool hasInteracted;
    
    private void Start()
    {
        controller = GetComponent<PickupController>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.InteractPress.started += Interact;
    }

    private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        if (SharedOverlapBox.HighestCollider == null)
            return;
        if (SharedOverlapBox.HighestCollider.gameObject.layer != 7)
            return;

        float interactTime = defaultInteractionTime;
        bool setFlag = false; //bool to check if the time was set
        if (controller.PickedItem != null) //Set the interact time to the time specified in the tool
        {
            Tool pickedTool = controller.PickedItem.GetComponent<Tool>();
            if (pickedTool != null)
            {
                interactTime = pickedTool.InteractionTime;
                setFlag = true;
            }

        }
        if (SharedOverlapBox.HighestCollider != null && !setFlag) //If no tool in hand set the interact time to the time specified in crafting
        {
            Crafting isCrafting = SharedOverlapBox.HighestCollider.GetComponent<Crafting>();
            if (isCrafting != null)
                interactTime = isCrafting.InteractionTime;
        }

        //IF MORE THINGS WITH SPECIFIED INTERACT TIME APPEAR ADD IFS HERE!!!!


        StartCoroutine(InteractionCoroutine(interactTime));
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
                //hasInteracted = false;
                GetComponent<ProgressBar>().StopProgressBar();
                yield break;
            }

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log(playerInputActions.Player.InteractPress.IsPressed());
        if (interactTime > time) //Stop the interaction if player stops holding the button before time ends
        {
            //hasInteracted = false;
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

