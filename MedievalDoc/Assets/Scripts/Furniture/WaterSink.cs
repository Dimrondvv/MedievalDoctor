using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WaterSink : MonoBehaviour
{
    [SerializeField] private GameObject waterFlask;
    [SerializeField] private GameObject emptyWaterFlask;

    private void Start()
    {
<<<<<<< Updated upstream
        PickupController.OnPutdown.AddListener(PourWater);     
=======
        PickupController.OnInteract.AddListener(PourWater);
>>>>>>> Stashed changes
    }
    private void PourWater(PickupController player, Transform objectType)
    {
<<<<<<< Updated upstream
        var item = player.PickedItem;
        if (item.GetComponent<Item>().ItemName == emptyWaterFlask.GetComponent<Item>().ItemName)
=======
        try
        {
            var item = player.PickedItem;
            if (item == emptyWaterFlask)
            {
                player.PickedItem = null;
                Destroy(item);
                item = Instantiate(waterFlask);
                PlayerManager playerManager = App.Instance.GameplayCore.PlayerManager;
                playerManager.PickupController.SetPickedItem(item);
            }

        }
        catch (Exception ex)
>>>>>>> Stashed changes
        {
            Debug.LogException(ex, this);
        }
    }
}