using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQuest : MonoBehaviour
{
    [SerializeField] List<Quest> quests;
    [SerializeField] List<TutorialQuest> tutorialQuests;

    public static Quest CurrentQuest;


}
