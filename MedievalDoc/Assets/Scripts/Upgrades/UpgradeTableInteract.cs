using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTableInteract : MonoBehaviour, IInteractable
{
    public float InteractionTime { get; set; }
    private UIManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = App.Instance.GameplayCore.UIManager;
    }

    private void Update()
    {

        if(manager.upgradeUI.isActive && SharedOverlapBox.HighestCollider.gameObject != gameObject)
        {
            manager.UpgradeBoard();
        }
    }

    public void SubscribeToInteract()
    {
        PickupController.OnInteract.AddListener(OnTableInteract);
        gameObject.layer = 7;
    }

    private void OnTableInteract(GameObject obj, PickupController pcp) 
    {
        if (obj != gameObject)
            return;

        manager.UpgradeBoard();
        PickupController.OnInteract.RemoveListener(OnTableInteract);
        gameObject.layer = 0;
    }
}
