using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDataHandler : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] string interactionLogFileName;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = App.Instance.GameplayCore.GameManager;
        if (gameManager == null)
            App.Instance.GameplayCore.OnGameManagerRegistered.AddListener(WaitForGameManager);
        else
            LoadInteractionLog();
    }
    private void OnDestroy()
    {
        SaveInteractionLog();
    }

    private void WaitForGameManager(GameManager manager)
    {
        gameManager = manager;
        LoadInteractionLog();
    }

    private void SaveInteractionLog()
    {
        InteractionLog log = App.Instance.GameplayCore.GameManager.interactionLog;
        App.Instance.GameplayCore.SaveManager.SaveGameData<InteractionLog>(log, interactionLogFileName + ".json");
    }
    private void LoadInteractionLog()
    {
        Debug.Log("load");
        gameManager.interactionLog = App.Instance.GameplayCore.SaveManager.LoadGameData<InteractionLog>(interactionLogFileName + ".json");
        if (gameManager.interactionLog == null)
        {
            Debug.Log("new");
            gameManager.interactionLog = new InteractionLog(new Dictionary<string, int>(), new Dictionary<string, int>(), new Dictionary<string, int>());
        }
    }
}
