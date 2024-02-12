using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int maxDeaths;
    [SerializeField] public int bedHealingValue;
    public int deathCounter;
    private bool isNight;
    public bool IsNight
    {
        get { return isNight; }
        set { isNight = value; }
    }
    private void Awake() 
    { 

        App.Instance.GameplayCore.RegisterGameManager(this);
    }

    private void OnDestroy()
    {
        App.Instance.GameplayCore.UnregisterGameManager();
    }

    public void CheckDeathCounter()
    {
        if(deathCounter >= maxDeaths){
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}