using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PickupController pickupController;
    public GameObject playerObject;
    public PickupController PickupController { get { return pickupController; } }

    private int score = 0; // player score
    public int Score { get { return score; } set { score = value; } }

    private int money = 0; // money
    public int Money { get { return money; } set { money = value; } }

    private bool startProgressBar;
    public bool StartProgressBar { get { return startProgressBar; } set { startProgressBar = value; } }

    public Animator GetAnimator { get { return animator; } set { animator = value; } }

    public int scoreToCashModifier;
    public int scoreToHpModifier;
    
    private GameObject uiText;
    private GameObject uiTextChild;

    private void Awake()
    {
        App.Instance.GameplayCore.RegisterPlayerManager(this);
    }
    private void Start()
    {
        uiText = App.Instance.GameplayCore.UIManager.UiPrefab;
        uiTextChild = uiText.transform.GetChild(0).gameObject;
    }

    public void UpdateStats(int health, int score, int cash)
    {
        this.score += score;
        money += cash;
    }
}
