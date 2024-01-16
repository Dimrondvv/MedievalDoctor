using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Patient : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] public SicknessScriptableObject sickness;

    List<GameObject> usedItems;

    public int spawnerID;
    [SerializeField] SpawnPatientTimer SpawnPatientSpawner;

    //public int health;
    [SerializeField] private int health; // player Health (if =< 0 - game over)
    public int Health { get { return health; } set { health = value; } }

    [SerializeField] private int maxHealth; // player Health (if =< 0 - game over)
    public int HealthMax { get { return maxHealth; } set { maxHealth = value; } }

    private List<Symptom> additionalSymptoms = new List<Symptom>();

    private Dictionary<Symptom, string> discoveredSymptoms = new Dictionary<Symptom, string>(); //Key - symptom / Display value
    public Dictionary<Symptom, string> DiscoveredSymptoms { get { return discoveredSymptoms; } }
    public string patientStory;
    public bool isAlive;

    [SerializeField] private bool immune; // immunity for tests
    public bool Immune { get { return immune; } set { immune = value; } }



    public static UnityEvent<GameObject> OnHealthChange = new UnityEvent<GameObject>();




    private void Start(){
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
            PatientEventManager.Instance.OnRemoveSymptom.AddListener(RemoveDiscoveredSymptom);
            PatientEventManager.Instance.OnAddSymptom.AddListener(AddAdditionalSymptom);
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

    private void RemoveDiscoveredSymptom(Symptom symptom, Patient patient)
    {
        if (patient != this)
            return;
        patient.DiscoveredSymptoms.Remove(symptom);
    }

    private void AddAdditionalSymptom(Symptom symptom, Patient patient)
    {
        if (patient != this)
            return;

        patient.additionalSymptoms.Add(symptom);
        patient.DiscoveredSymptoms.Add(symptom, symptom.symptomName + " (+)");
    }

    private void DiscoverNonCriticalSymptoms(Patient patient)
    {
        if (patient != this || DiscoveredSymptoms.Count != 0)
            return;

        foreach(var symptom in sickness.symptomList)
        {
            if (!symptom.isCritical)
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

    
    private void Awake()
    {
        usedItems = new List<GameObject>();
        
    }
    private int CompareItems() //Compares items used on a patient to the items needed to cure, returns 0 if wrong item is used, 1 if
    {                          //the items so far are correct, and 3 if all the items are correct and requirements for curement are met
        for (int i = 0; i < usedItems.Count; i++)
        {
            if (usedItems[i].name != sickness.toolsRequired[i].name)
                return 0;
            else if(i == sickness.toolsRequired.Count - 1 && usedItems[i].name == sickness.toolsRequired[i].name)
            {
                return 2;
            }
        }
        return 1;
    }
}
