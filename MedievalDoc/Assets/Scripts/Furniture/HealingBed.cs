using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBed : MonoBehaviour
{
    private int healingValue;
    private Patient patient;
    private int tempelapsedTime;
    private TimerManager timerManager;

    private void Start()
    {
        healingValue = App.Instance.GameplayCore.GameManager.bedHealingValue;
        timerManager = App.Instance.GameplayCore.TimerManager;
    }
    private void Update()
    {
        if (tempelapsedTime < timerManager.ElapsedTime)
        {
            HealTick();
            tempelapsedTime = timerManager.ElapsedTime;
        }
    }
    private void HealTick()
    {
        if (this.GetComponentInChildren<Patient>() != null)
        {
            patient = this.GetComponentInChildren<Patient>();
            if (patient.IsAlive)
            {
                patient.Health += healingValue;
            }
        }

    }
}
