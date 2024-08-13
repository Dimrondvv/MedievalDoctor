using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
public class InitializePatientStats : MonoBehaviour
{
    private List<Sickness> sicknessPool;
    private string sicknessContainersKey;
    private int sicknessCount;

    void Start()
    {
        PatientManager.OnPatientSpawn.AddListener(SetPatientStats);
        sicknessPool = new List<Sickness>();
        sicknessContainersKey = Data.ImportJsonData.levelConfig[LevelButtons.levelID - 1].sicknessContainer;
        sicknessCount = 0;
    }

    

    private void SetPatientStats(Patient patient)
    {
        if (Data.ImportJsonData.sicknessContainersConfig.Length > (LevelButtons.levelID - 1)) { // Check if sickness container exists for this level
            foreach (var sickness in HelperFunctions.SicknessContainersLookup(sicknessContainersKey).levelSicknesses) {
                sicknessPool.Add(HelperFunctions.SicknessLookup(sickness));
                Debug.Log(sickness);
            }
        }

        if (sicknessCount >= sicknessPool.Count)
        {
            sicknessCount = 0;
        }

        patient.SetSickness(sicknessPool[sicknessCount]);
        sicknessCount++;

        patient.PatientName = App.Instance.GameplayCore.PatientManager.names.GetRandomName();
        PatientManager.OnPatientSpawnFinalized.Invoke(patient);
    }
    public void SetPatientStats(Patient patient, Sickness sickness) 
    {
        patient.SetSickness(sickness);
        patient.PatientName = App.Instance.GameplayCore.PatientManager.names.GetRandomName();
    }
}
