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
    [SerializeField] PatronCharacter patron;

    private PatronCharacter patronCharacter;

    private void Update()
    {
        ui.GetComponent<TMP_Text>().text = $"Score:  {PlayerManager.Instance.Score} \nHealth: {PlayerManager.Instance.PlayerHealth} \nMoney:  {PlayerManager.Instance.Money}";
        timer.GetComponent<TMP_Text>().text = string.Format("{0:00}:{1:00}", TimerManager.Instance.ElapsedTime/60, TimerManager.Instance.ElapsedTime % 60);
        if (patronCharacter.IsQuestActive)
        {
            if (patronCharacter.PatronType.questList[patronCharacter.QuestID].questAction == QuestAction.RemoveSymptom)
            {
                questTXT.GetComponent<TMP_Text>().text = $"Patron: {patron.PatronType.patronName}\nQuest:  {patronCharacter.PatronType.questList[patronCharacter.QuestID].questName}\nTask:   Remove {patronCharacter.PatronType.questList[patronCharacter.QuestID].symptom.symptomName}\n        /{patronCharacter.PatronType.questList[patronCharacter.QuestID].requiredAmmount}\nReward: {patronCharacter.PatronType.questList[patronCharacter.QuestID].reward}";
            }
            else
            {                                                                                                                                                                                                                                                                         // listOfRemovedSymptomsForQuest[patronType.questList[questID].symptom]
                questTXT.GetComponent<TMP_Text>().text = $"Patron: {patron.PatronType.patronName}\nQuest:  {patronCharacter.PatronType.questList[patronCharacter.QuestID].questName}\nTask:   Add {patronCharacter.PatronType.questList[patronCharacter.QuestID].symptom.symptomName}\n        {patronCharacter.ListOfAddedSymptomsForQuest[patronCharacter.PatronType.questList[patronCharacter.QuestID].symptom]}/{patronCharacter.PatronType.questList[patronCharacter.QuestID].requiredAmmount}\nReward: {patronCharacter.PatronType.questList[patronCharacter.QuestID].reward}";
            }
        }
    }

    private void Start()
    {
        PickupController.OnPickup.AddListener(SetItemSlot);
        PickupController.OnPutdown.AddListener(NullItemSlot);
        patronCharacter = patron.GetComponent<PatronCharacter>();
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
        //itemSlot.sprite = null;
    }
}