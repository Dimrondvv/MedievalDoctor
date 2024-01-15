using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSickness : MonoBehaviour
{
    SpawnPatientTimer spawnPatientTimer;
    private int sicknessID;
    private int storyID;

    public void RandomizeSicknessFunction()
    {
        sicknessID = Random.Range(0, spawnPatientTimer.Sicknesses.Count);
        storyID = Random.Range(0, spawnPatientTimer.Sicknesses[sicknessID].stories.Count);


        SicknessScriptableObject sicknessCopy = Instantiate(spawnPatientTimer.Sicknesses[sicknessID]);
        spawnPatientTimer.SpawnedPatient.GetComponent<Patient>().sickness = sicknessCopy;
        spawnPatientTimer.SpawnedPatient.GetComponent<Patient>().patientStory = sicknessCopy.stories[storyID];
    }

    private void Awake()
    {
        spawnPatientTimer = GetComponent<SpawnPatientTimer>();
    }
}
