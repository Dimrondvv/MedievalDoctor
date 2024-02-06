using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Patient : MonoBehaviour
{
    GameObject player;

    [SerializeField] public SicknessScriptableObject sickness;
    public int spawnerID;

    [SerializeField] private int health; // player Health (if =< 0 - game over)
    public int Health { get { return health; } set { health = value; } }

    [SerializeField] private int maxHealth; // player Health (if =< 0 - game over)
    public int HealthMax { get { return maxHealth; } set { maxHealth = value; } }

    private List<Symptom> additionalSymptoms = new List<Symptom>();
    public List<Symptom> AdditionalSymptoms { get { return additionalSymptoms} set { additionalSymptoms = value; } }

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

    private void Start()
    {
        player = PlayerManager.Instance.PlayerController.GetPlayerController().gameObject;
        isAlive = true;
    }


    public void Death()
    {
        // Release the Chair if patients dies on it (Fix)
        if (spawnerID >= 0)
        {
            SpawnPatientTimer.SpawnPoints[spawnerID].GetComponent<Chair>().IsOccupied = false;
        }

        PatientEventManager.Instance.OnPatientDeath.Invoke(this);// Release the bed on death
        GameManager.Instance.CheckDeathCounter();
        GameManager.Instance.deathCounter+=1;
        PlayerManager.Instance.PlayerHealth -= 25;
        Destroy(this.gameObject); // if dead = destroy object
    }

    private void OnEnable()
    {
        PlayerController.OnInteract.AddListener(InteractWithPatient);
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

}
