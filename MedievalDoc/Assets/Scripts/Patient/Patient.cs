using System.Collections;
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
        player = PlayerManager.Instance.PickupController.GetPickupController().gameObject;
        isAlive = true;
    }


    public void Death()
    {
        // Release the Chair if patients dies on it (Fix)
        if (spawnerID >= 0)
        {
            SpawnPatientTimer.SpawnPoints[spawnerID].GetComponent<Chair>().IsOccupied = false;
        }

        OnPatientDeath.Invoke(this);// Release the bed on death
        GameManager.Instance.CheckDeathCounter();
        GameManager.Instance.deathCounter+=1;
        PlayerManager.Instance.PlayerHealth -= 25;
        Destroy(this.gameObject); // if dead = destroy object
    }

    private void OnEnable()
    {
        PickupController.OnInteract.AddListener(InteractWithPatient);
    }
    private void OnDisable()
    {
        PickupController.OnInteract.RemoveListener(InteractWithPatient);
    }
    public void InteractWithPatient(GameObject interactedObject, PickupController controller)
    {
        if (interactedObject != this.gameObject)
            return;
        if(controller.PickedItem.GetComponent<Tool>() != null)
        {
            Tool.OnToolInteract.Invoke(controller.PickedItem, this);
        }
        
    }

}
