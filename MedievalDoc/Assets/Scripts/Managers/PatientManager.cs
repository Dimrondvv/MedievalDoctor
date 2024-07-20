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
    public static UnityEvent RageQuitPatient = new UnityEvent();

    private void Awake()
    {
        App.Instance.GameplayCore.RegisterPatientManager(this);
    }
    private void Start()
    {
        //Clear Sickness Pool
        sicknessPool.Clear();

        // Load Data from Imported json
        if (Data.ImportJsonData.sicknessContainersConfig.Length > (LevelButtons.levelID - 1)) { // Check if sickness container exists for this level
            foreach (var sickness in Data.ImportJsonData.sicknessContainersConfig[LevelButtons.levelID - 1].levelSicknesses) {
                sicknessPool.Add(Resources.Load($"Sicknesses/{sickness}") as SicknessScriptableObject);
            }
        }
        

        //Debug.Log(Data.ImportJsonData.sicknessContainersConfig[LevelButtons.levelID-1].levelSicknesses[0]);
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
