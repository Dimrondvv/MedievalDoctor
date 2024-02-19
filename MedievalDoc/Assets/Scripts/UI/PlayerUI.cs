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
    [SerializeField] DayAndNightController dayAndNightController;
    [SerializeField] TextMeshProUGUI dayTXT;

    private PatronCharacter patronCharacter;

    private void Update()
    {
        ui.GetComponent<TMP_Text>().text = $"Score:  {PlayerManager.Instance.Score} \nHealth: {PlayerManager.Instance.PlayerHealth} \nMoney:  {PlayerManager.Instance.Money}";
        timer.GetComponent<TMP_Text>().text = string.Format("{0:00}:{1:00}", TimerManager.Instance.ElapsedTime/60, TimerManager.Instance.ElapsedTime % 60);
        dayTXT.GetComponent<TMP_Text>().text = $"Day: {dayAndNightController.DayCounter}";
        if (patronCharacter.IsQuestActive)
        {
            questTXT.GetComponent<TMP_Text>().text = $"Patron:   {patron.PatronType.patronName}\n"; // Patron Name
            questTXT.GetComponent<TMP_Text>().text += $"Quest:    {patronCharacter.PatronType.questList[patronCharacter.QuestID].questName}\n"; // Quest Name
            questTXT.GetComponent<TMP_Text>().text += $"Deadline: {patronCharacter.PatronType.questList[patronCharacter.QuestID].daysToFinish} days\n"; // Days to finish
            questTXT.GetComponent<TMP_Text>().text += $"Reward:\n    {patronCharacter.PatronType.questList[patronCharacter.QuestID].goldReward} gold\n"; // Gold reward
            questTXT.GetComponent<TMP_Text>().text += $"    {patronCharacter.PatronType.questList[patronCharacter.QuestID].scoreReward} points\n"; // score reward

            questTXT.GetComponent<TMP_Text>().text += "Task:\n"; // tasks
            for (int i = 0; i < patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks.Count;i++)
            {
                if (patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].questAction == QuestAction.RemoveSymptom)
                {
                    questTXT.GetComponent<TMP_Text>().text += $"    Remove {patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].symptom.symptomName}"; // Symptom
                    questTXT.GetComponent<TMP_Text>().text += $"  {patronCharacter.ListOfRemovedSymptomsForQuest[patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].symptom]}/{patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].requiredAmmount}\n";// ammount
                }
                if (patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].questAction == QuestAction.AddSymptom)
                {
                    questTXT.GetComponent<TMP_Text>().text += $"    Add {patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].symptom.symptomName}"; // Symptom
                    questTXT.GetComponent<TMP_Text>().text += $"  {patronCharacter.ListOfAddedSymptomsForQuest[patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].symptom]}/{patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].requiredAmmount}\n";// ammount
                }
            }
        }
        else
        {
            questTXT.GetComponent<TMP_Text>().text = "";
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