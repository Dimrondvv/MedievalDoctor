using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestFunctionality
{
    public static Quest currentQuest { get; private set; }
    private static List<Quest> completedQuests = new List<Quest>();

    public static Quest TutorialQuestLine()
    {
        Quest selectedquest = null;
        if (currentQuest == null)
            selectedquest = App.Instance.GameplayCore.GameManager.tutorialQuests[0];
        else if (currentQuest is TutorialQuest)
            selectedquest = ((TutorialQuest)currentQuest).nextQuest;

        App.Instance.GameplayCore.UIManager.questUI.SetupQuestUI(selectedquest);
        currentQuest = selectedquest;

        return selectedquest;
    }
    public static Quest SelectRandomQuest()
    {
        List<Quest> validQuests = new List<Quest>();
        foreach(var quest in App.Instance.GameplayCore.GameManager.quests)
        {
            if (!completedQuests.Contains(quest))
            {
                bool flag = false;
                foreach(var req in quest.spawnRequirements)
                {
                    if (!req.CheckRequirement())
                        flag = true;
                }
                if (!flag)
                    validQuests.Add(quest);
            }
        }
        if (validQuests.Count > 0)
            return validQuests[Random.Range(0, validQuests.Count - 1)];
        else
            return null;
    }
    public static bool CheckCompletion()
    {
        bool isCompleted = true;
        foreach(var req in currentQuest.completeRequirements)
        {
            if (!req.CheckRequirement())
                isCompleted = false;
        }

        if (isCompleted)
        {
            App.Instance.GameplayCore.UIManager.questUI.ResetQuestUI();
        }

        return isCompleted;
    }

}
