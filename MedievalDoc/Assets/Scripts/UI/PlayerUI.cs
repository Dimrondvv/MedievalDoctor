using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ui;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] Image itemSlot;


    private void Update()
    {
        ui.GetComponent<TMP_Text>().text = $"Score:    {PlayerManager.Instance.Score} \nHealth: {PlayerManager.Instance.PlayerHealth} \nMoney: {PlayerManager.Instance.Money}";
        timer.GetComponent<TMP_Text>().text = string.Format("{0:00}:{1:00}", TimerManager.Instance.ElapsedTime/60, TimerManager.Instance.ElapsedTime % 60);
    }

    private void Start()
    {
        PickupController.OnPickup.AddListener(SetItemSlot);
        PickupController.OnPutdown.AddListener(NullItemSlot);
    }

    private void SetItemSlot(GameObject item, Transform obj)
    {
        Tool tool = item.GetComponent<Tool>();
        if (!tool)
            return;
        Debug.Log("TEST: " + tool.ItemIcon);
        if (tool.ItemIcon != null)
        {
            itemSlot.sprite = tool.ItemIcon;
        }
        else
        {
            Debug.LogError("No sprite");
        }
    }
    private void NullItemSlot(PickupController pc, Transform tr)
    {
        if (pc.PickedItem == null)
        {
            itemSlot.sprite = null;
        }
        //itemSlot.sprite = null;
    }
}