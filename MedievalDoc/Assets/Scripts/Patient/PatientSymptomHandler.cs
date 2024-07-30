using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientSymptomHandler : MonoBehaviour
{
    [SerializeField] private SymptomDependencies symptomDependencies;
    private Patient patient;

    // Start is called before the first frame update
    void Start()
    {
        patient = GetComponent<Patient>();
    }

    private void OnEnable()
    {
        Patient.OnTryAddSymptom.AddListener(AddSymptom);
        Patient.OnTryRemoveSymptom.AddListener(RemoveDiscoveredSymptom);
        PatientManager.OnPatientReleased.AddListener(CheckIfCured);
    }
    private void OnDestroy()
    {
        Patient.OnTryAddSymptom.RemoveListener(AddSymptom);
        Patient.OnTryRemoveSymptom.RemoveListener(RemoveDiscoveredSymptom);
        PatientManager.OnPatientReleased.RemoveListener(CheckIfCured);
    }

    private void RemoveDiscoveredSymptom(Symptom symptom, Patient patient, InteractionTool tool)
    {
        if (patient != this.patient || !patient.FindSymptom(symptom))
            return;



        if (symptomDependencies.canSymptomBeRemoved(symptom, patient))
        {
            foreach (var i in patient.Symptoms)
            {
                if (i.symptom == symptom)
                {
                    List<Symptom.AddedOnRemoval> symptomAdded = i.symptom.addOnRemove;
                    foreach (var smpt in symptomAdded)
                    {
                        if (smpt != null) //Check if addOnRemove variable equals the default value and therefore is null
                        {
                            if (!GetComponent<Patient>().FindSymptom(smpt.notPresentToAdd))
                            {
                                Patient.OnTryAddSymptom.Invoke(smpt.symtpomAddedOnRemoval, patient, tool);
                            }
                        }
                    }
                    patient.Symptoms.Remove(i);
                    Patient.OnRemoveSymptom.Invoke(symptom, patient, tool);
                    patient.removedSymptoms.Add(symptom);
                    break;
                }
            }
        }
    }
    private void AddSymptom(Symptom symptom, Patient patient, InteractionTool tool)
    {
        if (patient != this.patient || patient.FindSymptom(symptom))
            return;


        if (symptomDependencies.canSymptomBeAdded(symptom, patient))
        {
            if (symptom.possibleLocalizations.Count > 0)
            {
                if (SelectLocalization(patient, symptom) == Localization.None && !symptom.possibleLocalizations.Contains(Localization.None))
                    return;
                patient.InsertSymptomToList(symptom, symptom.possibleLocalizations[Random.Range(0, symptom.possibleLocalizations.Count - 1)]);
            }
            else
                patient.InsertSymptomToList(symptom);
            if (symptom.doesRemoveLocalization)
                RemoveLocalization(patient, symptom.localizationRemoved);
            Patient.OnAddSymptom.Invoke(symptom, patient, tool);
        }
    }

    private Localization SelectLocalization(Patient patient, Symptom symptom)
    {
        List<Localization> locsToCheck = symptom.possibleLocalizations;
        while (locsToCheck.Count > 0)
        {
            Localization loc = locsToCheck[Random.Range(0, locsToCheck.Count - 1)];
            if (FindLocationObject(patient, loc) != null)
                return loc;
            else
                locsToCheck.Remove(loc);
        }
        return Localization.None;
    }

    private void RemoveLocalization(Patient patient, Localization localization)
    {
        FindLocationObject(patient, localization).SetActive(false);
        List<SicknessScriptableObject.SymptomStruct> symptomsToRemove = new List<SicknessScriptableObject.SymptomStruct>();
        foreach (var sympt in patient.symptoms)
        {
            if (sympt.localization == localization)
                symptomsToRemove.Add(sympt);
        }
        foreach(var sympt in symptomsToRemove)
        {
            patient.symptoms.Remove(sympt);
        }
    }

    private void CheckIfCured(Patient patient)
    {
        if (patient != this.patient)
            return;


        bool isCured = patient.symptoms.Count == 0;
        if (isCured)
        {
            Patient.OnCureDisease.Invoke(patient);
        }
    }
    public static GameObject FindSymptomObject(Patient patient, SicknessScriptableObject.SymptomStruct symptom)
    {
        foreach(Transform child in patient.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.name == symptom.GetSymptomName() + "_" + symptom.localization.ToString() && symptom.isLocalizationSensitive)
                return child.gameObject;
            else if (child.gameObject.name == symptom.GetSymptomName())
                return child.gameObject;
        }
        return null;
    }
    public static GameObject FindLocationObject(Patient patient, Localization localization)
    {
        foreach (Transform child in patient.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.name == localization.ToString())
                return child.gameObject;
        }
        return null;
    }
}
