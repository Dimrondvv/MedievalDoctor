using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class LevelSelector : MonoBehaviour
{
    private string levelID;
    private int levelOrder;
    private GameObject levelPrefab;
    

    public UnityEvent OnLevelSelected = new UnityEvent();
    
    void Start()
    {
        LoadLevel();
    }

    private void LoadLevel() {
        // Load level
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("RoomsTest"));
        foreach (var level in Data.ImportJsonData.levelConfig) {
            if (int.Parse(level.levelOrder) == LevelButtons.levelID) {
                levelPrefab = Instantiate(Resources.Load($"Levels/{level.levelPrefab}")) as GameObject;
            }
        }
    }

}
