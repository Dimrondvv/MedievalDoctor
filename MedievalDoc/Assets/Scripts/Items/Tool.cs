using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    //Key - bool which defines if symptom action is valid
    [SerializeField] private SymptomOptions symptomAdded;
    [SerializeField] private SymptomOptions symptomRemoved;
    [SerializeField] private SymptomOptions symptomChecked;
    [SerializeField] private Sprite itemIcon;
    
    private void AddSymptom(GameObject tool, Patient patient)
    {
        if (tool != this.gameObject || !symptomAdded.isValid)
            return;

        patient.sickness.AddSymptom(symptomAdded.symptom);
        PatientEventManager.Instance.OnAddSymptom.Invoke(tool, patient);

    }
    private void RemoveSymptom(GameObject tool, Patient patient)
    {
        if (tool != this.gameObject || !symptomRemoved.isValid)
            return;

        patient.sickness.RemoveSymptom(symptomRemoved.symptom);

        if(patient.sickness.CheckIfCured())
            PatientEventManager.Instance.OnCureDisease.Invoke(patient);
        else
            PatientEventManager.Instance.OnRemoveSymptom.Invoke(tool, patient);

    }
    private void CheckSymptom(GameObject tool, Patient patient)
    {
        if (tool != this.gameObject || !symptomChecked.isValid)
            return;

        bool isPresent = patient.sickness.CheckSymptom(symptomChecked.symptom);
        if (isPresent)
        {
            PatientEventManager.Instance.OnCheckSymptom.Invoke(symptomChecked.symptom, patient);
        }

    }

    private void Start()
    {
        PatientEventManager.Instance.OnToolInteract.AddListener(CheckSymptom);
        PatientEventManager.Instance.OnToolInteract.AddListener(RemoveSymptom);
        PatientEventManager.Instance.OnToolInteract.AddListener(AddSymptom);
    }

    [System.Serializable]
    struct SymptomOptions
    {
        public bool isValid;
        public Symptom symptom;
    }
}
