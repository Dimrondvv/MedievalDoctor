using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class ToolInteraction : MonoBehaviour, IInteract
{
    public string InteractionPrompt => "Interacting";

    private GameObject finger;
    private Transform toolPickupPoint;
    private PickupController pickupController;

    void Start()
    {
        PickupController.OnPickup.AddListener(PickupTool);
        PickupController.OnPutdown.AddListener(PutdownTool);
        PlayerManager playerManager = App.Instance.GameplayCore.PlayerManager;
        toolPickupPoint = playerManager.PickupController.GetToolPickupPoint();
        finger = playerManager.PickupController.GetFingerObject();
        pickupController = playerManager.PickupController.GetPickupController();
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("This tool cannot be interacted with.");
        return false;
    }

    public bool Pickup(Interactor interactor)
    {
        if (pickupController.PickedItem != null)
        {
            PutdownTool();
        }

        PickupTool(this.gameObject, interactor.transform);

        return true;
    }

    private void PickupTool(GameObject pickedTool, Transform objectPoint)
    {
        Debug.Log("Picking up tool");
        if (pickedTool != this.gameObject)
        {
            Debug.Log("Picking up tool not found");
            return;
        }

        if (pickedTool == this.gameObject)
        {

            ItemLayDownPoint parentLayDown = transform.parent.GetComponent<ItemLayDownPoint>();
            if (parentLayDown != null)
                parentLayDown.checkIfOccupied = false;

            pickupController.SetPickedItem(pickedTool);
        }
    }

    private void PutdownTool()
    {
        Debug.Log("Putting down tool");
    }
}
