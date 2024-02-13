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
        DiscoverNonCriticalSymptoms(patient);

    }

    private void OnEnable()
    {
        Patient.OnTryAddSymptom.AddListener(AddSymptom);
        Patient.OnTryRemoveSymptom.AddListener(RemoveDiscoveredSymptom);
        Patient.OnRemoveSymptom.AddListener(CheckIfCured);
        Patient.OnCheckSymptom.AddListener(DiscoverSymptom);
    }
    private void OnDestroy()
    {
        Patient.OnTryAddSymptom.RemoveListener(AddSymptom);
        Patient.OnTryRemoveSymptom.RemoveListener(RemoveDiscoveredSymptom);
        Patient.OnRemoveSymptom.RemoveListener(CheckIfCured);
        Patient.OnCheckSymptom.RemoveListener(DiscoverSymptom);
    }

    private void RemoveDiscoveredSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != this.patient || !patient.FindSymptom(symptom) || !IsToolValid(patient, tool))
            return;



        if (symptomDependencies.canSymptomBeRemoved(symptom, patient))
        {
            foreach (var i in patient.Symptoms)
            {
                if (i.symptom == symptom)
                {
                    patient.Symptoms.Remove(i);
                    patient.DiscoveredSymptoms.Remove(symptom);
                    Patient.OnRemoveSymptom.Invoke(symptom, patient, tool);
                    break;
                }
            }
        }
    }
    private void AddSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != this.patient || patient.FindSymptom(symptom) || !IsToolValid(patient, tool))
            return;


        if (symptomDependencies.canSymptomBeAdded(symptom, patient))
        {
            patient.InsertSymptomToList(symptom);
            patient.DiscoveredSymptoms.Add(symptom, symptom.symptomName + " (+)");
            Patient.OnAddSymptom.Invoke(symptom, patient, tool);
        }
    }

    private bool IsToolValid(Patient patient, Tool tool)
    {
        foreach (var item in tool.SymptomsRemoved)
        {
            if (symptomDependencies.canSymptomBeRemoved(item, patient) == false)
                return false;
        }
        foreach (var item in tool.SymptomsAdded)
        {
            if (symptomDependencies.canSymptomBeAdded(item, patient) == false)
                return false;
        }
        foreach (var item in tool.SymptomsChecked)
        {
            if (symptomDependencies.canSymptomBeChecked(item, patient) == false)
                return false;
        }


        return true;
    }


    private void CheckIfCured(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != this.patient || tool.SymptomsAdded.Count > 0)
            return;


        bool isCured = patient.Symptoms.Count == 0;
        if (isCured)
        {
            // Release chair on curing? fix
            if (patient.GetComponent<Patient>().SpawnerID >= 0)
            {
                SpawnPatientTimer.SpawnPoints[GetComponent<Patient>().SpawnerID].GetComponent<Chair>().IsOccupied = false;
                GetComponent<Patient>().SpawnerID = -69;
            }
            Patient.OnCureDisease.Invoke(patient);
        }
    }
    private void DiscoverNonCriticalSymptoms(Patient patient)
    {
        if (patient != this.patient || patient.DiscoveredSymptoms.Count != 0)
            return;

        foreach (var symptom in patient.Symptoms)
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
