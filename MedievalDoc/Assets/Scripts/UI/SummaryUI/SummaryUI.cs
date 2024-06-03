using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private int money;
    private int madPatients;
    private int deadPatients;
    // Start is called before the first frame update
    void Start()
    {
        money = App.Instance.GameplayCore.PlayerManager.Money;
        madPatients = App.Instance.GameplayCore.GameManager.madCounter;
        deadPatients = App.Instance.GameplayCore.GameManager.deathCounter;

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
        BillText.text = "Royal tax: " + "-150";
    }

    private void UpdatePatientDeathText()
    {
        DeathsText.text = "Deaths: " + deadPatients;
    }

    private void UpdateSummaryMoney()
    {
        SummaryText.text ="Summary: " + (money - 150).ToString();
    }
}
