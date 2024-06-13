using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class AppGameState : BaseState
{
    

    public AppGameState(int appState) : base(appState)
    {
    }

    public override void Initialize()
    {
        base.Initialize();
        App.Instance.GameplayCore.DaySummaryManager.ChangingToSummaryState.AddListener(ChanageToSummaryState);
        CheckForUpgrade();
        CheckForWin();
    }

    private void CheckForUpgrade()
    {
        int currentDay = App.Instance.GameplayCore.DaySummaryManager.dayAndNightController.DayCounter;
        List<int> upgradeDays = App.Instance.GameplayCore.UpgradeManager.upgradeDays;
        if (upgradeDays.Contains(currentDay))
        {
            App.Instance.GameplayCore.UpgradeManager.upgradeTable.SubscribeToInteract();
        }

    }

    private void CheckForWin()
    {
        int currentDay = App.Instance.GameplayCore.DaySummaryManager.dayAndNightController.DayCounter;
        if (currentDay == App.Instance.GameplayCore.GameManager.dayOfWin)
        {
            App.Instance.GameplayCore.GameManager.OnGameWin.Invoke();
        }
    }

    private void ChanageToSummaryState()
    {
        Parent.MakeTransition((int)EAppState.Summary);
        App.Instance.GameplayCore.DaySummaryManager.ChangingToSummaryState.RemoveListener(ChanageToSummaryState);

    }


    public override void OnExit(int next)
    {
        if (next != (int)EAppState.Summary)
        {
            SceneManager.UnloadSceneAsync("GameScene");
        }
        base.OnExit(next);
    }
}
