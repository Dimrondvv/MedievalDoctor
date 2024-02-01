using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSickness : MonoBehaviour
{
    SpawnPatientTimer spawnPatientTimer;
    static int sicknessCount = 0;
    private int sicknessID;
    private int storyID;

    public void RandomizeSicknessFunction()
    {
        sicknessID = sicknessCount++;
        storyID = Random.Range(0, spawnPatientTimer.Sicknesses[sicknessID].stories.Count);

        if (sicknessCount >= spawnPatientTimer.Sicknesses.Count)
            sicknessCount = 0;

        SicknessScriptableObject sicknessCopy = Instantiate(spawnPatientTimer.Sicknesses[sicknessID]);
        Patient spawnedPatient = spawnPatientTimer.SpawnedPatient.GetComponent<Patient>();
        spawnedPatient.sickness = sicknessCopy;
        spawnedPatient.patientStory = sicknessCopy.stories[storyID];
        spawnedPatient.PatientName = spawnPatientTimer.patientNames[Random.Range(0, spawnPatientTimer.patientNames.Count)];
        spawnedPatient.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV();
    }

    private void Awake()
    {
        spawnPatientTimer = GetComponent<SpawnPatientTimer>();
    }
}
