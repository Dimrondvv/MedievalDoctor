using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePatientStats : MonoBehaviour
{
    private List<SicknessScriptableObject> sicknessPool;

    void Start()
    {
        PatientManager.OnPatientSpawn.AddListener(SetPatientStats);
    }

    

    private void SetPatientStats(Patient patient)
    {
        sicknessPool = App.Instance.GameplayCore.PatientManager.sicknessPool;
        patient.SetSickness(sicknessPool[Random.Range(0, sicknessPool.Count - 1)]);
        patient.PatientName = App.Instance.GameplayCore.PatientManager.names.GetRandomName();
        PatientManager.OnPatientSpawnFinalized.Invoke(patient);
    }
    public void SetPatientStats(Patient patient, SicknessScriptableObject sickness) 
    {
        patient.SetSickness(sickness);
        patient.PatientName = App.Instance.GameplayCore.PatientManager.names.GetRandomName();
    }
}
