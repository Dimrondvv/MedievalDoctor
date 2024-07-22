using UnityEngine;

public class ToolInteraction : MonoBehaviour, IInteract
{
    public string InteractionPrompt => "Interacting";

    [SerializeField] private float pickupRange;
    private PickupController playerController;
    private PlayerInputActions playerInputActions;

    private void Start()
    {
        playerController = App.Instance.GameplayCore.PlayerManager.PickupController.GetPickupController();
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
            PickupController.OnPickup?.Invoke(this.gameObject, interactor.transform);
            playerController.SetPickedItem(this.gameObject);
            Debug.Log("Picked up tool.");
        }
        return true;
    }
}
