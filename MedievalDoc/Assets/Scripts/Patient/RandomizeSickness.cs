using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSickness : MonoBehaviour
{
    SpawnPatientTimer SpawnPatientTimer;
    static int sicknessCount = 0;
    private int sicknessID;
    private int storyID;

    public void RandomizeSicknessFunction()
    {
        sicknessID = sicknessCount++;
        storyID = Random.Range(0, SpawnPatientTimer.Sicknesses[sicknessID].stories.Count);

        if (sicknessCount >= SpawnPatientTimer.Sicknesses.Count)
            sicknessCount = 0;

        SicknessScriptableObject sicknessCopy = Instantiate(SpawnPatientTimer.Sicknesses[sicknessID]);
        Patient spawnedPatient = SpawnPatientTimer.SpawnedPatient.GetComponent<Patient>();
        spawnedPatient.Sickness = sicknessCopy;
        spawnedPatient.patientStory = sicknessCopy.stories[storyID];
        spawnedPatient.PatientName = SpawnPatientTimer.PatientNames[Random.Range(0, SpawnPatientTimer.PatientNames.Count)];
        spawnedPatient.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV();
    }

    private void Awake()
    {
        SpawnPatientTimer = GetComponent<SpawnPatientTimer>();
        sicknessCount = 0;
    }
}
