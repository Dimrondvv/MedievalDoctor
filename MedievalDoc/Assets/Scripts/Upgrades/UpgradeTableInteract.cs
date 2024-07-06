using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeTableInteract : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject upgradeText;
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
        upgradeText.SetActive(true);
    }

    private void OnTableInteract(GameObject obj, PickupController pcp) 
    {
        if (obj != gameObject)
            return;

        manager.UpgradeBoard();
        PickupController.OnInteract.RemoveListener(OnTableInteract);
        gameObject.layer = 0;
        upgradeText.SetActive(false);
    }
}
