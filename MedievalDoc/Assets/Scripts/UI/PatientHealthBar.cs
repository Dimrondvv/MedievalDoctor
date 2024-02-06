using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientHealthBar : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private Patient patient;
    [SerializeField] private List<Image> symptomIconFields;

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
    private void AddSymptomIcon(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != GetComponentInParent<Patient>())
            return;

        if (symptom.symptomIcon == null)
        {
            Debug.LogError("No icon");
            return;
        }
        
        foreach(var item in symptomIconFields)
        {
            if(item.sprite == null)
            {
                item.gameObject.SetActive(true);
                item.sprite = symptom.symptomIcon;
                return;
            }
        }
    }
    private void RemoveSymptomIcon(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != GetComponentInParent<Patient>())
            return;
        foreach (var item in symptomIconFields)
        {
            if (item.sprite == symptom.symptomIcon)
            {
                item.sprite = null;
                item.gameObject.SetActive(false);
            }
        }
    }
    private void Start()
    {

        Patient thisPatient = GetComponentInParent<Patient>();
        foreach (var item in thisPatient.sickness.symptomList)
        {
            AddSymptomIcon(item.symptom, thisPatient ,null);
        }
        Patient.OnAddSymptom.AddListener(AddSymptomIcon);
        Patient.OnRemoveSymptom.AddListener(RemoveSymptomIcon);
    }
}
