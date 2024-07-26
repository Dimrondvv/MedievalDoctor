using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemChest : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private TextMeshPro nameDisplay;
    private bool isClosed = false;
    private Animator animator;
    private Item chestItem;

    public string InteractionPrompt => throw new System.NotImplementedException();

    private void Start()
    {
        nameDisplay.text = itemPrefab.GetComponent<Item>().ItemName;
        animator = GetComponent<Animator>();
        chestItem = itemPrefab.GetComponent<Item>();
        PickupController.OnPickup.AddListener(TakeItemFromChest);
        PickupController.OnPutdown.AddListener(PutItemInChest);
        PickupController.OnInteract.AddListener(CloseChest);
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

    private void CloseChest(GameObject interactedObject, PickupController player)
    {
        if (interactedObject != gameObject)
            return;

        if (isClosed)
        {
            animator.SetBool("isOpen", true);
            isClosed = false;
            PickupController.OnPickup.AddListener(TakeItemFromChest);
            PickupController.OnPutdown.AddListener(PutItemInChest);
        }
        else
        {
            animator.SetBool("isOpen", false);
            isClosed = true;
            PickupController.OnPickup.RemoveListener(TakeItemFromChest);
            PickupController.OnPutdown.RemoveListener(PutItemInChest);
        }
    }

    private void Update()
    {
        if (Interactor.InteractableCollider == GetComponent<Collider>())
        {
            nameDisplay.gameObject.SetActive(true);
        }
        else
        {
            nameDisplay.gameObject.SetActive(false);
        }
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

        if (isClosed)
        {
            // Open the chest and enable item interaction
            Debug.Log("Tosty");
            CloseChest(gameObject, playerController);
        }
        else
        {
            // Pick up the item from the chest
            if (playerManager.PickupController.PickedItem != null)
            {
                PutItemInChest(playerController, transform);
            }
            else
            {
                TakeItemFromChest(gameObject, transform);
            }
        }

        return true;
    }
}
