using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPatientTimer : MonoBehaviour
{
    public float elapsedTime;
    [SerializeField] GameObject Patient;
    [SerializeField] int SpawnTime; // Timer for patient spawning
    public GameObject SpawnedPatient;
    [SerializeField] public List<SicknessScriptableObject> Sicknesses;

    [SerializeField] public List<GameObject> SpawnPointsCopy;
    public static List<GameObject> SpawnPoints;

    [SerializeField] public List<string> patientNames;
    public int spawnerID;
    RandomizeSickness randomizeSickness;
    [SerializeField] int maxPatientCounter;
    private int currentSpawnedPatients;
    private int availableSpawners;

    void Start()
    {
        InvokeRepeating("TimeCheck", 2, 1);
        randomizeSickness = GetComponent<RandomizeSickness>();
    }

    private void Awake()
    {
        SpawnPoints = SpawnPointsCopy;
    }

    void Spawning()
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

    void TrySpawning()
    {
        spawnerID = Random.Range(0, SpawnPoints.Count);

        if (SpawnPoints[spawnerID].GetComponent<Chair>().IsOccupied == true)
        {
            TrySpawning();
        }
        else
        {
            Patient.GetComponent<Patient>().spawnerID = spawnerID;
            SpawnedPatient = Instantiate(Patient, SpawnPoints[spawnerID].transform.position, Quaternion.identity);
            PatientManager.Instance.patients.Add(SpawnedPatient.GetComponent<Patient>());   
            SpawnedPatient.SetActive(true);
            SpawnedPatient.GetComponent<PatientDamage>().enabled = true;
            SpawnPoints[spawnerID].GetComponent<Chair>().IsOccupied = true;
            SpawnPoints[spawnerID].GetComponentInChildren<Chair>().IsOccupied = true;

            //.transform.SetParent(pickupPoint);
            
            randomizeSickness.RandomizeSicknessFunction();
            currentSpawnedPatients += 1;
        }
    }

    void CheckSpawners()
    {
        availableSpawners = 0;
        foreach (GameObject chair in SpawnPoints) if (chair.GetComponent<Chair>().IsOccupied == false)
            {
                availableSpawners += 1;
            }
    }
    void TimeCheck()
    {
        if (TimerManager.Instance.ElapsedTime > 1)
        {
            if (TimerManager.Instance.ElapsedTime % SpawnTime == 0)
            {
                Spawning();
            }
        }
    }
}
