﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Patient : MonoBehaviour
{
    GameObject player;

    [SerializeField] private SicknessScriptableObject sickness;
    public SicknessScriptableObject Sickness
    {
        get { return sickness; }
        set { sickness = value; }
    }


    [SerializeField] private int spawnerID;
    public int SpawnerID
    { 
        get { return spawnerID; } 
        set { spawnerID = value; } 
        }

    [SerializeField] private int health; // player Health (if =< 0 - game over)
    public int Health { get { return health; } set { health = value; } }

    [SerializeField] private int maxHealth; // player Health (if =< 0 - game over)
    public int HealthMax { get { return maxHealth; } set { maxHealth = value; } }

    private List<Symptom> additionalSymptoms = new List<Symptom>();
    public List<Symptom> AdditionalSymptoms { get { return additionalSymptoms; } set { additionalSymptoms = value; } }

    public List<SicknessScriptableObject.SymptomStruct> symptoms;
    public List<SicknessScriptableObject.SymptomStruct> Symptoms { get { return symptoms; } set { symptoms = value; } }

    private Dictionary<Symptom, string> discoveredSymptoms = new Dictionary<Symptom, string>(); //Key - symptom / Display value
    public Dictionary<Symptom, string> DiscoveredSymptoms { get { return discoveredSymptoms; } }
    public string patientStory;
    public bool isAlive;

    [SerializeField] private bool immune; // immunity for tests
    public bool Immune { get { return immune; } set { immune = value; } }

    private string patientName;
    public string PatientName
    {
        get { return patientName; }
        set { patientName = value; }
    }

    public static UnityEvent<Patient> OnPatientDeath = new UnityEvent<Patient>();

    public static UnityEvent<Symptom, Patient> OnCheckSymptom = new UnityEvent<Symptom, Patient>(); //Invoked when tool is used to check for symptom
    public static UnityEvent<Symptom, Patient, Tool> OnAddSymptom = new UnityEvent<Symptom, Patient, Tool>(); //Invoked when tool used adds a symptom to patient
    public static UnityEvent<Symptom, Patient, Tool> OnTryAddSymptom = new UnityEvent<Symptom, Patient, Tool>(); //Invoked when tool used adds a symptom to patient
    public static UnityEvent<Symptom, Patient, Tool> OnRemoveSymptom = new UnityEvent<Symptom, Patient, Tool>(); //Invoked when tool used removes a symptom from patient
    public static UnityEvent<Symptom, Patient, Tool> OnTryRemoveSymptom = new UnityEvent<Symptom, Patient, Tool>(); //Invoked when tool used removes a symptom from patient
    public static UnityEvent<Patient> OnCureDisease = new UnityEvent<Patient>(); //Invoked when patient's disease is cured
    //public static UnityEvent<GameObject> OnHealthChange = new UnityEvent<GameObject>();

    private void Start()
    {
        player = App.Instance.GameplayCore.PlayerManager.PickupController.GetPickupController().gameObject;
        isAlive = true;
        PatientManager.OnPatientSpawn.Invoke(this);
    }


    public void Death()
    {

        OnPatientDeath.Invoke(this);// Release the bed on death
        App.Instance.GameplayCore.GameManager.CheckDeathCounter();
        App.Instance.GameplayCore.GameManager.deathCounter+=1;
        App.Instance.GameplayCore.PlayerManager.PlayerHealth -= 25;
        PickupController.OnInteract.RemoveListener(InteractWithPatient);
        Destroy(this.gameObject); // if dead = destroy object
    }

    private void OnEnable()
    {
        PickupController.OnInteract.AddListener(InteractWithPatient);
        symptoms = new List<SicknessScriptableObject.SymptomStruct>();
    }
    private void OnDisable()
    {
        PickupController.OnInteract.RemoveListener(InteractWithPatient);
    }

    public void SetSickness(SicknessScriptableObject sickness)
    {
        this.sickness = sickness;
        if(sickness.stories.Count > 0)
            patientStory = sickness.stories[Random.Range(0, sickness.stories.Count - 1)];

        CopySymptoms(sickness);
    }

    public void CopySymptoms(SicknessScriptableObject sickness)
    {
        foreach(SicknessScriptableObject.SymptomStruct symptom in sickness.symptomList)
        {
            symptoms.Add(symptom);
        }
    }

    public bool FindSymptom(Symptom symptom)
    {
        foreach(var i in symptoms)
        {
            if (i.symptom == symptom)
                return true;
        }
        return false;
    }
    public void InsertSymptomToList(Symptom sympt)
    {
        SicknessScriptableObject.SymptomStruct symptomStruct = new SicknessScriptableObject.SymptomStruct
        {
            symptom = sympt,
            isHidden = false
        };

        symptoms.Add(symptomStruct);
    }


    public void InteractWithPatient(GameObject interactedObject, PickupController controller)
    {
        if (interactedObject != this.gameObject || controller.PickedItem == null)
            return;
        if(controller.PickedItem.GetComponent<Tool>() != null)
        {
            Tool.OnToolInteract.Invoke(controller.PickedItem, this);
        }
        
    }

}
