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

    [SerializeField] TextMeshProUGUI questTXT;
    [SerializeField] DayAndNightController dayAndNightController;
    [SerializeField] TextMeshProUGUI dayTXT;

    private TMP_Text questText;
    private TMP_Text uiText;
    private TMP_Text timerText;
    private TMP_Text dayText;


    private void Start()
    {
        PickupController.OnPickup.AddListener(SetItemSlot);
        PickupController.OnPutdown.AddListener(NullItemSlot);

        questText = questTXT.GetComponent<TMP_Text>();
        uiText = ui.GetComponent<TMP_Text>();
        timerText = timer.GetComponent<TMP_Text>();
        dayText = dayTXT.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        SetUIText();
    }

    private void SetUIText()
    {
        TimerManager timerManager = App.Instance.GameplayCore.TimerManager;
        PlayerManager playerManager = App.Instance.GameplayCore.PlayerManager;
        uiText.text = $"Score:  {playerManager.Score} \nHealth: {playerManager.PlayerHealth} \nMoney:  {playerManager.Money}";
        timerText.text = string.Format("{0:00}:{1:00}", timerManager.ElapsedTime / 60, timerManager.ElapsedTime % 60);
        dayText.text = $"Day: {dayAndNightController.DayCounter}";
    }

    private void SetItemSlot(GameObject item, Transform obj)
    {
        Tool tool = item.GetComponent<Tool>();
        if (!tool)
            return;
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
    }
}