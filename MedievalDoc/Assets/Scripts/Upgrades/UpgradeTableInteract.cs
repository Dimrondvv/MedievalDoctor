using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeTableInteract : MonoBehaviour
{
    [SerializeField] GameObject upgradeText;
    private UIManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = App.Instance.GameplayCore.UIManager;
    }

    private void Update()
    {
        if(manager.upgradeUI.isActive && !Interactor.InteractableCollider)
        {
            manager.UpgradeBoard();
        }
    }

    public void SubscribeToInteract()
    {
        PickupController.OnInteract.AddListener(OnTableInteract);
        upgradeText.SetActive(true);
    }

    private void OnTableInteract(GameObject obj, PickupController pcp) 
    {
        if (obj != gameObject)
            return;

        manager.UpgradeBoard();
        PickupController.OnInteract.RemoveListener(OnTableInteract);
        upgradeText.SetActive(false);
    }
}
