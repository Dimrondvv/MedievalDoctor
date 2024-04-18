using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPatientTimer : MonoBehaviour
{
    private float elapsedTime;
    public float ElapsedTime
    {
        get { return elapsedTime; }
        set { elapsedTime = value; }
    }

    [SerializeField] GameObject Patient;

    [SerializeField] private int spawnTime; // Timer for patient spawning
    public int SpawnTime 
    { 
        get { return spawnTime; } 
        set { spawnTime = value; } 
    }

    private int spawnerID;
    public int SpawnerID
    {
        get { return spawnerID; }
        set { spawnerID = value; }
    }


    private GameObject spawnedPatient;
    public GameObject SpawnedPatient
    {
        get { return spawnedPatient; }
        set { spawnedPatient = value; }
    }



    [SerializeField] private List<SicknessScriptableObject> sicknesses;
    public List<SicknessScriptableObject> Sicknesses
    {
        get { return sicknesses; }
        set { sicknesses = value; }
    }

    [SerializeField] private List<GameObject> SpawnPointsCopy;
    private static List<GameObject> spawnPoints;
    public static List<GameObject> SpawnPoints
    {
        get { return spawnPoints; }
        set { spawnPoints = value; }
    }

    [SerializeField] private List<string> patientNames;
    public List<string> PatientNames
    {
        get { return patientNames; }
        set { patientNames = value; }
    }





    private RandomizeSickness randomizeSickness;
    [SerializeField] private int maxPatientCounter;
    private int currentSpawnedPatients;
    private int availableSpawners;


    public static UnityEvent<Patient> OnPatientSpawn = new UnityEvent<Patient>(); //Invoked when tool is used to check for symptom


    void Start()
    {
        InvokeRepeating("TimeCheck", 0, 1);
        randomizeSickness = GetComponent<RandomizeSickness>();
        Spawning(); // Spawn patient at the start of the game
    }

    private void Awake()
    {
        SpawnPoints = SpawnPointsCopy;
    }
    private void Update()
    {
        SpawnOnKeyPress();
    }
    private void Spawning()
    {
        CheckSpawners();
        if (availableSpawners == 0)
        {
        }

        else if (currentSpawnedPatients == maxPatientCounter)
        {
        }
        else
        {
            TrySpawning();
        }
        availableSpawners = 0;
    }

    public Patient TrySpawning()
    {
        spawnerID = Random.Range(0, SpawnPoints.Count);

        if (SpawnPoints[spawnerID].GetComponent<Chair>().IsOccupied == true)
        {
            TrySpawning();
        }
        else
        {
            Patient.GetComponent<Patient>().SpawnerID = spawnerID;
            spawnedPatient = Instantiate(Patient, spawnPoints[spawnerID].transform.position, Quaternion.identity);
            App.Instance.GameplayCore.PatientManager.patients.Add(spawnedPatient.GetComponent<Patient>());   
            spawnedPatient.SetActive(true);
            spawnedPatient.GetComponent<Patient>().Health = GetComponent<Patient>().HealthMax;
            spawnedPatient.GetComponent<Patient>().Immune = false;
            spawnedPatient.GetComponent<PatientDamage>().enabled = true;
            spawnPoints[spawnerID].GetComponent<Chair>().IsOccupied = true;
            spawnPoints[spawnerID].GetComponentInChildren<Chair>().IsOccupied = true;
            randomizeSickness.RandomizeSicknessFunction();
            OnPatientSpawn.Invoke(spawnedPatient.GetComponent<Patient>());
            currentSpawnedPatients += 1;
        }
        return spawnedPatient.GetComponent<Patient>();
    }

    private void CheckSpawners()
    {
        availableSpawners = 0;
        foreach (GameObject chair in spawnPoints) if (chair.GetComponent<Chair>().IsOccupied == false)
            {
                availableSpawners += 1;
            }
    }
    private void TimeCheck()
    {
        float time = App.Instance.GameplayCore.TimerManager.ElapsedTime;
        if (time > 1)
        {
            if (time % SpawnTime == 0)
            {
                if (App.Instance.GameplayCore.GameManager.IsNight)
                {
                    // Night = no patient spawned
                }
                else
                {
                    Spawning();
                }
            }
        }
    }

    [ExecuteInEditMode] private void SpawnOnKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Spawning();
        }
    }

}
