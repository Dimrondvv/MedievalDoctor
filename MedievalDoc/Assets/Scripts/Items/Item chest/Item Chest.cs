using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemChest : MonoBehaviour, IInteract, IInteractable
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private TextMeshPro nameDisplay;
    private bool isClosed = false;
    private Item chestItem;

    public string InteractionPrompt => "Trying to interact";

    [SerializeField] float interactionTime;
    public float InteractionTime { get { return interactionTime; } set { interactionTime = value; } }

    private void Start()
    {
        nameDisplay.text = itemPrefab.GetComponent<Item>().ItemName;
        chestItem = itemPrefab.GetComponent<Item>();
        PickupController.OnPickup.AddListener(TakeItemFromChest);
        PickupController.OnPutdown.AddListener(PutItemInChest);
    }

    private void TakeItemFromChest(GameObject item, Transform objectType)
    {
        if (objectType == null || objectType.gameObject != gameObject)
            return;

        Debug.Log("Take item");
        item = Instantiate(itemPrefab);
        PlayerManager playerManager = App.Instance.GameplayCore.PlayerManager;
        playerManager.PickupController.SetPickedItem(item);
    }

    private void PutItemInChest(PickupController player, Transform objectType)
    {
        if (objectType == null || objectType.gameObject != gameObject || player.PickedItem.GetComponent<Item>() == null)
            return;

        var item = player.PickedItem;
        if (item == null || item.GetComponent<Item>().ItemName != chestItem.ItemName)
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
