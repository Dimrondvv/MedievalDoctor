using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPatientTimer : MonoBehaviour
{
    public float elapsedTime;
    float timeRemaining = 1;
    //[SerializeField] int countdown;
    [SerializeField] GameObject Patient;
    [SerializeField] int SpawnTime; // Timer for patient spawning
    GameObject SpawnedPatient;
    [SerializeField] List<SicknessScriptableObject> Sicknesses;

    [SerializeField] List<GameObject> SpawnPoints;


    private List<GameObject> OccupiedSpawners;
    private List<GameObject> AvailableSpawners;

    private int sicknessID;
    private int deathTimer;

    private int spawnerID;

    [SerializeField] int maxPatientCounter;
    private int currentSpawnedPatients;

    void Spawning()
    {

        CheckSpawners();

        if (AvailableSpawners.Count == 0) {
            Debug.Log("No available chairs for patients.");  
        }

        if (currentSpawnedPatients == maxPatientCounter)
        {
            Debug.Log("Maximum number of patients spawned");
        }

        else {
            Debug.Log("Spawning patient.");
            spawnerID = Random.Range(0, AvailableSpawners.Count);

            SpawnedPatient = Instantiate(Patient, AvailableSpawners[spawnerID].transform.position, Quaternion.identity);

            AvailableSpawners[spawnerID].GetComponent<Chair>().isOccupied = true;

            RandomizeSickness();

            SpawnedPatient.GetComponent<DeathTimer>().elapsedTime = 0; // time of his life set to 0
            SpawnedPatient.GetComponent<DeathTimer>().countdown = deathTimer; // for how long he can live without help
            SpawnedPatient.GetComponent<Patient>().sickness = Sicknesses[sicknessID];
            CheckSpawners();
            currentSpawnedPatients += 1;
            Debug.Log("Spawned patients:"+currentSpawnedPatients);
        }
    }


    void CheckSpawners()
    {
        OccupiedSpawners = new List<GameObject>();
        AvailableSpawners = new List<GameObject>();
        foreach (GameObject chair in SpawnPoints)
        {

            if (chair.GetComponent<Chair>().isOccupied == false)
            {
                AvailableSpawners.Add(chair);
            }
            else
            {
                OccupiedSpawners.Add(chair);
            }
        }
    }



    void Update()
    {
        OneSecondTimer();
    }

    void RandomizeSickness()
    {
        sicknessID = Random.Range(0, Sicknesses.Count);

        deathTimer = Random.Range(Sicknesses[sicknessID].timeToDieMin, Sicknesses[sicknessID].timeToDieMax);

        Debug.Log("Sickness:"+ Sicknesses[sicknessID].sicknessName + "Time to die:"+deathTimer);

    }



    void OneSecondTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            elapsedTime += 1;
            timeRemaining = 1;
            TimeCheck();
        }
    }

    void TimeCheck()
    {
        //Debug.Log(elapsedTime);

        if (elapsedTime > 1)
        {

            if (elapsedTime % SpawnTime == 0)
            {
                Spawning();
            }
        }
    }



}
