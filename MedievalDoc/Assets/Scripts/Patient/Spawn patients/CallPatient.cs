using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPatient : MonoBehaviour, IInteractable
{
    [SerializeField] public float interactionTime;
    [SerializeField] public Transform tempSpawnPosition;
    public float InteractionTime { get { return interactionTime; } set { interactionTime = value; } }

    private GameObject patientPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PickupController.OnInteract.AddListener(CallPatientIn);
        patientPrefab = App.Instance.GameplayCore.PatientManager.patientPrefab;
    }

    private void CallPatientIn(GameObject interactionObject, PickupController player)
    {
        if (interactionObject != gameObject)
            return;

        var patient = Instantiate(patientPrefab, tempSpawnPosition); //Instantiate patient prefab at spawn position
    }
}
