using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientHealthBar : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private Patient patient;

    private float healthNormalized;

    public void healthBar()
    {
        healthNormalized = (float)patient.Health / patient.HealthMax;
        barImage.fillAmount = healthNormalized;
    }
    private void Update()
    {
        transform.eulerAngles = Vector3.zero;
        
    }
}
