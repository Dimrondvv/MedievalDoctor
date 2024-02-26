using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemChest : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private TextMeshPro nameDisplay;
    private bool isClosed = false;
    private Animator animator;
    private Item chestItem;

    private void Start()
    {
        //nameDisplay.text = itemPrefab.GetComponent<Item>().ItemName;
        animator = GetComponent<Animator>();
        chestItem = itemPrefab.GetComponent<Item>();
        PickupController.OnPickup.AddListener(TakeItemFromChest);
        PickupController.OnPutdown.AddListener(PutItemInChest);
        PickupController.OnInteract.AddListener(CloseChest);
    }

    private void TakeItemFromChest(GameObject item, Transform objectType)
    {
        if (objectType == null)
            return;
        if (objectType.gameObject != gameObject)
            return;
        Debug.Log("Take item");
        item = Instantiate(itemPrefab);
        PlayerManager.Instance.PickupController.SetPickedItem(item);
    }
    private void PutItemInChest(PickupController player, Transform objectType)
    {
        if (objectType == null)
            return;
        if (objectType.gameObject != gameObject || player.PickedItem.GetComponent<Item>() == null)
            return;
        var item = player.PickedItem;
        if (item == null || item.GetComponent<Item>().ItemName != chestItem.ItemName)
            return;
        
        player.PickedItem = null;
        Destroy(item);
    }

    #region ChestPickupPutdown
    private void PickupChest(GameObject pickedFurniture, Transform objectType)
    {
        if (objectType.gameObject != gameObject)
            return;
        PickupController playerController = PlayerManager.Instance.PickupController.GetPickupController();
        Transform furniturePickupPoint = playerController.GetFurniturePickupPoint();
        GameObject player = playerController.gameObject;

        playerController.PickedItem = pickedFurniture;
        pickedFurniture.GetComponent<Collider>().enabled = false;
        Collider pickedFurnitureCollider = pickedFurniture.GetComponent<Collider>();
        SnapBlueprint pickedFurnitureBlueprint = pickedFurniture.GetComponent<SnapBlueprint>();

        pickedFurnitureCollider.enabled = false;
        pickedFurniture.transform.position = furniturePickupPoint.position;
        pickedFurnitureBlueprint.CreateBlueprint(pickedFurniture);
        pickedFurniture.transform.SetParent(player.transform);

        var lastChild = player.transform.childCount - 1;

        player.transform.GetChild(lastChild).localEulerAngles = new Vector3(0, 0, 0);
    }
    private void PutdownChest(PickupController player, Transform objectType)
    {
        if (player.PickedItem != gameObject)
            return;
        ItemChest furniture = player.PickedItem.GetComponent<ItemChest>();

        if (furniture)
        {
            GameObject putDownFurniture = player.PickedItem;
            SnapBlueprint blueprint = putDownFurniture.GetComponent<SnapBlueprint>();
            BlueprintTrigger blueprintTrigger = blueprint.Blueprint.GetComponent<BlueprintTrigger>();


            if (blueprintTrigger.isPlacable)
            {
                putDownFurniture.transform.position = blueprint.Blueprint.transform.position;
                putDownFurniture.transform.rotation = blueprint.Blueprint.transform.rotation;

                blueprint.DestroyBlueprint();

                putDownFurniture.transform.SetParent(null);
                putDownFurniture.GetComponent<Collider>().enabled = true;
                player.PickedItem = null;

            }
        }
    }
    #endregion

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
            PickupController.OnPickup.RemoveListener(PickupChest);
            PickupController.OnPutdown.RemoveListener(PutdownChest);
        }
        else
        {
            animator.SetBool("isOpen", false);
            isClosed = true;
            PickupController.OnPickup.AddListener(PickupChest);
            PickupController.OnPutdown.AddListener(PutdownChest);
            PickupController.OnPickup.RemoveListener(TakeItemFromChest);
            PickupController.OnPutdown.RemoveListener(PutItemInChest);

        }

    }


}
