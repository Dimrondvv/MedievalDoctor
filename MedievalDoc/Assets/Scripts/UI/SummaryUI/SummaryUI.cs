using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class SummaryUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text HealedPatientText;

    [SerializeField]
    private TMP_Text UpsetPatientText;

    [SerializeField]
    private TMP_Text EarnedMoneyText;

    [SerializeField]
    private TMP_Text BillText;

    [SerializeField]
    private TMP_Text DeathsText;

    [SerializeField]
    private TMP_Text SummaryText;

    [SerializeField]
    private Button endDayButton;

    [SerializeField]
    private Button mainMenuButton;

    private int money;
    private int madPatients;
    private int deadPatients;
    private int royalTax;

    public UnityEvent OnEndDayPressed = new UnityEvent();
    public UnityEvent OnMainMenuPressed = new UnityEvent();

    private void Awake()
    {
        endDayButton.onClick.AddListener(HandlePlayPressed);
        mainMenuButton.onClick.AddListener(HandleMenuPressed);
    }

    // Start is called before the first frame update
    void Start()
    {
        money = App.Instance.GameplayCore.PlayerManager.Money;
        madPatients = App.Instance.GameplayCore.GameManager.MadCounter;
        deadPatients = App.Instance.GameplayCore.GameManager.deathCounter;
        royalTax = App.Instance.GameplayCore.GameManager.RoyalTax;
 

        UpdateHealedPatientText();
        UpdateMadPatients();
        UpdateMoneyCountText();
        UpdateRoyalTax();
        UpdatePatientDeathText();
        UpdateSummaryMoney();
    }

    // Update is called once per frame
    void Update()
    {
        if (money - royalTax >= 0)
        {
            endDayButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(false);
        }
        if (money - royalTax < 0)
        {
            mainMenuButton.gameObject.SetActive(true);
            endDayButton.gameObject.SetActive(false);
        }
    }

    private void UpdateHealedPatientText()
    {
        int wyleczeniPacjenci = 0;
        foreach (int ilosc in App.Instance.GameplayCore.GameManager.localInteractionLog.patientsCured.Values){
            wyleczeniPacjenci += ilosc;
        }

        // It should work wtf 
        HealedPatientText.text = "Healed Patients: " + wyleczeniPacjenci.ToString();
    }

    private void UpdateMoneyCountText()
    {
        EarnedMoneyText.text = "Earned Money: " + money.ToString();
    }

    private void UpdateMadPatients()
    {
        UpsetPatientText.text = "Upset Patients: " + madPatients.ToString();
    }
    
    private void UpdateRoyalTax()
    {
        BillText.text = "Royal tax: " + royalTax.ToString();
    }

    private void UpdatePatientDeathText()
    {
        DeathsText.text = "Deaths: " + deadPatients;
    }

    private void UpdateSummaryMoney()
    {
        SummaryText.text ="Summary: " + (money - royalTax).ToString();
        App.Instance.GameplayCore.PlayerManager.Money = money - royalTax;
    }

    private void HandlePlayPressed()
    {      
        OnEndDayPressed.Invoke();
        App.Instance.GameplayCore.DaySummaryManager.PressEndDay();
    }

    private void HandleMenuPressed()
    {
        OnMainMenuPressed.Invoke();
        App.Instance.GameplayCore.DaySummaryManager.PressMenu();
    }
}
