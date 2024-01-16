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
    [SerializeField] public List<GameObject> SpawnPoints;
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

    void Spawning()
    {
        CheckSpawners();
        if (availableSpawners == 0)
        {
            Debug.Log("No available chairs for patients.");
        }

        else if (currentSpawnedPatients == maxPatientCounter)
        {
            Debug.Log("Maximum ammount of Patients has been reached.");
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

        if (SpawnPoints[spawnerID].GetComponent<Chair>().isOccupied == true)
        {
            TrySpawning();
        }
        else
        {
            Patient.GetComponent<Patient>().spawnerID = spawnerID;
            SpawnedPatient = Instantiate(Patient, SpawnPoints[spawnerID].transform.position, Quaternion.identity);
            SpawnedPatient.GetComponent<PatientDamage>().enabled = true;
            SpawnPoints[spawnerID].GetComponent<Chair>().isOccupied = true;
            randomizeSickness.RandomizeSicknessFunction();
            currentSpawnedPatients += 1;
        }
    }

    void CheckSpawners()
    {
        availableSpawners = 0;
        foreach (GameObject chair in SpawnPoints) if (chair.GetComponent<Chair>().isOccupied == false)
            {
                availableSpawners += 1;
            }
    }

    void FreeUpChair(Patient patient)
    {
        SpawnPoints[patient.spawnerID].GetComponent<Chair>().isOccupied = false;
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
