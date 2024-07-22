using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
public class InitializePatientStats : MonoBehaviour
{
    private Sickness[] sicknessPool;

    void Start()
    {
        PatientManager.OnPatientSpawn.AddListener(SetPatientStats);
    }

    

    private void SetPatientStats(Patient patient)
    {
        sicknessPool = ImportJsonData.sicknessConfig;
        patient.SetSickness(sicknessPool[Random.Range(0, sicknessPool.Length - 1)]);
        patient.PatientName = App.Instance.GameplayCore.PatientManager.names.GetRandomName();
        PatientManager.OnPatientSpawnFinalized.Invoke(patient);
    }
    public void SetPatientStats(Patient patient, Sickness sickness) 
    {
        patient.SetSickness(sickness);
        patient.PatientName = App.Instance.GameplayCore.PatientManager.names.GetRandomName();
    }
}
