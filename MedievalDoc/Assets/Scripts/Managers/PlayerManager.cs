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

    private bool startProgressBar;
    public bool StartProgressBar { get { return startProgressBar; } set { startProgressBar = value; } }

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
        //Patient.OnCureDisease.AddListener(IncrementScore);
        Patient.OnReleasePatient.AddListener(InrecementScorePatientReleased);
        uiText = UIManager.Instance.UiPrefab;
        uiTextChild = uiText.transform.GetChild(0).gameObject;
    }
    
    
    private void IncrementScore(Patient patient)
    {
        score++;
        money += 100;
        Destroy(patient.gameObject);
    }
    private void InrecementScorePatientReleased(Patient patient)
    {
        if (patient.CurrentPoints > 0)
            score += patient.CurrentPoints;
        else
        {
            playerHealth += patient.CurrentPoints;
            if(playerHealth <= 0)
            {
                App.Instance.GameplayCore.GameManager.EndGame();
            }
        }

    }
}
