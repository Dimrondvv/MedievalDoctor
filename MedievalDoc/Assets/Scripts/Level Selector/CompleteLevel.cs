using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompleteLevel : MonoBehaviour
{
    private List<int> stars = new List<int>();


    private void Start() {
        
        App.Instance.GameplayCore.GameManager.OnStarCount.AddListener(OnLevelComplete);

        if (Data.ImportJsonData.levelConfig.Length > (LevelButtons.levelID - 1)) {
            foreach (var star in Data.ImportJsonData.levelConfig[LevelButtons.levelID - 1].starsRangeList) {
                stars.Add(int.Parse(star));
            }
            App.Instance.GameplayCore.GameManager.OnStarCount.AddListener(OnLevelComplete);
        }
    }

    public void OnLevelComplete() {
        if (LevelButtons.levelID == (LevelButtons.UnlockedLevels + 1)) {
            int playerScore = App.Instance.GameplayCore.PlayerManager.Score;

            if (playerScore >= stars[0] && playerScore < stars[1]) {
                LevelButtons.UnlockedLevels++;
                App.Instance.GameplayCore.GameManager.LevelStarsCount[LevelButtons.levelID - 1] = 1;
            } else if (playerScore >= stars[1] && playerScore < stars[2]) {
                LevelButtons.UnlockedLevels++;
                App.Instance.GameplayCore.GameManager.LevelStarsCount[LevelButtons.levelID - 1] = 2;
            } else if (playerScore >= stars[2]) {
                LevelButtons.UnlockedLevels++;
                App.Instance.GameplayCore.GameManager.LevelStarsCount[LevelButtons.levelID - 1] = 3;
            } else {
                App.Instance.GameplayCore.GameManager.LevelStarsCount[LevelButtons.levelID - 1] = 0;
            }

            PlayerPrefs.GetInt("UnlockedLevels", LevelButtons.UnlockedLevels);
            App.Instance.GameplayCore.GameManager.OnStarCount.RemoveListener(OnLevelComplete);
        }
    }
}
