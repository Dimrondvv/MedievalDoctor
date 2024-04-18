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

    private TMP_Text questText;
    private TMP_Text uiText;
    private TMP_Text timerText;
    private TMP_Text dayText;


    private PatronCharacter patronCharacter;
    private void Start()
    {
        PickupController.OnPickup.AddListener(SetItemSlot);
        PickupController.OnPutdown.AddListener(NullItemSlot);

        patronCharacter = patron.GetComponent<PatronCharacter>();
        questText = questTXT.GetComponent<TMP_Text>();
        uiText = ui.GetComponent<TMP_Text>();
        timerText = timer.GetComponent<TMP_Text>();
        dayText = dayTXT.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        SetUIText();
        SetQuestText();
    }

    private void SetUIText()
    {
        TimerManager timerManager = App.Instance.GameplayCore.TimerManager;
        PlayerManager playerManager = App.Instance.GameplayCore.PlayerManager;
        uiText.text = $"Score:  {playerManager.Score} \nHealth: {playerManager.PlayerHealth} \nMoney:  {playerManager.Money}";
        timerText.text = string.Format("{0:00}:{1:00}", timerManager.ElapsedTime / 60, timerManager.ElapsedTime % 60);
        dayText.text = $"Day: {dayAndNightController.DayCounter}";
    }

    private void SetQuestText()
    {
        if (patronCharacter.IsQuestActive)
        {
            if (patronCharacter.PatronType.questList[patronCharacter.QuestID].type == QuestType.SymptomQuest)
            {

                questText.text = $"Patron:   {patron.PatronType.patronName}\n"; // Patron Name
                questText.text += $"Quest:    {patronCharacter.PatronType.questList[patronCharacter.QuestID].questName}\n"; // Quest Name
                questText.text += $"Deadline: {patronCharacter.PatronType.questList[patronCharacter.QuestID].daysToFinish} days\n"; // Days to finish
                questText.text += $"Reward:\n    {patronCharacter.PatronType.questList[patronCharacter.QuestID].goldReward} gold\n"; // Gold reward
                questText.text += $"    {patronCharacter.PatronType.questList[patronCharacter.QuestID].scoreReward} points\n"; // score reward

                questText.text += "Task:\n"; // tasks
                for (int i = 0; i < patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks.Count; i++)
                {
                    if (patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].questAction == QuestAction.RemoveSymptom)
                    {
                        questText.text += $"    Remove {patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].symptom.symptomName}"; // Symptom
                        questText.text += $"  {patronCharacter.ListOfRemovedSymptomsForQuest[patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].symptom]}/{patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].requiredAmmount}\n";// ammount
                    }
                    if (patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].questAction == QuestAction.AddSymptom)
                    {
                        questText.text += $"    Add {patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].symptom.symptomName}"; // Symptom
                        questText.text += $"  {patronCharacter.ListOfAddedSymptomsForQuest[patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].symptom]}/{patronCharacter.PatronType.questList[patronCharacter.QuestID].tasks[i].requiredAmmount}\n";// ammount
                    }
                }
            }
            if (patronCharacter.PatronType.questList[patronCharacter.QuestID].type == QuestType.PatientQuest)
            {
                questText.text = $"Patron:   {patron.PatronType.patronName}\n"; // Patron Name
                questText.text += $"Quest:    {patronCharacter.PatronType.questList[patronCharacter.QuestID].questName}\n"; // Quest Name
                questText.text += $"Deadline: {patronCharacter.PatronType.questList[patronCharacter.QuestID].daysToFinish} days\n"; // Days to finish
                questText.text += $"Reward:\n    {patronCharacter.PatronType.questList[patronCharacter.QuestID].goldReward} gold\n"; // Gold reward
                questText.text += $"    {patronCharacter.PatronType.questList[patronCharacter.QuestID].scoreReward} points\n"; // score reward
                questText.text += "Task:\n"; // tasks

                questText.text += $"    Kill: {patronCharacter.KillThisPatient.PatientName}";
            }
        }
        else
        {
            questText.text = "";
        }
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