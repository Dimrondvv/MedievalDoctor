using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Symptom Dependency", menuName = "ScriptableObjects/Symptom Dependency", order = 1)]
public class SymptomDependencies : ScriptableObject
{
    public List<Dependency> dependencies;


    [System.Serializable]
    public struct Dependency
    {
        [SerializeField] private Symptom symptom;
        [SerializeField] private List<Symptom> symptomsPresentRequiredToRemove;
        [SerializeField] private List<Symptom> symptomsNotPresentRequiredToRemove;
        [SerializeField] private List<Symptom> symptomsPresentRequiredToAdd;
        [SerializeField] private List<Symptom> symptomsNotPresentRequiredToAdd;
        [SerializeField] private List<Symptom> symptomsPresentRequiredToCheck;
        [SerializeField] private List<Symptom> symptomsNotPresentRequiredToCheck;

        public Symptom GetSymptom { get { return symptom; } }

        public bool canBeAdded(Patient patient)
        {
            foreach(var symptom in patient.Symptoms)
            {
                if (!symptomsPresentRequiredToAdd.Contains(symptom))
                    return false;
                else if (symptomsNotPresentRequiredToAdd.Contains(symptom))
                    return false;
            }
            return true;
        }
        public bool canBeRemoved(Patient patient)
        {
            foreach (var symptom in patient.Symptoms)
            {
                if (!symptomsPresentRequiredToRemove.Contains(symptom))
                    return false;
                else if (symptomsNotPresentRequiredToRemove.Contains(symptom))
                    return false;
            }
            return true;
        }
        public bool canBeChecked(Patient patient)
        {
            foreach (var symptom in patient.Symptoms)
            {
                if (!symptomsPresentRequiredToCheck.Contains(symptom))
                    return false;
                else if (symptomsNotPresentRequiredToCheck.Contains(symptom))
                    return false;
            }
            return true;
        }
    }

    public bool canSymptomBeRemoved(Symptom symptom, Patient patient)
    {
        bool dependencyExists = false;
        foreach(Dependency i in dependencies)
        {
            if(symptom = i.GetSymptom)
            {
                dependencyExists = true; //If dependency exists and is not met function returns false, but if there is no dependency it returns true
                if (i.canBeRemoved(patient)) //Returns true if dependency is met
                {
                    return true;
                }
            }
        }
        if (dependencyExists)
            return false;
        else
            return true;
    }
    public bool canSymptomBeAdded(Symptom symptom, Patient patient)
    {
        bool dependencyExists = false;
        foreach (Dependency i in dependencies)
        {
            if (symptom = i.GetSymptom)
            {
                dependencyExists = true; //If dependency exists and is not met function returns false, but if there is no dependency it returns true
                if (i.canBeAdded(patient)) //Returns true if dependency is met
                {
                    return true;
                }
            }
        }
        if (dependencyExists)
            return false;
        else
            return true;
    }
    public bool canSymptomBeChecked(Symptom symptom, Patient patient)
    {
        bool dependencyExists = false;
        foreach (Dependency i in dependencies)
        {
            if (symptom = i.GetSymptom)
            {
                dependencyExists = true; //If dependency exists and is not met function returns false, but if there is no dependency it returns true
                if (i.canBeChecked(patient)) //Returns true if dependency is met
                {
                    return true;
                }
            }
        }
        if (dependencyExists)
            return false;
        else
            return true;
    }
}
