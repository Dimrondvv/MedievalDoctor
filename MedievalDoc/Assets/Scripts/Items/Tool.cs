using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tool : MonoBehaviour
{
    //Key - bool which defines if symptom action is valid
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private string toolName;
    [SerializeField] private List<Symptom> symptomsAdded;
    [SerializeField] private List<Symptom> symptomsRemoved;
    [SerializeField] private List<Symptom> symptomsChecked;
    [SerializeField] private bool isOneUse;
    public static UnityEvent<GameObject, Patient> OnToolInteract = new UnityEvent<GameObject, Patient>(); //Invoked when interacted with a tool

    public List<Symptom> SymptomsRemoved { get { return symptomsRemoved; } }
    public List<Symptom> SymptomsAdded { get { return symptomsAdded; } }
    public List<Symptom> SymptomsChecked { get { return symptomsChecked; } }
    public string ToolName { get { return toolName; } }

    private PlayerInputActions playerInputActions;
    public Sprite ItemIcon
    {
        get { return itemIcon; }
    }
    private void Start()
    {
        OnToolInteract.AddListener(UseTool);
    }

    private void UseTool(GameObject tool, Patient patient)
    {
        if (tool != gameObject)
            return;

        if (symptomsRemoved.Count > 0)
            RemoveSymptom(patient);
        if (symptomsAdded.Count > 0)
            AddSymptom(patient);
        if (symptomsChecked.Count > 0)
            CheckSymptom(patient);

        if (isOneUse)
        {
            Destroy(gameObject);
            PlayerManager.Instance.PickupController.PickedItem = null;
        }
    }

    private void AddSymptom(Patient patient)
    {
        foreach(var symptom in symptomsAdded)
            Patient.OnTryAddSymptom.Invoke(symptom, patient, this);

        if (isOneUse)
            Destroy(gameObject);

    }
    private void RemoveSymptom(Patient patient)
    {
        foreach (Symptom symptom in symptomsRemoved)
        {
            Patient.OnTryRemoveSymptom.Invoke(symptom, patient, this);
        }
    }
    private void CheckSymptom(Patient patient)
    {
        foreach (Symptom symptom in symptomsChecked)
        {
            bool isPresent = patient.FindSymptom(symptom);
            if (isPresent)
            {
                Patient.OnCheckSymptom.Invoke(symptom, patient);
            }
        }
    }
}
