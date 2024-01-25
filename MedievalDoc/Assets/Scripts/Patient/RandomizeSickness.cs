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
        Patient spawnedPatient = spawnPatientTimer.SpawnedPatient.GetComponent<Patient>();
        spawnedPatient.sickness = sicknessCopy;
        spawnedPatient.patientStory = sicknessCopy.stories[storyID];
        spawnPatientTimer.SpawnedPatient.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV();

    }

    private void Awake()
    {
        spawnPatientTimer = GetComponent<SpawnPatientTimer>();
    }
}
