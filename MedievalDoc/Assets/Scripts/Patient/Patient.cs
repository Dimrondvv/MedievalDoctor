using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Patient : MonoBehaviour
{
    GameObject player;

    [SerializeField] public SicknessScriptableObject sickness;


    public int spawnerID;
    [SerializeField] SpawnPatientTimer SpawnPatientSpawner;

    //public int health;
    [SerializeField] private int health; // player Health (if =< 0 - game over)
    public int Health { get { return health; } set { health = value; } }

    [SerializeField] private int maxHealth; // player Health (if =< 0 - game over)
    public int HealthMax { get { return maxHealth; } set { maxHealth = value; } }

    public List<Symptom> additionalSymptoms = new List<Symptom>();

    private Dictionary<Symptom, string> discoveredSymptoms = new Dictionary<Symptom, string>(); //Key - symptom / Display value
    public Dictionary<Symptom, string> DiscoveredSymptoms { get { return discoveredSymptoms; } }
    public string patientStory;
    public bool isAlive;

    [SerializeField] private bool immune; // immunity for tests
    private string patientName;
    public string PatientName
    {
        get { return patientName; }
        set { patientName = value; }
    }
    public bool Immune { get { return immune; } set { immune = value; } }



    public static UnityEvent<GameObject> OnHealthChange = new UnityEvent<GameObject>();




    private void Start(){
        player = PlayerManager.Instance.PlayerController.GetPlayerController().gameObject;
        health = 100;
        maxHealth = 100;
        if (sickness)
            DiscoverNonCriticalSymptoms(this);
        else
            Debug.LogError("No sickness");
    }


    public void Death()
    {
        SpawnPatientSpawner.SpawnPoints[spawnerID].GetComponent<Chair>().isOccupied = false;
        GameManager.Instance.deathCounter+=1;
        Debug.Log(GameManager.Instance.deathCounter);
        PlayerManager.Instance.PlayerHealth -= 10;
        //Debug.Log(PlayerManager.Instance.PlayerHealth);
        Destroy(this.gameObject); // if dead = destroy object
    }

    private void OnEnable()
    {

        PlayerController.OnInteract.AddListener(InteractWithPatient);
        if (PatientEventManager.Instance != null)
        {
            PatientEventManager.Instance.OnCheckSymptom.AddListener(DiscoverSymptom);
            PatientEventManager.Instance.OnTryAddSymptom.AddListener(AddAdditionalSymptom);
            PatientEventManager.Instance.OnTryRemoveSymptom.AddListener(RemoveDiscoveredSymptom);
            PatientEventManager.Instance.OnRemoveSymptom.AddListener(CheckIfCured);

        }
    }
    private void OnDisable()
    {
        PlayerController.OnInteract.RemoveListener(InteractWithPatient);
    }
    public void InteractWithPatient(GameObject interactedObject, PlayerController controller)
    {
        if (interactedObject != this.gameObject)
            return;
        if(controller.PickedItem == null)
        {
            PatientEventManager.Instance.OnHandInteract.Invoke(this);
        }
        else if(controller.PickedItem.GetComponent<Tool>() != null)
        {
            PatientEventManager.Instance.OnToolInteract.Invoke(controller.PickedItem, this);
        }
        
    }

    private bool CanSymptomBeCured(Symptom symptom)
    {
        foreach (var item in sickness.solutionList)
        {
            if (item.symptom == symptom)
            {
                foreach (var sympt in item.symptomsNotPresentToCure)
                {
                    bool isPresent = sickness.CheckSymptom(sympt);
                    if (isPresent || additionalSymptoms.Contains(sympt))
                    {
                        return false;
                    }
                }
                foreach (var sympt in item.symptomsPresentToCure)
                {
                    bool isPresent = sickness.CheckSymptom(sympt);
                    if (!isPresent && !additionalSymptoms.Contains(sympt))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    private void RemoveDiscoveredSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != this)
            return;
        bool isRemoved = patient.sickness.RemoveSymptom(symptom);
        if (!isRemoved) //If the symptom is not removed from sickness try removing it from additional symptoms
        {
            if (CanSymptomBeCured(symptom) == false)
                return;

            if (sickness.CheckSymptom(symptom) == true)
            {
                sickness.RemSymptom(symptom);
                patient.DiscoveredSymptoms.Remove(symptom);
                PatientEventManager.Instance.OnRemoveSymptom.Invoke(symptom, patient, tool);
            }
            else
            {
                patient.additionalSymptoms.Remove(symptom);
                patient.DiscoveredSymptoms.Remove(symptom);
                PatientEventManager.Instance.OnRemoveSymptom.Invoke(symptom, patient, tool);
            }
        }
        else
        {
            patient.DiscoveredSymptoms.Remove(symptom);
            PatientEventManager.Instance.OnRemoveSymptom.Invoke(symptom, patient, tool);
        }

    }

    private void AddAdditionalSymptom(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != this || sickness.CheckSymptom(symptom))
            return;

        foreach(var item in tool.SymptomsRemoved)
        {
            if (CanSymptomBeCured(item) == false)
                return;
        }

        if (!patient.additionalSymptoms.Contains(symptom)) //prevent duplicate symptoms
        {
            patient.additionalSymptoms.Add(symptom);
            patient.DiscoveredSymptoms.Add(symptom, symptom.symptomName + " (+)");
            PatientEventManager.Instance.OnAddSymptom.Invoke(symptom, patient, tool);
        }
    }

    private void DiscoverNonCriticalSymptoms(Patient patient)
    {
        if (patient != this || DiscoveredSymptoms.Count != 0)
            return;

        foreach(var symptom in sickness.symptomList)
        {
            if (!symptom.isHidden)
                DiscoveredSymptoms.Add(symptom.symptom, symptom.GetSymptomName());
            else
                DiscoveredSymptoms.Add(symptom.symptom, "?");
        }
    }
    private void DiscoverSymptom(Symptom symptom, Patient patient)
    {
        if (patient != this)
            return;
        patient.DiscoveredSymptoms[symptom] = symptom.symptomName;
    }
    private void CheckIfCured(Symptom symptom, Patient patient, Tool tool)
    {
        if (patient != this || tool.SymptomsAdded.Count > 0)
            return;

        bool noAdditionalSymptoms = additionalSymptoms.Count == 0;
        bool solutionMet = true;
        foreach(SicknessScriptableObject.SolutionStruct symptomCheck in sickness.solutionList)
        {
            foreach(SicknessScriptableObject.SymptomStruct symptomStruct in sickness.symptomList)
            {
                if (symptomStruct.symptom == symptomCheck.symptom)
                {
                    solutionMet = false;
                    Debug.Log($"Symp checked for: {symptomCheck} Symp found: {symptomStruct.symptom}");
                }
            }
        }
        bool isCured = noAdditionalSymptoms && solutionMet;
        if (isCured)
        {
            Debug.Log("Cured");
            PatientEventManager.Instance.OnCureDisease.Invoke(this);
        }
    }

}
