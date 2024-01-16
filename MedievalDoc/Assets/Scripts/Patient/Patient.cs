using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private List<string> discoveredSymptoms = new List<string>();
    public List<string> DiscoveredSymptoms { get { return discoveredSymptoms; } }
    public string patientStory;
    public bool isAlive;

    [SerializeField] private bool immune; // immunity for tests
    public bool Immune { get { return immune; } set { immune = value; } }

    

    private void Start(){
        health = 100;
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
            PatientEventManager.Instance.OnHandInteract.AddListener(DiscoverNonCriticalSymptoms);
            PatientEventManager.Instance.OnCheckSymptom.AddListener(DiscoverSymptom);
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
        else if(controller.PickedItem.GetComponent<SnapBlueprint>() != null)
        {
            PatientEventManager.Instance.OnToolInteract.Invoke(controller.PickedItem, this);
        }
    }

    private void DiscoverNonCriticalSymptoms(Patient patient)
    {
        if (patient != this || DiscoveredSymptoms.Count != 0)
            return;
        foreach(var symptom in sickness.symptomList)
        {
            if (!symptom.isCritical)
                DiscoveredSymptoms.Add(symptom.GetSymptomName());
            else
                DiscoveredSymptoms.Add("?");
        }
    }
    private void DiscoverSymptom(Symptom symptom, Patient patient)
    {
        if (patient != this)
            return;
        for(int i = 0; i < patient.DiscoveredSymptoms.Count; i++)
        {
            if(patient.DiscoveredSymptoms[i] == "?")
            {
                patient.DiscoveredSymptoms[i] = symptom.symptomName; 
            }
        }
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
