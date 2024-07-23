using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
public class InitializePatientStats : MonoBehaviour
{
    private List<Sickness> sicknessPool;
    private string sicknessContainersKey;

    void Start()
    {
        PatientManager.OnPatientSpawn.AddListener(SetPatientStats);
        sicknessPool = new List<Sickness>();
        sicknessContainersKey = Data.ImportJsonData.levelConfig[LevelButtons.levelID - 1].sicknessContainer;
    }

    

    private void SetPatientStats(Patient patient)
    {
        if (Data.ImportJsonData.sicknessContainersConfig.Length > (LevelButtons.levelID - 1)) { // Check if sickness container exists for this level

            Debug.Log(HelperFunctions.SicknessContainersLookup(sicknessContainersKey));
            foreach (var sickness in Data.ImportJsonData.sicknessContainersConfig[LevelButtons.levelID - 1].levelSicknesses) {
                sicknessPool.Add(HelperFunctions.SicknessLookup(sickness));
                Debug.Log(sickness);
            }
        }

        patient.SetSickness(sicknessPool[Random.Range(0, sicknessPool.Count - 1)]);
        patient.PatientName = App.Instance.GameplayCore.PatientManager.names.GetRandomName();
        PatientManager.OnPatientSpawnFinalized.Invoke(patient);
    }
    public void SetPatientStats(Patient patient, Sickness sickness) 
    {
        patient.SetSickness(sickness);
        patient.PatientName = App.Instance.GameplayCore.PatientManager.names.GetRandomName();
    }
}
