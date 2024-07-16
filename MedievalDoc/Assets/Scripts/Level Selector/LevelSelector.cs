using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    
    void Start()
    {
        App.Instance.GameplayCore.OnLevelChange.AddListener(DebugLevel);    
    }

    private void DebugLevel(int i)
    {
        Debug.Log(App.Instance.GameplayCore.GameManager.ChoosenLevel);
        Debug.Log("DuPSKO: " + i);
    }
}
