using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance { get { return instance; } }
    [SerializeField] private Animator animator;
    [SerializeField]
    private PickupController pickupController;
    public PickupController PickupController { get { return pickupController; } }

    private int score; // player score
    public int Score { get { return score; } set { score = value; } }

    private int playerHealth; // player Health (if =< 0 - game over)
    public int PlayerHealth { get { return playerHealth; } set { playerHealth = value; } }

    private int money; // money
    public int Money { get { return money; } set { money = value; } }

    public Animator GetAnimator { get { return animator; } set { animator = value; } } 
    private GameObject uiText;
    private GameObject uiTextChild;

    private void Awake()
    {
        instance = this;
        score = 0;
        playerHealth = 100;
        money = 0;
    }
    private void Start()
    {
        Patient.OnCureDisease.AddListener(IncrementScore);
        uiText = UIManager.Instance.UiPrefab;
        uiTextChild = uiText.transform.GetChild(0).gameObject;
    }
    
    
    private void IncrementScore(Patient patient)
    {
        score++;
        money += 100;
        Destroy(patient.gameObject);
    }

}
