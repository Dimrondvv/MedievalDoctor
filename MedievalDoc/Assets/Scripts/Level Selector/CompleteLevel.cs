using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompleteLevel : MonoBehaviour
{
    private List<int> stars = new List<int>();


    private void Start() {
        foreach (var star in Data.ImportJsonData.levelConfig[LevelButtons.levelID-1].starsRangeList) {
            stars.Add(int.Parse(star));
        }
        
    }

    public void OnLevelComplete() {
        if (LevelButtons.levelID == (LevelButtons.UnlockedLevels+1)) {
            LevelButtons.UnlockedLevels++;
            PlayerPrefs.GetInt("UnlockedLevels", LevelButtons.UnlockedLevels);

            int playerScore = App.Instance.GameplayCore.PlayerManager.Score;
           
            if (playerScore >= stars[0] && playerScore < stars[1]) {
                App.Instance.GameplayCore.GameManager.LevelStarsCount.Add(1);
            } else if(playerScore >= stars[1] && playerScore < stars[2]) {
                App.Instance.GameplayCore.GameManager.LevelStarsCount.Add(2);
            } else {
                App.Instance.GameplayCore.GameManager.LevelStarsCount.Add(3);
            }
        }

        App.Instance.GameplayCore.GameManager.OnLevelComplete.Invoke();
    }
}
