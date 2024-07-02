using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSink : MonoBehaviour
{
    [SerializeField] private GameObject waterFlask;
    [SerializeField] private GameObject emptyWaterFlask;

    private void Start()
    {
        PickupController.OnPutdown.AddListener(PourWater);     
    }
    private void PourWater(PickupController player, Transform objectType)
    {
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
