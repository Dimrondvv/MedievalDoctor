using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChest : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;

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
        if (objectType == null)
            return;
        if (objectType.gameObject != gameObject )
            return;
        
        var item = player.PickedItem;
/*        if (item.GetComponent<Item>() == null)
            return;
        if)*/
            player.PickedItem = null;
        Destroy(item);
    }

}
