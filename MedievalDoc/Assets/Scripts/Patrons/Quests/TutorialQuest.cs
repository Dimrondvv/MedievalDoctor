using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialQuest", menuName = "ScriptableObjects/TutorialQuest")]
public class TutorialQuest : Quest
{
    public TutorialQuest nextQuest;
}
