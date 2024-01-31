using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PatientEventManager : MonoBehaviour
{
    private static PatientEventManager instance;
    public static PatientEventManager Instance
    {
        get { return instance; }
        private set { instance = value; }
    }

    public UnityEvent<GameObject, Patient> OnToolInteract = new UnityEvent<GameObject, Patient>(); //Invoked when interacted with a tool
    public UnityEvent<Patient> OnHandInteract = new UnityEvent<Patient>(); //Invoked when interacted with hand

    public UnityEvent<Symptom, Patient> OnCheckSymptom = new UnityEvent<Symptom, Patient>(); //Invoked when tool is used to check for symptom
    public UnityEvent<Symptom, Patient, Tool> OnAddSymptom = new UnityEvent<Symptom, Patient, Tool>(); //Invoked when tool used adds a symptom to patient
    public UnityEvent<Symptom, Patient, Tool> OnTryAddSymptom = new UnityEvent<Symptom, Patient, Tool>(); //Invoked when tool used adds a symptom to patient
    public UnityEvent<Symptom, Patient, Tool> OnRemoveSymptom = new UnityEvent<Symptom, Patient, Tool>(); //Invoked when tool used removes a symptom from patient
    public UnityEvent<Symptom, Patient, Tool> OnTryRemoveSymptom = new UnityEvent<Symptom, Patient, Tool>(); //Invoked when tool used removes a symptom from patient
    public UnityEvent<Patient> OnCureDisease = new UnityEvent<Patient>(); //Invoked when patient's disease is cured
    private void Awake()
    {
        instance = this;
    }
}

