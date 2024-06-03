using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questNameField;
    [SerializeField] TextMeshProUGUI questDescriptionField;
    [SerializeField] TextMeshProUGUI questRewardField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupQuestUI(Quest quest)
    {
        questNameField.text += quest.questName;
        questDescriptionField.text += quest.description;
        questRewardField.text += quest.reward.ToString();
    }
    public void ResetQuestUI()
    {
        questNameField.text = "Quest name: ";
        questDescriptionField.text = "Quest description: ";
        questRewardField.text = "Quest reward: ";
    }
}
