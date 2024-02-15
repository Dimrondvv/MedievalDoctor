using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChest : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    private bool isClosed = false;

    private void Start()
    {
        PickupController.OnPickup.AddListener(TakeItemFromChest);
        PickupController.OnPutdown.AddListener(PutItemInChest);
    }

    private void TakeItemFromChest(GameObject item, Transform objectType)
    {
        if (objectType == null)
            return;
        if (objectType.gameObject != gameObject)
            return;

        item = Instantiate(itemPrefab);
        PlayerManager.Instance.PickupController.SetPickedItem(item);
    }
    private void PutItemInChest(PickupController player, Transform objectType)
    {
        if (objectType == null || player.PickedItem == null)
            return;
        if (objectType.gameObject != gameObject || player.PickedItem.GetComponent<Item>() == null)
            return;
        var item = player.PickedItem;
        if (item == null || item.GetComponent<Item>().ItemName != itemPrefab.GetComponent<Item>().ItemName)
            return;
        
        player.PickedItem = null;
        Destroy(item);
    }
    private void PickupChest()
    {

    }
    private void PutdownChest()
    {

    }

    private void CloseChest(GameObject interactedObject, PickupController player)
    {
        if (interactedObject != gameObject)
            return;
        if (isClosed)
        {
            isClosed = false;

        }
        else
        {

        }

    }


}
