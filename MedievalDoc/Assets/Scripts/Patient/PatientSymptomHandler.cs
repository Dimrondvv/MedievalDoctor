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

    private void RemoveDiscoveredSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != this.patient || !patient.FindSymptom(symptom))
            return;



        if (symptomDependencies.canSymptomBeRemoved(symptom, patient))
        {
            foreach (var i in patient.Symptoms)
            {
                if (i.symptom == symptom)
                {
                    Symptom.AddedOnRemoval symptomAdded = i.symptom.addOnRemove;
                    if (symptomAdded.symtpomAddedOnRemoval != null) //Check if addOnRemove variable equals the default value and therefore is null
                    {
                        if(!GetComponent<Patient>().FindSymptom(symptomAdded.notPresentToAdd))
                        {
                            Patient.OnTryAddSymptom.Invoke(symptomAdded.symtpomAddedOnRemoval, patient, tool);
                        }
                    }
                    patient.Symptoms.Remove(i);
                    Patient.OnRemoveSymptom.Invoke(symptom, patient, tool);
                    break;
                }
            }
        }
    }
    private void AddSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != this.patient || patient.FindSymptom(symptom))
            return;


        if (symptomDependencies.canSymptomBeAdded(symptom, patient))
        {
            if(symptom.possibleLocalizations.Count > 0)
                patient.InsertSymptomToList(symptom, symptom.possibleLocalizations[Random.Range(0, symptom.possibleLocalizations.Count - 1)]);
            else
                patient.InsertSymptomToList(symptom);

            if (symptom.doesRemoveLocalization)
                RemoveLocalization(patient, symptom.localizationRemoved);
            Patient.OnAddSymptom.Invoke(symptom, patient, tool);
        }
    }

    private void RemoveLocalization(Patient patient, Localization localization)
    {
        FindLocationObject(patient, localization).SetActive(false);
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
        foreach(Transform child in patient.transform)
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
        foreach (Transform child in patient.transform)
        {
            if (child.gameObject.name == localization.ToString())
                return child.gameObject;
        }
        return null;
    }
}
