using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class WaterSink : MonoBehaviour
{
    [SerializeField] private GameObject waterFlask;
    [SerializeField] private GameObject emptyWaterFlask;

    private void Start()
    {
        PickupController.OnInteract.AddListener(PourWater);     
    }
    private void PourWater(GameObject interactedObject, PickupController player)
    {
        if (interactedObject != gameObject) {
            return;
        }
        var item = player.PickedItem;
        if (item.GetComponent<Item>().ItemName == emptyWaterFlask.GetComponent<Item>().ItemName)
        {
            player.PickedItem = null;
            Destroy(item);
            item = Instantiate(waterFlask);
            PlayerManager playerManager = App.Instance.GameplayCore.PlayerManager;
            playerManager.PickupController.SetPickedItem(item);
        }
    }
}
