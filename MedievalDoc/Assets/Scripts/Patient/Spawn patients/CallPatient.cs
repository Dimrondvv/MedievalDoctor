using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPatient : MonoBehaviour, IInteractable
{
    [SerializeField] public float interactionTime;
    [SerializeField] public Transform tempSpawnPosition;
    public float InteractionTime { get { return interactionTime; } set { interactionTime = value; } }

    private GameObject patientPrefab;
    public GameObject PatientPrefab { get { return patientPrefab; }}

    private PatientWalking patientWalking;
    private AudioSource bellAudio;

    // Start is called before the first frame update
    void Start()
    {
        PickupController.OnInteract.AddListener(CallPatientIn);
        patientPrefab = App.Instance.GameplayCore.PatientManager.patientPrefab;
        patientWalking = GetComponent<PatientWalking>();
        bellAudio = GetComponent<AudioSource>();
    }

    private void CallPatientIn(GameObject interactionObject, PickupController player)
    {
        if (interactionObject != gameObject || App.Instance.GameplayCore.PatientManager.patients.Count != 0)
            return;

        bellAudio.Play();
        var patient = Instantiate(patientPrefab, tempSpawnPosition); //Instantiate patient prefab at spawn position
        patientWalking.StartWalking(patient, patientWalking.endPoint);
    }
}
