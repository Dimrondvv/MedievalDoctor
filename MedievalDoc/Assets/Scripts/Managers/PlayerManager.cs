using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance { get { return instance; } }

    private int score; // player score
    public int Score { get { return score; } set { score = value; } }

    private int playerHealth; // player Health (if =< 0 - game over)
    public int PlayerHealth { get { return playerHealth; } set { playerHealth = value; } }

    private int money; // money
    public int Money { get { return money; } set { money = value; } }



    private void Awake()
    {
        instance = this;
        score = 0;
        playerHealth = 100;
        money = 0;
    }



}
