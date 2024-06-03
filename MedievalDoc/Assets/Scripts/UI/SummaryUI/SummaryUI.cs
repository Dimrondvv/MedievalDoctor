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

    // Start is called before the first frame update
    void Start()
    {
        UpdateMoneyCountText();
        UpdateHealedPatientText();
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
        EarnedMoneyText.text = "Earned Money: " + App.Instance.GameplayCore.PlayerManager.Money.ToString();
    }
}
