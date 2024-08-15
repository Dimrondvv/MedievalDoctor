using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemChest : MonoBehaviour, IInteract, IInteractable
{
    [SerializeField] private TextMeshPro nameDisplay;
    private bool isClosed = false;
    private GameObject chestItem;
    private string itemID;
    private string chestItemName;
    public string InteractionPrompt => "Trying to interact";

    [SerializeField] float interactionTime;
    public float InteractionTime { get { return interactionTime; } set { interactionTime = value; } }

    private void Start()
    {
        InitializeChest();
        if (chestItem.GetComponent<Item>() != null)
            nameDisplay.text = chestItem.GetComponent<Item>().ItemName;
        else if (chestItem.GetComponent<Tool>() != null)
        {
            itemID = HelperFunctions.ToolChestLookup(gameObject.name).toolID;
            nameDisplay.text = HelperFunctions.ToolLookup(itemID).toolName;
        }
        else
            Debug.LogWarning($"Chest {gameObject.name} has a wrong name or item/tool doesn't exist");
        chestItemName = nameDisplay.text;
        PickupController.OnPickup.AddListener(TakeItemFromChest);
        PickupController.OnPutdown.AddListener(PutItemInChest);
    }

    private void InitializeChest()
    {
        chestItem = HelperFunctions.ChestItemLookup(gameObject.name);
    }

    private void TakeItemFromChest(GameObject item, Transform objectType)
    {
        if (objectType == null || objectType.gameObject != gameObject)
            return;

        Debug.Log("Take item");
        item = Instantiate(chestItem);
        item.name = chestItem.name;
        PlayerManager playerManager = App.Instance.GameplayCore.PlayerManager;
        playerManager.PickupController.SetPickedItem(item);
    }

    private void PutItemInChest(PickupController player, Transform objectType)
    {
        if (objectType == null || objectType.gameObject != gameObject)
            return;



        var item = player.PickedItem;

        string itemName = item.GetComponent<Item>() ? item.GetComponent<Item>().ItemName : HelperFunctions.ToolLookup(itemID).toolName;

        if (item == null || itemName != chestItemName)
            return;

        player.PickedItem = null;
        Destroy(item);
    }

    public bool Interact(Interactor interactor)
    {
        // Disable interaction
        return false;
    }

    public bool Pickup(Interactor interactor)
    {
        // Implement the logic for picking up the item
        PlayerManager playerManager = App.Instance.GameplayCore.PlayerManager;
        PickupController playerController = playerManager.PickupController;

        // Pick up the item from the chest
        if (playerManager.PickupController.PickedItem != null)
        {
            PutItemInChest(playerController, transform);
        }
        else
        {
            TakeItemFromChest(gameObject, transform);
        }

        return true;
    }
}
