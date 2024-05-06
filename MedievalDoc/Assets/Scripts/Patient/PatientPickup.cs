using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientPickup : MonoBehaviour
{
    private PickupController playerController;
    GameObject player;
    Patient patient;
    Transform toolPickupPoint;

    private void Start()
    {
        PickupController.OnPickup.AddListener(PickupPatient);
        PickupController.OnPutdown.AddListener(PutdownPatient);
        PlayerManager playerManager = App.Instance.GameplayCore.PlayerManager;
        toolPickupPoint = playerManager.PickupController.GetToolPickupPoint();
        player = playerManager.PickupController.GetPlayerGameObject();
        playerController = playerManager.PickupController.GetPickupController();
    }

    private void PickupPatient(GameObject pickedPatient, Transform objectPoint)
    {
        if (pickedPatient == this.gameObject)
        {
            if (objectPoint != null) {
                objectPoint.GetComponentInChildren<PatientLayDownPoint>().IfOccupied = false;
            }
            
            playerController.PickedItem = pickedPatient;
            pickedPatient.GetComponent<Collider>().enabled = false;
            pickedPatient.transform.position = toolPickupPoint.position;
            pickedPatient.transform.SetParent(player.transform);
            var lastChild = player.transform.childCount - 1;
            player.transform.GetChild(lastChild).localEulerAngles = Vector3.zero;

            // Release chair (fix)
            if (pickedPatient.GetComponent<Patient>().SpawnerID >= 0)
            {
                GetComponent<Patient>().SpawnerID = -69;
            }
        }
        

    }

    private void PutdownPatient(PickupController pickedPatient, Transform pickupPoint)
    {
        if (pickedPatient.PickedItem == null || pickupPoint == null) {
            return;
        }

        PatientPickup patientPickup = pickedPatient.PickedItem.GetComponent<PatientPickup>();
        PatientLayDownPoint patientLayDownPoint = pickupPoint.GetComponentInChildren<PatientLayDownPoint>();

        // TO ADD PUTING PATIENT ON FLOOR 
        if (patientPickup && patientLayDownPoint && !patientLayDownPoint.IfOccupied)
        {
            GameObject putDownFurniture = pickedPatient.PickedItem;

            pickupPoint.GetComponentInChildren<PatientLayDownPoint>().IfOccupied = true;
            putDownFurniture.transform.position = patientLayDownPoint.transform.position;
            putDownFurniture.transform.rotation = patientLayDownPoint.transform.rotation;
            putDownFurniture.transform.SetParent(pickupPoint);
            putDownFurniture.GetComponent<Collider>().enabled = true;
            playerController.PickedItem = null;
        }
    }
}
