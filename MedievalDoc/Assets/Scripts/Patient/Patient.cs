﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Data;

public class Patient : MonoBehaviour
{
    [SerializeField] private Sickness sickness;
    [SerializeField] private int health; // player Health (if =< 0 - game over)
    [SerializeField] private int maxHealth; // player Health (if =< 0 - game over)
    [SerializeField] private bool immune; // immunity for tests
    [SerializeField] private int spawnerID;
    [SerializeField] public int maximumAnger;

    public List<Symptom> symptoms = new List<Symptom>();
    public List<Symptom> removedSymptoms = new List<Symptom>();
    public List<Symptom> Symptoms { get { return symptoms; } set { symptoms = value; } }
    public string patientStory;
    private bool isAlive;
    private string patientName;
    private int angryMeter;
    private bool isQuitting;
    private bool tiltproof;

    public static UnityEvent<Patient> OnPatientDeath = new UnityEvent<Patient>();
    public static UnityEvent<Symptom, Patient> OnCheckSymptom = new UnityEvent<Symptom, Patient>(); //Invoked when tool is used to check for symptom
    public static UnityEvent<Symptom, Patient, Tool> OnAddSymptom = new UnityEvent<Symptom, Patient, Tool>(); //Invoked when tool used adds a symptom to patient
    public static UnityEvent<Symptom, Patient, Tool> OnTryAddSymptom = new UnityEvent<Symptom, Patient, Tool>(); //Invoked when tool used adds a symptom to patient
    public static UnityEvent<Symptom, Patient, Tool> OnRemoveSymptom = new UnityEvent<Symptom, Patient, Tool>(); //Invoked when tool used removes a symptom from patient
    public static UnityEvent<Symptom, Patient, Tool> OnTryRemoveSymptom = new UnityEvent<Symptom, Patient, Tool>(); //Invoked when tool used removes a symptom from patient
    public static UnityEvent<Patient> OnCureDisease = new UnityEvent<Patient>(); //Invoked when patient's disease is cured
    public Sickness Sickness { get { return sickness; } set { sickness = value; } }
    public int SpawnerID { get { return spawnerID; } set { spawnerID = value; } }
    public bool IsQuitting { get { return isQuitting; } set { isQuitting = value; } }
    public bool Tiltproof { get { return tiltproof; } set { tiltproof = value; } }
    public string PatientName { get { return patientName; } set { patientName = value; } }
    public int AngryMeter { get { return angryMeter; } }
    public int MaximumAnger { get { return maximumAnger; } }
    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
    public bool Immune { get { return immune; } set { immune = value; } }
    public int Health { get { return health; } set { health = value; } }
    public int HealthMax { get { return maxHealth; } set { maxHealth = value; } }
    private void Start()
    {
        isAlive = true;
        immune = true;
        isQuitting = false;
        PatientManager.OnPatientSpawn.Invoke(this);
        PatientManager.OnPatientReleased.AddListener(ReleasePatient);
    }

    private void OnEnable()
    {
        PickupController.OnInteract.AddListener(InteractWithPatient);
        symptoms = new List<Symptom>();
    }

    private void OnDisable()
    {
        PickupController.OnInteract.RemoveListener(InteractWithPatient);
    }

    public void IncreaseMaddness(int value)
    {
        angryMeter += value;
    }

    public void Death()
    {
        OnPatientDeath.Invoke(this);
        App.Instance.GameplayCore.PatientManager.patients.Remove(this);
        App.Instance.GameplayCore.GameManager.CheckDeathCounter();
        App.Instance.GameplayCore.GameManager.deathCounter += 1;
        PickupController.OnInteract.RemoveListener(InteractWithPatient);
        Destroy(this.gameObject);
        UpdatePlayerScore();
    }

    public void Upset()
    {
        OnPatientDeath.Invoke(this);
        App.Instance.GameplayCore.GameManager.CheckDeathCounter();
        App.Instance.GameplayCore.GameManager.MadCounter += 1;
        PickupController.OnInteract.RemoveListener(InteractWithPatient);
    }

    public void RageQuit()
    {
        PatientManager.RageQuitPatient.Invoke();
        Upset();
        UpdatePlayerScore();
        Debug.Log("Im Leaving >:(");
    }

    public void SetSickness(Sickness sickness)
    {
        if(this.sickness != null)
        {
            this.sickness = null;
            symptoms.Clear();
            patientStory = null;
        }

        this.sickness = sickness;
        patientStory = sickness.sicknessStory;

        CopySymptoms(sickness);
    }

    public void CopySymptoms(Sickness sickness)
    {
        foreach(string symptomKey in sickness.symptomsContainerList)
        {
            /*GameObject sympt = PatientSymptomHandler.FindSymptomObject(this, symptom);
            if(sympt != null)
                sympt.SetActive(true);
            if (symptom.partOverride != null)
            {
                GameObject loc = PatientSymptomHandler.FindLocationObject(this, symptom.localization);
                if(loc != null)
                    loc.SetActive(false);
            }*/
            symptoms.Add(HelperFunctions.SymptomLookup(symptomKey));
        }
    }

    public bool FindSymptom(Symptom symptom)
    {
        foreach(var sympt in symptoms)
        {
            if (sympt == symptom)
                return true;
        }
        return false;
    }
    public void InsertSymptomToList(Symptom sympt, Localization local = Localization.None)
    {
        symptoms.Add(sympt);
    }

    private int CalculateSCore()
    {
        int score = 0;



        foreach(var symptom in symptoms)
        {
            try
            {
                score -= System.Int32.Parse(symptom.symptomPoints);
            }
            catch
            {
                Debug.LogError($"Unable to convert {symptom.symptomPoints} to a number");
            }
        }

        foreach (var symptom in removedSymptoms)
        {
            try
            {
                score += System.Int32.Parse(symptom.symptomPoints);
            }
            catch
            {
                Debug.LogError($"Unable to convert {symptom.symptomPoints} to a number");
            }
        }
        return score;
    }

    public void InteractWithPatient(GameObject interactedObject, PickupController controller)
    {
        Debug.Log(interactedObject, controller);
        if (interactedObject != this.gameObject || controller.PickedItem == null)
            return;
        if(controller.PickedItem.GetComponent<Tool>() != null)
        {
            Tool.OnToolInteract.Invoke(controller.PickedItem, this);
        }
    }

    private void UpdatePlayerScore()
    {
        int score = CalculateSCore();
        PlayerManager playerManager = App.Instance.GameplayCore.PlayerManager;
        int hpModifier = playerManager.scoreToHpModifier;
        int cashModifier = playerManager.scoreToCashModifier;

        playerManager.UpdateStats(score / hpModifier, score, score / cashModifier); //Updat stats with values of score divided by respective modifiers
    }

    private void ReleasePatient(Patient patient)
    {
        if (patient != this)
            return;
        UpdatePlayerScore();
    }
}
