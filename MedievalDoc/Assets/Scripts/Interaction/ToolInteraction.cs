using UnityEngine;

public class ToolInteraction : MonoBehaviour, IInteract
{
    public string InteractionPrompt => "Interacting";

    private PickupController playerController;

    private void Start()
    {
        playerController = App.Instance.GameplayCore.PlayerManager.PickupController;
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("This tool cannot be interacted with.");
        return false;
    }

    public bool Pickup(Interactor interactor)
    {
        if (playerController.PickedItem == null)
        {
            // Pick up the item
            PickupController.OnPickup?.Invoke(gameObject, interactor.transform);
            playerController.SetPickedItem(gameObject);
            Debug.Log("Picked up tool.");
        }
        return true;
    }
}
