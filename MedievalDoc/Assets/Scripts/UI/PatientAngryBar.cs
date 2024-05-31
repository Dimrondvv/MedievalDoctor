using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientAngryBar : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private Patient patient;

    private float angryNormalized;
    public void angryBar()
    {
        angryNormalized = (float)patient.AngryMeter / patient.maximumAnger;
        barImage.fillAmount = angryNormalized;
    }
}
