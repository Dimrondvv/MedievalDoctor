using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientSymptomHandler : MonoBehaviour
{
    private Patient patient;

    // Start is called before the first frame update
    void Start()
    {
        patient = GetComponent<Patient>();
        if (patient.Sickness)
            DiscoverNonCriticalSymptoms(patient);
        else
            Debug.LogError("No Sickness");
    }

    private void OnEnable()
    {
        Patient.OnTryAddSymptom.AddListener(AddAdditionalSymptom);
        Patient.OnTryRemoveSymptom.AddListener(RemoveDiscoveredSymptom);
        Patient.OnRemoveSymptom.AddListener(CheckIfCured);
        Patient.OnCheckSymptom.AddListener(DiscoverSymptom);
    }
    private void OnDestroy()
    {
        Patient.OnTryAddSymptom.RemoveListener(AddAdditionalSymptom);
        Patient.OnTryRemoveSymptom.RemoveListener(RemoveDiscoveredSymptom);
        Patient.OnRemoveSymptom.RemoveListener(CheckIfCured);
        Patient.OnCheckSymptom.RemoveListener(DiscoverSymptom);
    }
    private bool CanSymptomBeCured(Symptom symptom)
    {
        foreach (var item in patient.Sickness.solutionList)
        {
            if (item.symptom == symptom)
            {
                foreach (var sympt in item.symptomsNotPresentToCure)
                {
                    bool isPresent = patient.Sickness.CheckSymptom(sympt);
                    if (isPresent || patient.AdditionalSymptoms.Contains(sympt))
                    {
                        return false;
                    }
                }
                foreach (var sympt in item.symptomsPresentToCure)
                {
                    bool isPresent = patient.Sickness.CheckSymptom(sympt);
                    if (!isPresent && !patient.AdditionalSymptoms.Contains(sympt))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    private void RemoveDiscoveredSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != this.patient)
            return;
        Debug.Log("test"+ symptom.name);
        bool isRemoved = patient.Sickness.RemoveSymptom(symptom);
        if (!isRemoved) //If the symptom is not removed from Sickness try removing it from additional symptoms
        {
            if (CanSymptomBeCured(symptom) == false)
                return;

            if (patient.Sickness.CheckSymptom(symptom) == true)
            {
                patient.Sickness.RemSymptom(symptom);
                patient.DiscoveredSymptoms.Remove(symptom);
                Patient.OnRemoveSymptom.Invoke(symptom, patient, tool);
            }
            else
            {
                patient.AdditionalSymptoms.Remove(symptom);
                patient.DiscoveredSymptoms.Remove(symptom);
                Patient.OnRemoveSymptom.Invoke(symptom, patient, tool);
            }
        }
        else
        {
            patient.DiscoveredSymptoms.Remove(symptom);
            Patient.OnRemoveSymptom.Invoke(symptom, patient, tool);
        }

    }
    private void AddAdditionalSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != this.patient || patient.Sickness.CheckSymptom(symptom))
            return;

        foreach (var item in tool.SymptomsRemoved)
        {
            if (CanSymptomBeCured(item) == false)
                return;
        }

        if (!patient.AdditionalSymptoms.Contains(symptom)) //prevent duplicate symptoms
        {
            patient.AdditionalSymptoms.Add(symptom);
            patient.DiscoveredSymptoms.Add(symptom, symptom.symptomName + " (+)");
            Patient.OnAddSymptom.Invoke(symptom, patient, tool);
        }
    }


    private void CheckIfCured(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != this.patient || tool.SymptomsAdded.Count > 0)
            return;
        bool noAdditionalSymptoms = patient.AdditionalSymptoms.Count == 0;
        bool solutionMet = patient.Sickness.symptomList.Count == 0;

        bool isCured = noAdditionalSymptoms && solutionMet;
        if (isCured)
        {
            Debug.Log("---------------"+symptom.name);
            Debug.Log("Cured");
            Patient.OnCureDisease.Invoke(patient);
        }
    }
    private void DiscoverNonCriticalSymptoms(Patient patient)
    {
        if (patient != this.patient || patient.DiscoveredSymptoms.Count != 0)
            return;

        foreach (var symptom in patient.Sickness.symptomList)
        {
            if (!symptom.isHidden)
                patient.DiscoveredSymptoms.Add(symptom.symptom, symptom.GetSymptomName());
            else
                patient.DiscoveredSymptoms.Add(symptom.symptom, "?");
        }
    }
    private void DiscoverSymptom(Symptom symptom, Patient patient)
    {
        if (patient != this.patient)
            return;
        patient.DiscoveredSymptoms[symptom] = symptom.symptomName;
    }

}
