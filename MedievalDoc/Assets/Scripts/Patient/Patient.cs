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

    public List<SicknessScriptableObject.SymptomStruct> symptoms;
    public List<SicknessScriptableObject.SymptomStruct> Symptoms { get { return symptoms; } set { symptoms = value; } }

    private Dictionary<Symptom, string> discoveredSymptoms = new Dictionary<Symptom, string>(); //Key - symptom / Display value
    public Dictionary<Symptom, string> DiscoveredSymptoms { get { return discoveredSymptoms; } }
    public string patientStory;


    private bool isAlive;
    public bool IsAlive { get { return isAlive; } set {  isAlive = value; } }

    [SerializeField] private bool immune; // immunity for tests
    public bool Immune { get { return immune; } set { immune = value; } }

    private string patientName;
    public string PatientName
    {
        get { return patientName; }
        set { patientName = value; }
    }

    private int angryMeter;
    public int AngryMeter { get { return angryMeter; } }
    [SerializeField] public int maximumAnger;
    public int MaximumAnger { get { return maximumAnger; } }

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
        player = App.Instance.GameplayCore.PlayerManager.playerObject;
        isAlive = true;
        PatientManager.OnPatientSpawn.Invoke(this);
        PatientManager.OnPatientReleased.AddListener(ReleasePatient);
    }

    public void IncreaseMaddness(int value)
    {
        angryMeter += value;
    }

    public void Death()
    {
        OnPatientDeath.Invoke(this);
        App.Instance.GameplayCore.GameManager.CheckDeathCounter();
        App.Instance.GameplayCore.GameManager.deathCounter+=1;
        PickupController.OnInteract.RemoveListener(InteractWithPatient);
    }

    public void RageQuit()
    {
        Death();
        Debug.Log("Im Leaving >:(");
        Destroy(gameObject);
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

    private int CalculateSCore()
    {
        int score = 0;

        foreach(var symptom in symptoms)
        {
            score += symptom.symptom.score;
        }

        return score;
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
        Destroy(gameObject); //TODO - animation of walking out
    }
}
