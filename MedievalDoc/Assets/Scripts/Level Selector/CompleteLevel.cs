using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{


    public void OnLevelComplete() {
        if (LevelButtons.levelID == (LevelButtons.UnlockedLevels+1)) {
            LevelButtons.UnlockedLevels++;
            PlayerPrefs.GetInt("UnlockedLevels", LevelButtons.UnlockedLevels);
        }

        App.Instance.GameplayCore.GameManager.OnLevelComplete.Invoke();
    }
}
