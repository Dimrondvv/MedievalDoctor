using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LevelSelector : MonoBehaviour
{
    public UnityEvent OnLevelSelected = new UnityEvent();
    void Start()
    {
        Debug.Log(LevelButtons.levelID);
        Debug.Log(App.Instance.GameplayCore.GameManager.ChoosenLevel);
    }

    private void LoadLevel() {
        // Load level
    }

}
