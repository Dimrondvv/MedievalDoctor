using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePatientStats : MonoBehaviour
{
    private List<SicknessScriptableObject> sicknessPool;

    void Start()
    {
        sicknessPool = App.Instance.GameplayCore.PatientManager.sicknessPool;
        PatientManager.OnPatientSpawn.AddListener(SetPatientStats);
    }

    

    private void SetPatientStats(Patient patient)
    {
        patient.SetSickness(sicknessPool[Random.Range(0, sicknessPool.Count - 1)]);
        patient.PatientName = App.Instance.GameplayCore.PatientManager.names.GetRandomName();
        PatientManager.OnPatientSpawnFinalized.Invoke(patient);
    }
    private void SetPatientStats(Patient patient, SicknessScriptableObject sickness) 
    {
        patient.SetSickness(sickness);
        patient.PatientName = App.Instance.GameplayCore.PatientManager.names.GetRandomName();
        PatientManager.OnPatientSpawnFinalized.Invoke(patient);
    }
}
