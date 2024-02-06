using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tool : MonoBehaviour
{
    //Key - bool which defines if symptom action is valid
    [SerializeField] private Sprite itemIcon;

    [SerializeField] private List<Symptom> symptomsAdded;
    [SerializeField] private List<Symptom> symptomsRemoved;
    [SerializeField] private List<Symptom> symptomsChecked;
    public static UnityEvent<GameObject, Patient> OnToolInteract = new UnityEvent<GameObject, Patient>(); //Invoked when interacted with a tool

    public List<Symptom> SymptomsRemoved { get { return symptomsRemoved; } }
    public List<Symptom> SymptomsAdded { get { return symptomsAdded; } }

    private PlayerInputActions playerInputActions;
    public Sprite ItemIcon
    {
        get { return itemIcon; }
    }

    private void AddSymptom(GameObject tool, Patient patient)
    {
        if (tool != this.gameObject || symptomsAdded.Count == 0)
            return;
        Debug.Log("Add symptom");
        foreach(var symptom in symptomsAdded)
            Patient.OnTryAddSymptom.Invoke(symptom, patient, this);

    }
    private void RemoveSymptom(GameObject tool, Patient patient)
    {
        if (tool != this.gameObject || symptomsRemoved.Count == 0)
            return;

        foreach (Symptom symptom in symptomsRemoved)
             Patient.OnTryRemoveSymptom.Invoke(symptom, patient, this);
    }
    private void CheckSymptom(GameObject tool, Patient patient)
    {
        if (tool != this.gameObject || symptomsChecked.Count == 0)
            return;
        foreach (Symptom symptom in symptomsChecked)
        {
            bool isPresent = patient.sickness.CheckSymptom(symptom);
            if (isPresent)
            {
                Patient.OnCheckSymptom.Invoke(symptom, patient);
            }
        }
    }

    private void Start()
    {
        OnToolInteract.AddListener(AddSymptom);
        OnToolInteract.AddListener(RemoveSymptom);
        OnToolInteract.AddListener(CheckSymptom);
    }
}
