using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatientManager : MonoBehaviour
{
    [SerializeField] public GameObject patientPrefab;
    [SerializeField] public List<Patient> patients;
    [SerializeField] public List<SicknessScriptableObject> sicknessPool; //List of sicknesses available to spawn
    [SerializeField] public Names names;
    public static UnityEvent<Patient> OnPatientSpawn = new UnityEvent<Patient>();
    public static UnityEvent<Patient> OnPatientSpawnFinalized = new UnityEvent<Patient>(); //Event called after patient stats are set
    public static UnityEvent<Patient> OnPatientReleased = new UnityEvent<Patient>(); //Event called after patient stats are set
    public static UnityEvent ReleasePatient = new UnityEvent();

    private void Awake()
    {
        App.Instance.GameplayCore.RegisterPatientManager(this);
    }
    private void Start()
    {
        Patient.OnCureDisease.AddListener(RemovePatientFromList);
        //Patient.OnPatientDeath.AddListener(RemovePatientFromList);
        OnPatientSpawn.AddListener(AddPatientToList);
        OnPatientReleased.AddListener(RemovePatientFromList);
    }
    private void OnDestroy()
    {
        App.Instance.GameplayCore.UnregisterPatientManager();
    }

    private void RemovePatientFromList(Patient patient)
    {
        patients.Remove(patient);
    }
    private void AddPatientToList(Patient patient)
    {
        patients.Add(patient);
    }
    public void InvokePatientRelease()
    {
        OnPatientReleased.Invoke(App.Instance.GameplayCore.PatientManager.patients[0]);
    }
}
