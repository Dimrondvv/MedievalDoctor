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
        if (patient.sickness)
            DiscoverNonCriticalSymptoms(patient);
        else
            Debug.LogError("No sickness");
    }
    private void OnEnable()
    {
        if (PatientEventManager.Instance != null)
        {
            PatientEventManager.Instance.OnTryAddSymptom.AddListener(AddAdditionalSymptom);
            PatientEventManager.Instance.OnTryRemoveSymptom.AddListener(RemoveDiscoveredSymptom);
            PatientEventManager.Instance.OnRemoveSymptom.AddListener(CheckIfCured);
            PatientEventManager.Instance.OnCheckSymptom.AddListener(DiscoverSymptom);
        }
    }
    private void OnDestroy()
    {
        PatientEventManager.Instance.OnTryAddSymptom.RemoveListener(AddAdditionalSymptom);
        PatientEventManager.Instance.OnTryRemoveSymptom.RemoveListener(RemoveDiscoveredSymptom);
        PatientEventManager.Instance.OnRemoveSymptom.RemoveListener(CheckIfCured);
        PatientEventManager.Instance.OnCheckSymptom.RemoveListener(DiscoverSymptom);
    }
    private bool CanSymptomBeCured(Symptom symptom)
    {
        foreach (var item in patient.sickness.solutionList)
        {
            if (item.symptom == symptom)
            {
                foreach (var sympt in item.symptomsNotPresentToCure)
                {
                    bool isPresent = patient.sickness.CheckSymptom(sympt);
                    if (isPresent || patient.AdditionalSymptoms.Contains(sympt))
                    {
                        return false;
                    }
                }
                foreach (var sympt in item.symptomsPresentToCure)
                {
                    bool isPresent = patient.sickness.CheckSymptom(sympt);
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
        if (patient != this)
            return;
        bool isRemoved = patient.sickness.RemoveSymptom(symptom);
        if (!isRemoved) //If the symptom is not removed from sickness try removing it from additional symptoms
        {
            if (CanSymptomBeCured(symptom) == false)
                return;

            if (patient.sickness.CheckSymptom(symptom) == true)
            {
                patient.sickness.RemSymptom(symptom);
                patient.DiscoveredSymptoms.Remove(symptom);
                PatientEventManager.Instance.OnRemoveSymptom.Invoke(symptom, patient, tool);
            }
            else
            {
                patient.AdditionalSymptoms.Remove(symptom);
                patient.DiscoveredSymptoms.Remove(symptom);
                PatientEventManager.Instance.OnRemoveSymptom.Invoke(symptom, patient, tool);
            }
        }
        else
        {
            patient.DiscoveredSymptoms.Remove(symptom);
            PatientEventManager.Instance.OnRemoveSymptom.Invoke(symptom, patient, tool);
        }

    }
    private void AddAdditionalSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != this || patient.sickness.CheckSymptom(symptom))
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
            PatientEventManager.Instance.OnAddSymptom.Invoke(symptom, patient, tool);
        }
    }
    private void CheckIfCured(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != this || tool.SymptomsAdded.Count > 0)
            return;

        bool noAdditionalSymptoms = patient.AdditionalSymptoms.Count == 0;
        bool solutionMet = true;
        foreach (SicknessScriptableObject.SolutionStruct symptomCheck in patient.sickness.solutionList)
        {
            foreach (SicknessScriptableObject.SymptomStruct symptomStruct in patient.sickness.symptomList)
            {
                if (symptomStruct.symptom == symptomCheck.symptom)
                {
                    solutionMet = false;
                    Debug.Log($"Symp checked for: {symptomCheck} Symp found: {symptomStruct.symptom}");
                }
            }
        }
        bool isCured = noAdditionalSymptoms && solutionMet;
        if (isCured)
        {
            Debug.Log("Cured");
            // Add gold on cure
            PlayerManager.Instance.Money += 100;
            PatientEventManager.Instance.OnCureDisease.Invoke(patient);
        }
    }
    private void DiscoverNonCriticalSymptoms(Patient patient)
    {
        if (patient != this || patient.DiscoveredSymptoms.Count != 0)
            return;

        foreach (var symptom in patient.sickness.symptomList)
        {
            if (!symptom.isHidden)
                patient.DiscoveredSymptoms.Add(symptom.symptom, symptom.GetSymptomName());
            else
                patient.DiscoveredSymptoms.Add(symptom.symptom, "?");
        }
    }
    private void DiscoverSymptom(Symptom symptom, Patient patient)
    {
        if (patient != this)
            return;
        patient.DiscoveredSymptoms[symptom] = symptom.symptomName;
    }

}
