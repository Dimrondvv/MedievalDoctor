using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange;
    [SerializeField] private float defaultInteractionTime;
    private PlayerInputActions playerInputActions;
    private PickupController controller;
    private AudioSource interactionAudio;
    //private bool hasInteracted;
    
    private void Start()
    {
        controller = GetComponent<PickupController>();
        interactionAudio = GetComponent<AudioSource>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.InteractPress.started += Interact;
    }

    private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        if (Interactor.InteractableCollider == null)
            return;

        float interactTime = defaultInteractionTime;
        bool setFlag = false; //bool to check if the time was set
        if (controller.PickedItem != null) //Set the interact time to the time specified in the tool
        {
            var pickedTool = controller.PickedItem.GetComponent<IInteractable>();
            if (pickedTool != null)
            {
                interactTime = pickedTool.InteractionTime;
                setFlag = true;
            }

        }
        if (Interactor.InteractableCollider != null && !setFlag) //If no tool in hand set the interact time to the time specified in crafting
        {
            var interactable = Interactor.InteractableCollider.GetComponent<IInteractable>();
            if (interactable != null)
                interactTime = interactable.InteractionTime;
        }

        //IF MORE THINGS WITH SPECIFIED INTERACT TIME APPEAR ADD IFS HERE!!!!


        StartCoroutine(InteractionCoroutine(interactTime));
    }

    IEnumerator InteractionCoroutine(float interactTime)
    {
        float time = 0.0f;
        GetComponent<ProgressBar>().StartProgressBar(interactTime);
        interactionAudio.Play();
        while (time < interactTime && playerInputActions.Player.InteractPress.IsPressed())
        {


            if (!SharedOverlapBox.IsInteractable) //Stop the interaction if player leaves the hitbox of interactable object
            {
                Debug.Log(Interactor.InteractableCollider.gameObject);
                //hasInteracted = false;
                GetComponent<ProgressBar>().StopProgressBar();
                interactionAudio.Stop();
                yield break;
            }

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (interactTime > time) //Stop the interaction if player stops holding the button before time ends
        {
            //hasInteracted = false;
            GetComponent<ProgressBar>().StopProgressBar();
            interactionAudio.Stop();
            yield break;
        }
        Collider highestCollider = Interactor.InteractableCollider;
        if (highestCollider.GetComponent<PickupController>() == null)
        {
            GetComponent<ProgressBar>().StopProgressBar();
            interactionAudio.Stop();
            PickupController.OnInteract.Invoke(highestCollider.gameObject, controller);
        }
    }

}

