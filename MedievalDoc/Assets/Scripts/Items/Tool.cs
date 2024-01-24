using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    //Key - bool which defines if symptom action is valid
    [SerializeField] private Sprite itemIcon;

    [SerializeField] private List<Symptom> symptomsAdded;
    [SerializeField] private List<Symptom> symptomsRemoved;
    [SerializeField] private List<Symptom> symptomsChecked;

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
            PatientEventManager.Instance.OnAddSymptom.Invoke(symptom, patient);

    }
    private void RemoveSymptom(GameObject tool, Patient patient)
    {
        if (tool != this.gameObject || symptomsRemoved.Count == 0)
            return;

        foreach (Symptom symptom in symptomsRemoved)
             PatientEventManager.Instance.OnRemoveSymptom.Invoke(symptom, patient);
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
                PatientEventManager.Instance.OnCheckSymptom.Invoke(symptom, patient);
            }
        }
    }

    private void Start()
    {
        PatientEventManager.Instance.OnToolInteract.AddListener(AddSymptom);
        PatientEventManager.Instance.OnToolInteract.AddListener(RemoveSymptom);
        PatientEventManager.Instance.OnToolInteract.AddListener(CheckSymptom);
    }
}
