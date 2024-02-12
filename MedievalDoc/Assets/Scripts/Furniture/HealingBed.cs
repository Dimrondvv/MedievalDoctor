using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBed : MonoBehaviour
{
    private int healingValue;
    private Patient patient;
    private int tempelapsedTime;

    private void Start()
    {
        healingValue = App.Instance.GameplayCore.GameManager.bedHealingValue;
    }
    private void Update()
    {
        if (tempelapsedTime < TimerManager.Instance.ElapsedTime)
        {
            HealTick();
            tempelapsedTime = TimerManager.Instance.ElapsedTime;
        }
    }
    private void HealTick()
    {
        if (this.GetComponentInChildren<Patient>() != null)
        {
            patient = this.GetComponentInChildren<Patient>();
            patient.Health += healingValue;
        }

    }
}
