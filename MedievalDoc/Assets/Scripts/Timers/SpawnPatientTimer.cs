using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPatientTimer : MonoBehaviour
{
    public float elapsedTime;
    [SerializeField] GameObject Patient;
    [SerializeField] int SpawnTime; // Timer for patient spawning
    GameObject SpawnedPatient;
    [SerializeField] List<SicknessScriptableObject> Sicknesses;

    [SerializeField] public List<GameObject> SpawnPoints;

    private int sicknessID;
    private int spawnerID;
    public int storyID;


    [SerializeField] int maxPatientCounter;

    [SerializeField] Timer Timer;
    private int currentSpawnedPatients;

    private int availableSpawners;

    public int damageFromSymptoms;


    void Start()
    {
        InvokeRepeating("TimeCheck", 2, 1);
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
            SpawnPoints[spawnerID].GetComponent<Chair>().isOccupied = true;
            RandomizeSickness();

            SpawnedPatient.GetComponent<DeathTimer>().elapsedTime = 0;
            SpawnedPatient.GetComponent<Patient>().sickness = Sicknesses[sicknessID];

            string story = string.Empty;

            for (int i = 0; i < Sicknesses[sicknessID].stories.Count; i++)
            {
                story += Sicknesses[sicknessID].stories[i] + "\n";
            }
            SpawnedPatient.GetComponent<Patient>().patientStory = story;


            for (int i = 0; i < Sicknesses[sicknessID].symptomList.Count; i++)
            {
                SpawnedPatient.GetComponent<DeathTimer>().damage += Sicknesses[sicknessID].symptomList[i].symptom.damage;
            }
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
    void RandomizeSickness()
    {
        sicknessID = Random.Range(0, Sicknesses.Count);
        storyID = Random.Range(0, Sicknesses[sicknessID].stories.Count);
    }

    void TimeCheck()
    {
        Debug.Log(Timer.elapsedTime);
        if (Timer.elapsedTime > 1)
        {

            if (Timer.elapsedTime % SpawnTime == 0)
            {
                Spawning();
            }
        }
    }



}
